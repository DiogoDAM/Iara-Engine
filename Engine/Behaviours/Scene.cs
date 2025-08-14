using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

namespace IaraEngine;

public abstract class Scene : IDisposable
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


	//Methods to handle GameObjects
	//
	public void AddGameObject(GameObject e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.AddGameObject() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].AddGameObject(e);
	}

	public void RemoveGameObject(GameObject e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.RemoveGameObject() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].RemoveGameObject(e);
	}

	public void DestroyGameObject(GameObject e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.DestroyGameObject() the layer: {layer} don't exist in the Scene: {Name}");

		Layers[layer].RemoveGameObject(e, true);
	}
	
	public bool ContainsGameObject(GameObject e, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.ContainsGameObject() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].GameObjects.Contains(e);
	}

	public GameObject FindGameObject(Predicate<GameObject> predicate, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.FindGameObject() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].GameObjects.Find(predicate);
	}

	public List<GameObject> FindAllGameObject(Predicate<GameObject> predicate, string layer)
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.FindAllGameObject() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].GameObjects.FindAll(predicate);
	}

	public T GetGameObjectByType<T>(string layer) where T : GameObject
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.GetGameObjectByType() the layer: {layer} don't exist in the Scene: {Name}");

		return (T)Layers[layer].GameObjects.GetGameObjectByType<T>();
	}

	public List<T> GetAllGameObjectsByType<T>(string layer) where T : GameObject
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.GetAllGameObjectsByType() the layer: {layer} don't exist in the Scene: {Name}");

		return Layers[layer].GameObjects.GetAllGameObjectsByType<T>();
	}

	public T GetGameObjectByTag<T>(string tag, string layer) where T : GameObject
	{
		if(!Layers.ContainsKey(layer)) throw new KeyNotFoundException($"IaraEngine :: Scene.GetGameObjectByTag() the layer: {layer} don't exist in the Scene: {Name}");
		if(String.IsNullOrEmpty(tag)) throw new NullReferenceException($"IaraEngine :: Scene.GetGameObjectByTag() the tag is null or empty");

		return Layers[layer].GameObjects.GetGameObjectByTag<T>(tag);
	}

	public int GetCountGameObjects()
	{
		int count = 0;
		foreach(var pair in Layers)
		{
			count += pair.Value.GameObjects.Count;
		}

		return count;
	}

	public int GetCountGameObjectsInLayer(string layer)
	{
		return Layers[layer].GameObjects.Count;
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
