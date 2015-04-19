using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Core;

public class SecondaryCharacter : Character
{
	public float fromY;
	public float toY;

	private bool _isRunning = false;
	private bool isRunning
	{
		get
		{
			return _isRunning;
		}
		set
		{
			_isRunning = value;

			if (_isRunning)
			{
				_animator.speed = 1.5f;
			}
			else
			{
				_animator.speed = 1f;
			}
		}
	}

	protected override float currentSpeed
	{
		get
		{
			if (_isRunning)
				return base.currentSpeed * 2f;
			else
				return base.currentSpeed;
		}
	}

	protected Target _target;

	public Transform foodTarget;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	protected override void Awake()
	{
		base.Awake();

		SetRandomSize();
		SetRandomSpeed();
		SetDirection(DirectionStatic.GetRandom());

		SetDepth(Random.Range(fromY, toY));

		Game.Instance.fartMachine.Add(this);

		StartThinking();
	}

	protected override void Update()
	{
		if (!_isRunning && _target != null)
		{
			SetDirection(DirectionToTarget());
		}

		if (outOfRightBorder)
		{
			SetDirection(Direction.Left);
		}
		if (outOfLeftBorder)
		{
			SetDirection(Direction.Right);
		}

		base.Update();
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void SenseFart(Vector3 pos, float power)
	{
		GetComponent<RandomTalker>().SenseFart();
		MoveAwayFromPos(pos);

		StopAllCoroutines();
		StartCoroutine(RunAway(power));
	}

	// ================================================================================
	//  privat methods
	// --------------------------------------------------------------------------------

	private void TargetRandomFoodTable()
	{
		FoodTable table = Game.Instance.fartMachine.foodTables.PickRandom();
		Vector3 targetPos = table.range.RandomPos();

		Target target = new Target(table, targetPos);

		_target = target;
	}

	private Direction DirectionToTarget()
	{
		if (_target == null)
		{
			return Direction.None;
		}

		float targetDistance = 0.3f;
		if (_target.targetPos.x > _transform.position.x + targetDistance)
			return Direction.Right;
		else if (_target.targetPos.x < _transform.position.x - targetDistance)
			return Direction.Left;
		else
		{
			return Direction.None;
		}
	}

	private IEnumerator RunAway(float time)
	{
		StartRunning();
		yield return new WaitForSeconds(time);
		StopRunning();
	}

	private void StartRunning()
	{
		isRunning = true;
		_target = null;
	}

	private void StopRunning()
	{
		isRunning = false;
		StartThinking();
	}

	private void StartThinking()
	{
		StartCoroutine(Thinking());
	}

	private IEnumerator Thinking()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(3f, 4f));

			if (_target == null)
			{
				TargetRandomFoodTable();
			}
			else
			{
				if (DirectionToTarget() == Direction.None)
				{
					Eat();					
				}
			}
		}
	}

	private void Eat()
	{
		Game.Instance.fartMachine.Eat();
		Game.Instance.gameAudioManager.PlayEatingSound();
		GameObject food = GameObjectFactory.GameObject(Game.Instance.fartMachine.foodPrefab, foodTarget.position);
		Destroy(food, 2f);
	}

	private void SetDepth(float p)
	{
		_transform.position = new Vector3(_transform.position.x, p, _transform.position.z);
	}

	private void SetRandomSize()
	{
		_displayObject.transform.localScale *= Random.Range(0.95f, 1.05f);
	}

	private void SetRandomSpeed()
	{
		float variation = Random.Range(0.8f, 1.2f);
		speed = variation * _startSpeed;
	}

	public void Leave()
	{
		StopAllCoroutines();
		Game.Instance.fartMachine.Remove(this);
		Deactivate();

		StartCoroutine(StartLeaving());
	}

	private IEnumerator StartLeaving()
	{
		yield return new WaitForSeconds(0.7f);
		_animationController.FadeOutFast(.5f);
		Destroy(gameObject, 0.5f);
	}
}