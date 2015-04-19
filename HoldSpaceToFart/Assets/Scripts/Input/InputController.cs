using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;

public class InputController : MonoBehaviour
{
	PlayerCharacter player;

	private Timer _timer;

	void Awake()
	{
		player = FindObjectOfType<PlayerCharacter>();

		_timer = new Timer(1f);
	}

	void OnLevelWasLoaded(int levelIndex)
	{
		player = FindObjectOfType<PlayerCharacter>();
	}

	void Update()
	{
		// check if in first level
		if (Application.loadedLevel == 0)
		{
			_timer.Update();

			if (Input.anyKeyDown || Input.GetMouseButtonDown(0) && _timer.hasEnded)
			{
				// load main level
				Game.Instance.NewGame();
			}

			return;
		}

		if (Game.isRunning)
		{
			UpdateKeyboard();

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Game.Instance.Pause();
			}
		}
		else if (Game.isPaused)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Game.Instance.Resume();
			}
		}
	}

	private void UpdateKeyboard()
	{
		float direction = Input.GetAxis("Horizontal");
		if (direction > 0)
		{
			player.SetMovement(Direction.Right);
		}
		else if (direction < 0)
		{
			player.SetMovement(Direction.Left);
		}
		else
		{
			player.SetMovement(Direction.None);
		}

		if (Input.GetButton("Jump"))
		{
			player.InputFart();
		}
	}
}