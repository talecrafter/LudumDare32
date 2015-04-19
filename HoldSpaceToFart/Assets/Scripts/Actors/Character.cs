using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;

public class Character : MonoBehaviour
{
	public const float gravity = 45f;

	public float speed = 3.0f;
	protected float _startSpeed;

	protected Direction current = Direction.Right;
	protected Direction lookDirection = Direction.Right;

	[SerializeField]
	protected GameObject _displayObject;

	protected Animator _animator;
	protected AnimationController _animationController;
	protected Transform _transform;

	protected virtual float currentSpeed
	{
		get
		{
			return speed;
		}
	}

	// ================================================================================
	//  border
	// --------------------------------------------------------------------------------

	protected virtual bool outOfRightBorder
	{
		get
		{
			return _transform.position.x >= rightBorder;
		}
	}

	protected virtual bool outOfLeftBorder
	{
		get
		{
			return _transform.position.x <= leftBorder;
		}
	}

	protected virtual float rightBorder
	{
		get
		{
			return Game.Instance.borderRight;
		}
	}

	protected virtual float leftBorder
	{
		get
		{
			return Game.Instance.borderLeft;
		}
	}

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		_transform = transform;
		_startSpeed = speed;
		_animator = GetComponentInChildren<Animator>();
		_animationController = GetComponent<AnimationController>();

		_animationController.FadeIn();
	}

	protected virtual void Update()
	{
		AutomaticMovement();

		HoldInBounds();

		if (current == Direction.Left || current == Direction.Right)
		{
			_animator.SetInteger("state", 1);
		}
		else
		{
			_animator.SetInteger("state", 0);
		}
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void MoveAwayFromPos(Vector3 pos)
	{
		if (pos.x > _transform.position.x)
		{
			SetDirection(Direction.Left);
		}
		else
		{
			SetDirection(Direction.Right);
		}
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void AutomaticMovement()
	{
		if (current == Direction.Left || current == Direction.Right)
		{
			Vector3 direction = new Vector3(1.0f, 0, 0);
			if (current == Direction.Left)
			{
				direction = new Vector3(-1.0f, 0, 0);

			}

			float change = currentSpeed;

			_transform.position += direction * change * Time.deltaTime;
		}
	}

	private void HoldInBounds()
	{
		if (outOfLeftBorder)
		{
			_transform.position = new Vector3(leftBorder, _transform.position.y, _transform.position.z);
		}
		if (outOfRightBorder)
		{
			_transform.position = new Vector3(rightBorder, _transform.position.y, _transform.position.z);
		}
	}

	protected void SetDirection(Direction newDirection)
	{
		if (newDirection != current)
		{
			if ((newDirection == Direction.Left && lookDirection == Direction.Right)
				|| (newDirection == Direction.Right && lookDirection == Direction.Left))
			{
				FlipDisplay();
			}

			current = newDirection;
		}
	}

	protected void FlipDisplay()
	{
		lookDirection = lookDirection.Flip();
		Vector3 previous = _displayObject.transform.localScale;
		_displayObject.transform.localScale = new Vector3(-previous.x, previous.y, previous.z);
	}

	protected Direction GetRandomDirection()
	{
		if (Random.Range(0, 2) == 0)
		{
			return Direction.Left;
		}
		else
		{
			return Direction.Right;
		}
	}
}