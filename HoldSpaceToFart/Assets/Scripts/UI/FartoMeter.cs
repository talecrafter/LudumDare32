using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class FartoMeter : MonoBehaviour
{
	public GameObject helpMessage;
	public GameObject fartPowerDisplay;

	public Color lowestColor;
	public Color highestColor;

    void Update()
	{
		UpdateFromPlayer();
    }

	private void UpdateFromPlayer()
	{
		if (Game.Instance.playerCharacter.isFarting)
		{
			helpMessage.SetActive(false);
			fartPowerDisplay.SetActive(true);

			float power = Game.Instance.playerCharacter.fartMass;

			fartPowerDisplay.transform.localScale = new Vector3(power, fartPowerDisplay.transform.localScale.y, fartPowerDisplay.transform.localScale.z);
		}
		else
		{
			helpMessage.SetActive(true);
			fartPowerDisplay.SetActive(false);
		}
	}
}