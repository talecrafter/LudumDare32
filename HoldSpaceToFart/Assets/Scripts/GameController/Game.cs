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

	void Awake()
	{
		if (base.Init() == false)
		{
			DestroyImmediate(gameObject);
			return;
		}

		Instance = this;

		gameAudioManager = GetComponent<AudioManager>();
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