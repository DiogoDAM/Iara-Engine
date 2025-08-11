using System;

using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class SceneLayer : IDisposable
{
	public Scene Scene;
	public string Name;
	public EntityList Entities;
	public SamplerState SamplerState = SamplerState.PointWrap;

	public bool Disposed { get; private set; }
	public bool IsActive;
	public bool IsDrawable;

	public SceneLayer(Scene scene, string name)
	{
		Scene = scene;
		Name = name;
		Entities = new();
	}

	//Methods to handle entities
	//
	public void AddEntity(Entity entity)
	{
		Entities.Add(entity);
		entity.Layer = Name;
		entity.Scene = Scene;
	}

	public void RemoveEntity(Entity entity, bool disposable=false)
	{
		Entities.Remove(entity, disposable);
	}

	//Behaviour Methods
	//
	public void Start()
	{
		Entities.ProcessAddAndRemove();

		foreach(Entity e in Entities)
		{
			e.Start();
		}

		IsActive = true;
		IsDrawable = true;
	}

	public void Update()
	{
		if(!IsActive || Disposed) return;

		foreach(Entity e in Entities)
		{
			e.Update();
		}

		Entities.ProcessAddAndRemove();
	}

	public void Draw()
	{
		if(!IsDrawable || Disposed) return;

		IaraGame.SpriteBatch.Begin(samplerState: SamplerState);

			foreach(Entity e in Entities)
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

			Entities.Dispose();
			Scene = null;

			Disposed = true;
		}
	}
}
