using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;
using CraftingLegends.Core;

public class AudioManager : BaseAudioManager
{
	public AudioClip roundWon;
	public AudioClip roundLost;

	public List<AudioClip> eatingSounds;

	public void PlayRoundWon()
	{
		Play(roundWon);
	}

	public void PlayRoundLost()
	{
		Play(roundLost);
	}

	public void PlayEatingSound()
	{
		Play(eatingSounds.PickRandom());
	}
}