using System;
using Microsoft.Xna.Framework;

namespace IaraEngine;

public abstract class GameObject : IDisposable, IPrototype
{
	public bool Disposed { get; protected set; }

	public Transform Transform;

	public bool IsActive; 
	public bool IsDrawable;

	public string Layer;
	public Scene Scene;
	public string Tag;

	public GameObject() 
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
	public static void AddToScene(GameObject e, string layer="Instances")
	{
		IaraGame.CurrentScene.AddGameObject(e, layer);
	}

	public static T CreateToScene<T>(string layer="Instances") where T : GameObject, new()
	{
		T obj = new T();

		IaraGame.CurrentScene.AddGameObject(obj, layer);

		return obj;
	}

	public static T CreateToScene<T>(Transform parent, string layer="Instances") where T : GameObject, new()
	{
		T obj = new T();

		obj.Transform.Parent = parent;

		IaraGame.CurrentScene.AddGameObject(obj, layer);

		return obj;
	}

	public static GameObject InstantiateToScene(GameObject prefab, string layer="Instances")
	{
		GameObject other = (GameObject)prefab.DeepClone();

		IaraGame.CurrentScene.AddGameObject(other, layer);

		return other;
	}

	public static GameObject InstantiateToScene(GameObject prefab, Vector2 position, string layer="Instances")
	{
		GameObject other = (GameObject)prefab.DeepClone();

		IaraGame.CurrentScene.AddGameObject(other, layer);

		other.Transform.Position = position;

		return other;
	}

	public static GameObject InstantiateToScene(GameObject prefab, Vector2 position, float rotation, Vector2 scale, string layer="Instances")
	{
		GameObject other = (GameObject)prefab.DeepClone();

		IaraGame.CurrentScene.AddGameObject(other, layer);

		other.Transform.Position = position;
		other.Transform.Rotation = rotation;
		other.Transform.Scale = scale;

		return other;
	}

	public static void RemoveFromScene(GameObject e, string layer="Instances")
	{
		IaraGame.CurrentScene.RemoveGameObject(e, layer);
	}

	public static void DestroyFromScene(GameObject e, string layer="Instances")
	{
		IaraGame.CurrentScene.DestroyGameObject(e, layer);
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
		GameObject e = (GameObject)MemberwiseClone();
		return e;
    }

    public virtual IPrototype DeepClone()
    {
		GameObject e = (GameObject)MemberwiseClone();

		e.Transform.Position = Transform.Position;
		e.Transform.Rotation = Transform.Rotation;
		e.Transform.Scale = Transform.Scale;

		return e;
    }
}
