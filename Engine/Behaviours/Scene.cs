using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

namespace IaraEngine;

public class Scene : IDisposable
{
	protected Dictionary<string, SceneLayer> Layers;

	public string Name;

	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }
	public bool Disposed { get; protected set; }

	public ContentManager Content { get; protected set; }
	public AssetsManager Assets { get; protected set; }

	public Scene()
	{
		Layers = new();
		Layers.Add("Instances", new SceneLayer(this, "Instances"));

		Content = new ContentManager(IaraGame.Content.ServiceProvider);
		Content.RootDirectory = "Content";
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
	public void AddEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.AddEntity() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].AddEntity(e);
	}

	public void RemoveEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.RemoveEntity() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].RemoveEntity(e);
	}

	public void DestroyEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.DestroyEntity() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].RemoveEntity(e, true);
	}
	
	public bool ContainsEntity(Entity e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.ContainsEntity() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].Entities.Contains(e);
	}

	public Entity FindEntity(Predicate<Entity> predicate, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.FindEntity() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].Entities.Find(predicate);
	}

	public List<Entity> FindAllEntity(Predicate<Entity> predicate, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.FindAllEntity() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].Entities.FindAll(predicate);
	}

	public T GetEntityByType<T>(string layer) where T : Entity
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.GetEntityByType() the layer: {layer} don't exist in the Scene: {Name}");

		return (T)Layers[layer].Entities.GetEntityByType<T>();
	}

	public List<T> GetAllEntitiesByType<T>(string layer) where T : Entity
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.GetAllEntitiesByType() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].Entities.GetAllEntitiesByType<T>();
	}

	public T GetEntityByTag<T>(string tag, string layer) where T : Entity
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.GetEntityByTag() the layer: {layer} don't exist in the Scene: {Name}");
		if(String.IsNullOrEmpty(tag)) throw new NullReferenceException($"IaraEngine :: Scene.GetEntityByTag() the tag is null or empty");

		return Layers[layer].Entities.GetEntityByTag<T>(tag);
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
