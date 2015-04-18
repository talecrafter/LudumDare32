using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PlayerCharacter : Character
{
	Direction targetDirection = Direction.None;

	public Transform cameraFocus;

	protected override float rightBorder
	{
		get
		{
			return Game.Instance.borderRight;
		}
	}

	protected override float leftBorder
	{
		get
		{
			return Game.Instance.borderLeft;
		}
	}

	protected override void Update()
	{
		if (outOfRightBorder && current == Direction.Right)
			SetDirection(Direction.None);

		if (outOfLeftBorder && current == Direction.Left)
			SetDirection(Direction.None);

		base.Update();
	}

	public void SetMovement(Direction newDirection)
	{
		targetDirection = newDirection;

		if (targetDirection == Direction.Right && outOfRightBorder)
			targetDirection = Direction.None;

		if (targetDirection == Direction.Left && outOfLeftBorder)
			targetDirection = Direction.None;

		SetDirection(targetDirection);
	}
}