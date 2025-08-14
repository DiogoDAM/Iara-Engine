using System;
using System.Collections.Generic;

namespace IaraEngine;

public abstract class Entity : GameObject 
{
	public ComponentList Components;

	public Entity() : base()
	{
		Components = new(this);
	}

	public override void Start()
	{
		foreach(Component c in Components)
		{
			c.Start();
		}

		Components.ProcessAddAndRemove();
	}

	public override void Update()
	{
		foreach(Component c in Components)
		{
			c.Update();
		}

		Components.ProcessAddAndRemove();
	}

	public override void Draw()
	{
		foreach(Component c in Components)
		{
			c.Draw();
		}
	}

	//Methods to handle the Components 
	//
	public void AddComponent(Component c) 
	{
		Components.Add(c);
	}

	public T RegisterComponent<T>() where T : Component, new()
	{
		T c = new();

		Components.Add(c);

		return c;
	}

	public void RemoveComponent(Component c) 
	{
		Components.Remove(c, false);
	}

	public void DestroyComponent(Component c) 
	{
		Components.Remove(c, true);
	}

	public bool ContainsComponent(Component c) 
	{
		return Components.Contains(c);
	}

	public Component FindComponent(Predicate<Component> predicate)
	{
		return Components.Find(predicate);
	}

	public List<Component> FindAllComponent(Predicate<Component> predicate)
	{
		return Components.FindAll(predicate);
	}

	public T GetComponentByType<T>() where T : Component
	{
		return Components.GetComponentByType<T>();
	}

	public List<T> GetAllComponentsByType<T>() where T : Component
	{
		return Components.GetAllComponentsByType<T>();
	}


	protected override void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Desactive();
				Scene = null;
				Components.Dispose();
				Disposed = true;
			}
		}
	}
}
