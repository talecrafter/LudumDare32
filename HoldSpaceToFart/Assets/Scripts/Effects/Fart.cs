using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Fart : MonoBehaviour
{
	public List<ParticleSystem> particleSystems;

	public ParticleSystem left;
	public ParticleSystem right;

	public void TriggerFart(float power, Direction direction)
	{
		float shakePower = 1f + power * .3f;

		foreach (var item in particleSystems)
		{
			SpawnFartSmoke(item, shakePower);
		}

		if (direction == Direction.Left)
		{
			SpawnFartSmoke(left, shakePower);
		}
		else
		{
			SpawnFartSmoke(right, shakePower);
		}
	}

	private static void SpawnFartSmoke(ParticleSystem particleSystem, float shakePower)
	{
		particleSystem.startSize *= shakePower;
		particleSystem.startSpeed *= shakePower;
		particleSystem.Emit(500);
	}
}