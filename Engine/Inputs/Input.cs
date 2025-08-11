using System;

namespace IaraEngine;

public class Input
{
	public KeyboardManager Keyboard;

	public Input()
	{
		Keyboard = new();
	}

	public void Update()
	{
		Keyboard.Update();
	}
}
