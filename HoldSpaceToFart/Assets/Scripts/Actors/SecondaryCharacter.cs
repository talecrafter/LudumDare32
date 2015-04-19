using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	protected override void Awake()
	{
		base.Awake();

		SetRandomSize();
		SetRandomSpeed();

		SetDepth(Random.Range(fromY, toY));

		Game.Instance.fartMachine.Add(this);
	}

	protected override void Update()
	{
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

	private IEnumerator RunAway(float time)
	{
		isRunning = true;
		yield return new WaitForSeconds(time);
		isRunning = false;
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
}