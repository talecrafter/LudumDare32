using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Core;

public class RandomImage : MonoBehaviour
{
	public List<Sprite> images;

	void Awake()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = images.PickRandom();
	}
}