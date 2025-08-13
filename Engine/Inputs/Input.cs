using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace IaraEngine
{
	public class Input
	{
		public MouseManager Mouse;
		public KeyboardManager Keyboard;

		private Dictionary<string, List<InputAction>> _actions;

		public Input()
		{
			Mouse = new();
			Keyboard = new();
			_actions = new();
		}

		public void Update()
		{
			Mouse.Update();
			Keyboard.Update();
		}
	
		public void AddAction(string actionName)
		{
			_actions.Add(actionName, new List<InputAction>());
		}

		public void AddKeyboardAction(string actionName, Keys key)
		{
			if(!_actions.ContainsKey(actionName)) throw new KeyNotFoundException($"IaraEngine :: Input.AddKeyboardAction() Input don't have {actionName} action");

			_actions[actionName].Add(InputAction.CreateKeyboardAction(key));
		}

		public void AddMouseAction(string actionName, byte button)
		{
			if(!_actions.ContainsKey(actionName)) throw new KeyNotFoundException($"IaraEngine :: Input.AddMouseAction() Input don't have {actionName} action");

			_actions[actionName].Add(InputAction.CreateMouseAction(button));
		}

		public List<InputAction> GetAction(string actionName)
		{
			if(!_actions.ContainsKey(actionName)) throw new KeyNotFoundException($"IaraEngine :: Input.GetAction() Input don't have {actionName} action");

			return _actions[actionName];
		}

		public bool IsDown(List<InputAction> actions)
		{
			foreach(var action in actions)
			{
				switch(action.Type)
				{
					case EInputActionType.Keyboard: return Keyboard.IsKeyDown(action.Key);
					case EInputActionType.Mouse: return Mouse.IsButtonDown(action.Button);
				}
			}

			return false;
		}

		public bool IsUp(List<InputAction> actions)
		{
			foreach(var action in actions)
			{
				switch(action.Type)
				{
					case EInputActionType.Keyboard: return Keyboard.IsKeyUp(action.Key);
					case EInputActionType.Mouse: return Mouse.IsButtonUp(action.Button);
				}
			}

			return false;
		}

		public bool WasPressed(List<InputAction> actions)
		{
			foreach(var action in actions)
			{
				switch(action.Type)
				{
					case EInputActionType.Keyboard: return Keyboard.WasKeyPressed(action.Key);
					case EInputActionType.Mouse: return Mouse.WasButtonPressed(action.Button);
				}
			}

			return false;
		}

		public bool WasReleased(List<InputAction> actions)
		{
			foreach(var action in actions)
			{
				switch(action.Type)
				{
					case EInputActionType.Keyboard: return Keyboard.WasKeyReleased(action.Key);
					case EInputActionType.Mouse: return Mouse.WasButtonReleased(action.Button);
				}
			}

			return false;
		}
	}
}
