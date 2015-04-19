using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Core;
using CraftingLegends.Framework;

public class FartMachine : MonoBehaviour
{
	public float scale;
	public float foodPerBite = 0.1f;

	public List<AudioClip> smallFart;
	public List<AudioClip> bigFart;

	public GameObject visitorPrefab;

	public List<SecondaryCharacter> characters = new List<SecondaryCharacter>();
	public List<FoodTable> foodTables;

	public Transform leftMarker;
	public Transform rightMarker;
	public int maxCount = 10;

	public int startSpawn = 10;

	private int currentRound = 0;

	public FartGameState state = FartGameState.Inbetween;
	private Timer _roundTimer;

	private float maxFood;
	private float food;

	public GameObject foodPrefab;

	public float foodLeft
	{
		get
		{
			if (food <= 0)
				return 0;

			return food / maxFood;
		}
	}

	public float timeProgress
	{
		get
		{
			if (_roundTimer == null || _roundTimer.hasEnded)
				return 1;

			return _roundTimer.progress;
		}
	}

	public List<int> spawnPerRound = new List<int>();
	public List<float> timePerRound = new List<float>();
	public List<float> foodPerRound = new List<float>();

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	void Awake()
	{
		StartGameplay();
	}

	void OnLevelWasLoaded(int levelIndex)
	{
		StartGameplay();
	}

	void Update()
	{
		if (state == FartGameState.Running)
		{
			_roundTimer.Update();

			if (_roundTimer.hasEnded)
			{
				WinRound();
			}
		}
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void Add(SecondaryCharacter secondaryCharacter)
	{
		if (!characters.Contains(secondaryCharacter))
			characters.Add(secondaryCharacter);
	}

	public void Remove(SecondaryCharacter secondaryCharacter)
	{
		if (characters.Contains(secondaryCharacter))
			characters.Remove(secondaryCharacter);
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

	public void DespawnRandom()
	{
		SecondaryCharacter visitor = characters.PickRandom();
		visitor.Leave();
	}

	public void WinRound()
	{
		EndOfRoundCleanUp();

		currentRound++;

		Game.Instance.gameAudioManager.PlayRoundWon();

		Game.Instance.messenger.Message("Round won!");
		StartCoroutine(ShowEndOfRound("You have saved enough food"));
	}

	public void LoseRound()
	{
		EndOfRoundCleanUp();

		Game.Instance.gameAudioManager.PlayRoundLost();

		Game.Instance.messenger.Message("Round lost!");
		StartCoroutine(ShowEndOfRound("The visitors ate too much food"));
	}

	private IEnumerator ShowEndOfRound(string message)
	{
		//yield return new WaitForSeconds(4f);

		//Game.Instance.messenger.Message(message);

		yield return new WaitForSeconds(2f);

		StartCoroutine(BeginRound());
	}

	public void Eat()
	{
		if (state == FartGameState.Running)
		{
			food -= foodPerBite;

			if (food <= 0)
			{
				LoseRound();
			}
		}
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void DespawnAll()
	{
		List<SecondaryCharacter> despawnChars = new List<SecondaryCharacter>();
		foreach (var visitor in characters)
		{
			despawnChars.Add(visitor);
		}

		foreach (var visitor in despawnChars)
		{
			visitor.Leave();
		}
	}

	private void EndOfRoundCleanUp()
	{
		state = FartGameState.Inbetween;
		DespawnAll();
	}

	private void StartGameplay()
	{
		if (Application.loadedLevel > 0)
		{
			foodTables = new List<FoodTable>(FindObjectsOfType<FoodTable>());

			StartCoroutine(Intro());
		}
	}

	private IEnumerator Intro()
	{
		yield return new WaitForSeconds(3f);
		Game.Instance.messenger.Message("Welcome to the vernissage!");
		yield return new WaitForSeconds(4f);
		Game.Instance.messenger.Message("Everything is prepared...");
		yield return new WaitForSeconds(4f);
		Game.Instance.messenger.Message("...but the food is not enough.");
		yield return new WaitForSeconds(4f);
		Game.Instance.messenger.Message("Hinder the visitors from eating too much.");
		yield return new WaitForSeconds(4f);
		StartCoroutine(BeginRound());
	}

	private IEnumerator BeginRound()
	{
		yield return new WaitForSeconds(1f);

		Game.Instance.messenger.Message("Difficulty " + (currentRound + 1).ToString());
		state = FartGameState.Running;
		_roundTimer = new Timer(GetTimeForRound(currentRound));
		maxFood = GetFoodForRound(currentRound);
		food = maxFood;

		for (int i = 0; i < GetSpawnNumberForRound(currentRound); i++)
		{
			Spawn();
		}
	}

	private int GetSpawnNumberForRound(int round)
	{
		if (round < spawnPerRound.Count)
		{
			return spawnPerRound[round];
		}

		return spawnPerRound[spawnPerRound.Count - 1] + (round - spawnPerRound.Count) * 2;
	}

	private float GetTimeForRound(int round)
	{
		if (round < timePerRound.Count)
		{
			return timePerRound[round];
		}

		return timePerRound[timePerRound.Count - 1] + (round - timePerRound.Count) * 2;
	}

	private float GetFoodForRound(int round)
	{
		if (round < foodPerRound.Count)
		{
			return foodPerRound[round];
		}

		return foodPerRound[foodPerRound.Count - 1];
	}

	private IEnumerator StartSpawning()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);

			if (characters.Count < maxCount)
			{
				Spawn();
			}
		}
	}
}