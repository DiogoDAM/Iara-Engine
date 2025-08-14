using System;

using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class SceneLayer : IDisposable
{
	public Scene Scene;
	public string Name;
	public GameObjectList GameObjects;
	public SamplerState SamplerState = SamplerState.PointWrap;

	public bool Disposed { get; private set; }
	public bool IsActive;
	public bool IsDrawable;

	public SceneLayer(Scene scene, string name)
	{
		Scene = scene;
		Name = name;
		GameObjects = new();
	}

	//Methods to handle GameObjects
	//
	public void AddGameObject(GameObject GameObject)
	{
		GameObjects.Add(GameObject);
		GameObject.Layer = Name;
		GameObject.Scene = Scene;
	}

	public void RemoveGameObject(GameObject GameObject, bool disposable=false)
	{
		GameObjects.Remove(GameObject, disposable);
	}

	//Behaviour Methods
	//
	public void Start()
	{
		GameObjects.ProcessAddAndRemove();

		foreach(GameObject e in GameObjects)
		{
			e.Start();
		}

		IsActive = true;
		IsDrawable = true;
	}

	public void Update()
	{
		if(!IsActive || Disposed) return;

		foreach(GameObject e in GameObjects)
		{
			e.Update();
		}

		GameObjects.ProcessAddAndRemove();
	}

	public void Draw()
	{
		if(!IsDrawable || Disposed) return;

		IaraGame.SpriteBatch.Begin(samplerState: SamplerState);

			foreach(GameObject e in GameObjects)
			{
				e.Draw();
			}
		
		IaraGame.SpriteBatch.End();
	}

	public void Dispose()
	{
		if(!Disposed)
		{
			IsActive = false;
			IsDrawable = false;

			GameObjects.Dispose();
			Scene = null;

			Disposed = true;
		}
	}
}
