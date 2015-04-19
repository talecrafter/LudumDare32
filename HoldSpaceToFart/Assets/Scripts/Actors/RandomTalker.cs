using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;
using CraftingLegends.Core;

public class RandomTalker : MonoBehaviour
{
	public TextMesh mesh;

	private FadingTimer _fadingTimer = null;

	public bool isTalking { get { return _fadingTimer != null; } }

	public List<string> sentences;
	public List<string> fartSensed;
	public List<Color> colors;
	public List<Color> angerColors;

    void Start()
	{
		mesh.color = colors.PickRandom();
		mesh.color = Utilities.ColorWithAlpha(mesh.color, 0);
		StartCoroutine(Talking());
    }

	private IEnumerator Talking()
	{
		while (true)
		{
			float randomTime = Random.Range(0, 2f);
			yield return new WaitForSeconds(randomTime);

			if (!isTalking)
				TalkRandomSentence();
		}
	}

	private void TalkRandomSentence()
	{
		if (ExtRandom.Chance(1, 3))
		{
			Talk(sentences.PickRandom());
		}
		else
		{
			Talk("Bla Bla");
		}
	}

    void Update()
	{
		if (_fadingTimer != null)
		{
			_fadingTimer.Update();
			if (_fadingTimer.hasEnded)
			{
				_fadingTimer = null;
			}
		}
		UpdateColor();
    }

	private void UpdateColor()
	{
		Color prevColor = mesh.color;

		if (_fadingTimer != null)
		{
			mesh.color = new Color(prevColor.r, prevColor.g, prevColor.b, _fadingTimer.progress);
		}
		else
		{
			mesh.color = new Color(prevColor.r, prevColor.g, prevColor.b, 0);
		}
	}

	public void Talk(string text, bool angered = false)
	{
		if (angered)
			mesh.color = angerColors.PickRandom();
		else
			mesh.color = colors.PickRandom();

		mesh.text = text;
		_fadingTimer = new FadingTimer(0.5f, 4f, 0.5f);
	}

	public void SenseFart()
	{
		Talk(fartSensed.PickRandom(), true);
	}
}