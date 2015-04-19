using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Target
{
	public FoodTable foodTable;
	public Vector3 targetPos;

	public Target(FoodTable table, Vector3 pos)
	{
		foodTable = table;
		targetPos = pos;
	}
}