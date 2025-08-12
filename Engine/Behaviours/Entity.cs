using System;
using Microsoft.Xna.Framework;

namespace IaraEngine;

public class Entity : IDisposable, IPrototype
{
	public bool Disposed { get; private set; }

	public Transform Transform;

	public bool IsActive; 
	public bool IsDrawable;

	public string Layer;
	public Scene Scene;
	public string Tag;

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
	public static void AddToScene(Entity e, string layer="Instances")
	{
		IaraGame.CurrentScene.AddEntity(e, layer);
	}

	public static T CreateToScene<T>(string layer="Instances") where T : Entity, new()
	{
		T obj = new T();

		IaraGame.CurrentScene.AddEntity(obj, layer);

		return obj;
	}

	public static T CreateToScene<T>(Transform parent, string layer="Instances") where T : Entity, new()
	{
		T obj = new T();

		obj.Transform.Parent = parent;

		IaraGame.CurrentScene.AddEntity(obj, layer);

		return obj;
	}

	public static Entity InstantiateToScene(Entity prefab, string layer="Instances")
	{
		Entity other = (Entity)prefab.DeepClone();

		IaraGame.CurrentScene.AddEntity(other, layer);

		return other;
	}

	public static Entity InstantiateToScene(Entity prefab, Vector2 position, string layer="Instances")
	{
		Entity other = (Entity)prefab.DeepClone();

		IaraGame.CurrentScene.AddEntity(other, layer);

		other.Transform.Position = position;

		return other;
	}

	public static Entity InstantiateToScene(Entity prefab, Vector2 position, float rotation, Vector2 scale, string layer="Instances")
	{
		Entity other = (Entity)prefab.DeepClone();

		IaraGame.CurrentScene.AddEntity(other, layer);

		other.Transform.Position = position;
		other.Transform.Rotation = rotation;
		other.Transform.Scale = scale;

		return other;
	}

	public static void RemoveFromScene(Entity e, string layer="Instances")
	{
		IaraGame.CurrentScene.RemoveEntity(e, layer);
	}

	public static void DestroyFromScene(Entity e, string layer="Instances")
	{
		IaraGame.CurrentScene.DestroyEntity(e, layer);
	}

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

    public IPrototype ShallowClone()
    {
		Entity e = (Entity)MemberwiseClone();
		return e;
    }

    public IPrototype DeepClone()
    {
		Entity e = (Entity)MemberwiseClone();

		e.Transform.Position = Transform.Position;
		e.Transform.Rotation = Transform.Rotation;
		e.Transform.Scale = Transform.Scale;

		return e;
    }
}
