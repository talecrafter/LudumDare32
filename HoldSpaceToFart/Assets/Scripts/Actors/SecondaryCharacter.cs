using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SecondaryCharacter : Character
{
	public float fromY;
	public float toY;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	protected override void Awake()
	{
		base.Awake();

		SetDepth(Random.Range(fromY, toY));
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
	//  privat methods
	// --------------------------------------------------------------------------------

	private void SetDepth(float p)
	{
		_transform.position = new Vector3(_transform.position.x, p, _transform.position.z);
	}

	private void SetRandomSize()
	{
		_transform.localScale *= Random.Range(0.85f, 1.15f);
	}

	private void SetRandomSpeed()
	{
		float variation = Random.Range(0.8f, 1.2f);
		speed = variation * _startSpeed;
	}
}