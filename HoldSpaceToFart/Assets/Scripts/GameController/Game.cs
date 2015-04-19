using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;

public class Game : BaseGameController
{
	// Singleton pattern
	public static new Game Instance = null;

	public AudioManager gameAudioManager;
	public UIManager uiManager;

	public PlayerCharacter playerCharacter;

	public FartMachine fartMachine;

	void Awake()
	{
		if (base.Init() == false)
		{
			DestroyImmediate(gameObject);
			return;
		}

		Instance = this;

		gameAudioManager = GetComponent<AudioManager>();
		uiManager = FindObjectOfType<UIManager>();
		fartMachine = GetComponent<FartMachine>();
	}

	protected override void LoadLevelData()
	{
		base.LoadLevelData();

		playerCharacter = FindObjectOfType<PlayerCharacter>();
	}

	public float borderRight
	{
		get
		{
			return levelBounds.xMax - 2.5f;
		}
	}

	public float borderLeft
	{
		get
		{
			return levelBounds.xMin + 2.5f;
		}
	}
}