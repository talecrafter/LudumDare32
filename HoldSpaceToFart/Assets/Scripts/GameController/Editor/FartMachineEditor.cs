using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEditor;

[CustomEditor(typeof(FartMachine))]
public class FartMachineEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (Application.isPlaying)
		{
			if (GUILayout.Button("Spawn", GUILayout.Height(40f)))
			{
				FartMachine machine = target as FartMachine;
				machine.Spawn();
			}
		}
	}
}