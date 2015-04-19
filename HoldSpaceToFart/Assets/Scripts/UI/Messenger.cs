using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class Messenger : MonoBehaviour
{
	private FadingTimer _timer = new FadingTimer(0.4f, 2.0f, 0.4f);

	private Text[] _text;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	public void Awake()
	{
		_timer.Stop();

		if (_text == null || _text.Length == 0)
			_text = GetComponentsInChildren<Text>();
		if (_text == null || _text.Length == 0)
		{
			var messengerObject = GameObject.Find("MessengerText") as GameObject;
			if (messengerObject != null)
				_text = messengerObject.GetComponentsInChildren<Text>();
		}

		if (_text != null)
		{
			for (int i = 0; i < _text.Length; i++)
			{
				_text[i].enabled = false;
			}
		}
	}

	public void Update()
	{
		if (!_timer.hasEnded)
		{
			_timer.Update();
			UpdateColor();
		}

		if (_timer.hasEnded)
		{
			for (int i = 0; i < _text.Length; i++)
			{
				_text[i].enabled = false;
			}
		}
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void Message(string newMessage)
	{
		for (int i = 0; i < _text.Length; i++)
		{
			_text[i].text = newMessage;
			_text[i].enabled = true;
		}

		_timer.Reset();

		UpdateColor();
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void UpdateColor()
	{
		float alpha = _timer.progress;
		if (_timer.hasEnded)
			alpha = 0;

		Color displayColor = new Color(1.0f, 1.0f, 1.0f, _timer.progress);
		if (_timer.hasEnded)
			displayColor.a = 0;

		for (int i = 0; i < _text.Length; i++)
		{
			Color color = _text[i].color;
			color.a = alpha;
			_text[i].color = color;
		}
	}
}
