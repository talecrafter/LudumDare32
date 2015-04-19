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

	public GameObject visitorPrefab;

	public List<SecondaryCharacter> characters = new List<SecondaryCharacter>();

	public Transform leftMarker;
	public Transform rightMarker;
	public int maxCount = 10;

	void Awake()
	{
		StartCoroutine(StartSpawning());
	}

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

	public void Spawn()
	{
		float posX = Random.Range(Game.Instance.borderLeft, Game.Instance.borderRight);
		Vector3 pos = new Vector3(posX, 0, 0);
		GameObject newVisitor = GameObjectFactory.GameObject(visitorPrefab, pos);
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private IEnumerator StartSpawning()
	{
		while (true)
		{
			if (characters.Count < maxCount)
			{
				Spawn();
			}

			yield return new WaitForSeconds(5f);
		}
	}
}