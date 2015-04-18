using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Fart : MonoBehaviour
{
	public List<ParticleSystem> particleSystems;

	public void TriggerFart(float power)
	{
		float shakePower = 1f + power * .3f;

		foreach (var item in particleSystems)
		{
			item.startSize *= shakePower;
			item.startSpeed *= shakePower;
			item.Emit(500);			
		}
	}
}