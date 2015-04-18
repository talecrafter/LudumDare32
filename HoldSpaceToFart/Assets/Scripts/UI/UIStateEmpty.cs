using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;

public class UIStateEmpty : MonoBehaviour, IUiState<GameState>
{
	[SerializeField]
	private GameState _gameState = GameState.None;

	public GameState gameState
	{
		get
		{
			return _gameState;
		}
	}

	public virtual void Enter()
	{

	}

	public virtual void Exit()
	{

	}

	public void InputUp()
	{
		
	}

	public void InputDown()
	{
		
	}

	public void InputLeft()
	{
		
	}

	public void InputRight()
	{
		
	}

	public void InputEnter()
	{
		
	}

	public void InputBack()
	{
		
	}
}