using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using CraftingLegends.Framework;

public class UIManager : BaseUIManager
{
	public void Awake()
	{
		if (!base.Init())
		{
			DestroyImmediate(gameObject);
			return;
		}
	}
}