using System;

namespace IaraEngine;

public class Entity : IDisposable
{
	public bool Disposed { get; private set; }

	public Transform Transform;

	public bool IsActive; 
	public bool IsDrawable;

	public string Layer;
	public Scene Scene;

	public Entity() 
	{ 
		Transform = new();
	}

	//Behaviours Methods
	//
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if(!IsActive || Disposed) return;
	}

	public virtual void Draw()
	{
		if(!IsDrawable || Disposed) return;
	}

	public virtual void Added()
	{
	}

	public virtual void Removed()
	{
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

	//Methods to handle entities in the IaraGame.CurrentScene
	//
	public static void AddToScene(Entity e, string layer)
	{
	}

	public static T CreateToScene<T>(string layer)
	{
	}

	public static Entity Instantiate(Entity prefab, string layer)
	{
	}

	public static Entity Instantiate(Entity prefab, string layer, Transform transform)
	{
	}

	public static void RemoveFromScene(Entity e, string layer)
	{
	}

	public static void DestroyFromScene(Entity e, string layer)
	{
	}

	public static void

	//Dispose Methods
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

				Scene = null;

				Disposed = true;
			}
		}
	}
}
