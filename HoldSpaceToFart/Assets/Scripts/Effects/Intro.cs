using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Intro : MonoBehaviour
{
    IEnumerator Start()
	{
		Game.Instance.playerCharacter.SetMovement(Direction.Left);
		yield return new WaitForSeconds(2.2f);
		Game.Instance.playerCharacter.SetMovement(Direction.None);
		yield return new WaitForSeconds(0.5f);
		Game.Instance.playerCharacter.TriggerFart();
		Game.Instance.playerCharacter.SetMovement(Direction.None);
    }
}