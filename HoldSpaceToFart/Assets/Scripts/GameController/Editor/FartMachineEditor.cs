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

		FartMachine machine = target as FartMachine;

		if (GUILayout.Button("Add", GUILayout.Height(40f)))
		{
			if (Application.isPlaying)
				machine.Spawn();
		}
		if (GUILayout.Button("Remove", GUILayout.Height(40f)))
		{
			if (Application.isPlaying)
				machine.Despawn();
		}
	}
}