using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
	public Image timeImage;
	public GameObject timeDisplay;

	public Image foodImage;
	public GameObject foodDisplay;

    void Update()
	{
		UpdateTime();
		UpdateFood();
    }

	private void UpdateFood()
	{
		if (Game.Instance.fartMachine.state == FartGameState.Running)
		{
			foodDisplay.SetActive(true);
			float progress = Game.Instance.fartMachine.foodLeft;
			foodImage.fillAmount = progress;
		}
		else
		{
			foodDisplay.SetActive(false);
		}
	}

	private void UpdateTime()
	{
		if (Game.Instance.fartMachine.state == FartGameState.Running)
		{
			timeDisplay.SetActive(true);
			float progress = Game.Instance.fartMachine.timeProgress;
			timeImage.fillAmount = 1f - progress;
		}
		else
		{
			timeDisplay.SetActive(false);
		}
	}
}