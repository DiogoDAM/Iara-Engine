using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IaraEngine;

public class MouseManager
{
	private MouseState _prev;
	private MouseState _curr;

	public Vector2 CursorPosition => _curr.Position.ToVector2();

	public MouseManager()
	{
		_prev = Mouse.GetState();
		_curr = Mouse.GetState();
	}

	public void Update()
	{
		_prev = _curr;
		_curr = Mouse.GetState();
	}

	public bool IsButtonDown(byte button)
	{
		switch(button)
		{
			case 0: return _curr.LeftButton == ButtonState.Pressed;
			case 1: return _curr.RightButton == ButtonState.Pressed;
			case 2: return _curr.MiddleButton == ButtonState.Pressed;
			default: throw new NotImplementedException($"IaraEngine :: The MouseManager don't have the button: {button}");
		}
	}

	public bool IsButtonUp(byte button)
	{
		switch(button)
		{
			case 0: return _curr.LeftButton == ButtonState.Released;
			case 1: return _curr.RightButton == ButtonState.Released;
			case 2: return _curr.MiddleButton == ButtonState.Released;
			default: throw new NotImplementedException($"IaraEngine :: The MouseManager don't have the button: {button}");
		}
	}

	public bool WasButtonPressed(byte button)
	{
		switch(button)
		{
			case 0: return _curr.LeftButton == ButtonState.Pressed && _prev.LeftButton == ButtonState.Released;
			case 1: return _curr.RightButton == ButtonState.Pressed && _prev.RightButton == ButtonState.Released;
			case 2: return _curr.MiddleButton == ButtonState.Pressed && _prev.MiddleButton == ButtonState.Released;
			default: throw new NotImplementedException($"IaraEngine :: The MouseManager don't have the button: {button}");
		}
	}

	public bool WasButtonReleased(byte button)
	{
		switch(button)
		{
			case 0: return _curr.LeftButton == ButtonState.Released && _prev.LeftButton == ButtonState.Pressed;
			case 1: return _curr.RightButton == ButtonState.Released && _prev.RightButton == ButtonState.Pressed;
			case 2: return _curr.MiddleButton == ButtonState.Released && _prev.MiddleButton == ButtonState.Pressed;
			default: throw new NotImplementedException($"IaraEngine :: The MouseManager don't have the button: {button}");
		}
	}
}
