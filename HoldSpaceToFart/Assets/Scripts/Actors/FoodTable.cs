using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class FoodTable : MonoBehaviour
{
	public Range range
	{
		get
		{
			if (_range != null)
				CalculateRange();

			return _range;
		}
	}
	private Range _range = null;

	void Awake()
	{
		CalculateRange();
	}

	private void CalculateRange()
	{
		_range = new Range(transform.position, 4f, 4f);
	}
}