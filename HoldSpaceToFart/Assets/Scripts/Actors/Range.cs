using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Range
{
	public Vector3 pivot;
	public float left;
	public float right;

	public Range(Vector3 pivot, float toTheLeft, float toTheRight)
	{
		this.pivot = pivot;

		left = pivot.x - toTheLeft;
		right = pivot.x + toTheRight;
	}

	public bool Contains(Vector3 pos)
	{
		if (pos.x >= left && pos.x <= right)
			return true;

		return false;
	}

	public bool Contains(Character character)
	{
		return Contains(character.transform.position);
	}

	public Vector3 RandomPos()
	{
		float x = Random.Range(left, right);
		return new Vector3(x, 0, 0);
	}

	public Vector3 leftPos
	{
		get
		{
			return new Vector3(left, pivot.y, pivot.z);
		}
	}

	public Vector3 rightPos
	{
		get
		{
			return new Vector3(right, pivot.y, pivot.z);
		}
	}
}