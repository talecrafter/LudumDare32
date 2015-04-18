using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SecondaryCharacter : Character
{
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