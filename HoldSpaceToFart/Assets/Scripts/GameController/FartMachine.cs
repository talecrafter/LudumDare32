using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Core;

public class FartMachine : MonoBehaviour
{
	public float scale;

	public List<AudioClip> smallFart;
	public List<AudioClip> bigFart;

	public List<SecondaryCharacter> characters = new List<SecondaryCharacter>();

	public Transform leftMarker;
	public Transform rightMarker;

	public void Add(SecondaryCharacter secondaryCharacter)
	{
		if (!characters.Contains(secondaryCharacter))
			characters.Add(secondaryCharacter);
	}

	public void Fart(Vector3 pos, float power, Direction direction)
	{
		// play audio
		if (power >= 1f)
		{
			Game.Instance.gameAudioManager.Play(bigFart.PickRandom());
		}
		else
		{
			Game.Instance.gameAudioManager.Play(smallFart.PickRandom());
		}

		// screen shake
		float shakePower = 1f + power * .3f;
		Game.Instance.screenShake.Shake(shakePower);

		float inFartDirection = 3f * power * scale;
		float againstFartDirection = 1f * power * scale;

		Range range;
		if (direction == Direction.Right)
		{
			range = new Range(pos, againstFartDirection, inFartDirection);
		}
		else
		{
			range = new Range(pos, inFartDirection, againstFartDirection);
		}

		// debug display
		leftMarker.position = range.leftPos;
		rightMarker.position = range.rightPos;

		foreach (var character in characters)
		{
			if (range.Contains(character))
			{
				character.SenseFart(range.pivot, power);
			}
		}
	}
}