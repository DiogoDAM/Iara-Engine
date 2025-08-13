using Microsoft.Xna.Framework.Input;

namespace IaraEngine;

public enum EInputActionType
{
	Keyboard,
	Mouse,
}

public class InputAction
{
	public EInputActionType Type { get; private set; }

	public Keys Key { get; set; }
	public byte Button { get; set; }

	public InputAction() { }

	public static InputAction CreateKeyboardAction(Keys key)
	{
		return new InputAction() { Type = EInputActionType.Keyboard, Key = key };
	}

	public static InputAction CreateMouseAction(byte button)
	{
		return new InputAction() { Type = EInputActionType.Keyboard, Button = button };
	}
}
