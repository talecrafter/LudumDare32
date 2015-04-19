using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Core;

public class PlayerCharacter : Character
{
	public float minimumFart = .5f;
	public float maxFart = 5f;

	public float fartMass = 0;
	public bool isFarting = false;
	private bool _lastInputWasFarting = false;

	Direction targetDirection = Direction.None;

	public Transform cameraFocus;

	public GameObject fartPrefab;

	public Transform fartLocation;


	public float fartProgress
	{ 
		get
		{
			if (!isFarting)
				return 0;

			return fartMass / maxFart;
		}
	}

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

	protected override float currentSpeed
	{
		get
		{
			if (isFarting)
				return 0f;

			return base.currentSpeed;
		}
	}

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	protected override void Update()
	{
		if (outOfRightBorder && current == Direction.Right)
			SetDirection(Direction.None);

		if (outOfLeftBorder && current == Direction.Left)
			SetDirection(Direction.None);

		if (isFarting)
		{
			if (_lastInputWasFarting)
			{
				UpdateFarting();
			}
			else
			{
				StopFarting();
			}
		}
		_lastInputWasFarting = false;

		base.Update();
	}

	private void UpdateFarting()
	{
		fartMass += Time.deltaTime;

		if (fartMass > maxFart)
			fartMass = maxFart;
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void InputFart()
	{
		_lastInputWasFarting = true;
		if (!isFarting)
		{
			BeginFarting();
		}
	}

	private void BeginFarting()
	{
		isFarting = true;
		fartMass = 0;
	}

	private void StopFarting()
	{
		isFarting = false;

		if (fartMass > minimumFart)
		{
			Fart();
		}
	}

	private void Fart()
	{
		float fartPower = fartMass;

		// create visuals
		GameObject fartObject = GameObjectFactory.GameObject(fartPrefab, fartLocation.position);
		Fart fart = fartObject.GetComponent<Fart>();
		fart.TriggerFart(fartPower, lookDirection.Flip());
		Destroy(fartObject, 3f);

		Game.Instance.fartMachine.Fart(_transform.position, fartPower, lookDirection.Flip());
	}

	public void SetMovement(Direction newDirection)
	{
		if (isFarting)
			return;

		targetDirection = newDirection;

		if (targetDirection == Direction.Right && outOfRightBorder)
			targetDirection = Direction.None;

		if (targetDirection == Direction.Left && outOfLeftBorder)
			targetDirection = Direction.None;

		SetDirection(targetDirection);
	}
}