using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class RandomColor : MonoBehaviour
{
	public Color fromColor;
	public Color toColor;

	public void Awake()
	{
		SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		Color newColor = new Color(Random.Range(fromColor.r, toColor.r), Random.Range(fromColor.g, toColor.g), Random.Range(fromColor.b, toColor.b));
		foreach (var spriteRenderer in spriteRenderers)
		{
			spriteRenderer.color = newColor;
		}
	}
}