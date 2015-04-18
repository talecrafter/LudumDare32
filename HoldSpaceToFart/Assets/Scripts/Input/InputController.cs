using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class InputController : MonoBehaviour
{
	PlayerCharacter player;

	void Awake()
	{
		player = FindObjectOfType<PlayerCharacter>();
	}

	void Update()
	{
		if (Game.isRunning)
		{
			UpdateKeyboard();			
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