using System;
using System.Collections.Generic;

namespace IaraEngine;

public class Scene : IDisposable
{
	protected Dictionary<string, SceneLayer> Layers;

	public string Name;

	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }
	public bool Disposed { get; protected set; }

	public Scene()
	{
		Layers = new();
		Layers.Add("Instances", new SceneLayer(this, "Instances"));
	}

	//Enumerator Methods
	public virtual void Added()
	{
	}

	public virtual void Removed()
	{
	}

	//Behaviour Methods
	public virtual void Start()
	{
		foreach(var pair in Layers)
		{
			pair.Value.Start();
		}
	}

	public virtual void Update()
	{
		if(!IsActive || Disposed) return;

		foreach(var pair in Layers)
		{
			pair.Value.Update();
		}
	}

	public virtual void Draw()
	{
		if(!IsDrawable || Disposed) return;

		foreach(var pair in Layers)
		{
			pair.Value.Draw();
		}
	}

	public virtual void Active()
	{
		IsActive = true;
		IsDrawable = true;
	}

	public virtual void Desactive()
	{
		IsActive = false;
		IsDrawable = false;
	}


	//Methods to handle entities
	//
	public virtual void AddEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.AddEntity() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].AddEntity(e);
	}

	public virtual void RemoveEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.AddEntity() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].RemoveEntity(e);
	}

	public virtual void DestroyEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.AddEntity() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].RemoveEntity(e, true);
	}

	public int GetCountEntities()
	{
		int count = 0;
		foreach(var pair in Layers)
		{
			count += pair.Value.Entities.Count;
		}

		return count;
	}

	public int GetCountEntitiesInLayer(string layer)
	{
		return Layers[layer].Entities.Count;
	}

	//Methods to Dispose
	//
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Desactive();

				foreach(var pair in Layers)
				{
					pair.Value.Dispose();
				}

				Layers.Clear();

				Disposed = true;
			}
		}
	}
}
