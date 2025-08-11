using Microsoft.Xna.Framework.Input;

namespace IaraEngine;

public class KeyboardManager
{
	private KeyboardState _prev;
	private KeyboardState _curr;

	public KeyboardManager()
	{
		_prev = Keyboard.GetState();
		_curr = Keyboard.GetState();
	}

	public void Update()
	{
		_prev = _curr;
		_curr = Keyboard.GetState();
	}

	public bool IsKeyDown(Keys key)
	{
		return _curr.IsKeyDown(key);
	}

	public bool IsKeyUp(Keys key)
	{
		return _curr.IsKeyUp(key);
	}

	public bool WasKeyPressed(Keys key)
	{
		return _curr.IsKeyDown(key) && _prev.IsKeyUp(key);
	}

	public bool WasKeyReleased(Keys key)
	{
		return _curr.IsKeyUp(key) && _prev.IsKeyDown(key);
	}

	public bool IsKeysDown(params Keys[] keys)
	{
		foreach(Keys key in keys)
		{
			if(_curr.IsKeyUp(key)) return false;
		}

		return true;
	}

	public bool IsKeysUp(params Keys[] keys)
	{
		foreach(Keys key in keys)
		{
			if(_curr.IsKeyDown(key)) return false;
		}

		return true;
	}
}
