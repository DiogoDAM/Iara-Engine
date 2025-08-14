using System;
using System.Collections;
using System.Collections.Generic;

namespace IaraEngine;
public class ComponentList : IEnumerable, IEnumerable<Component>, IDisposable {

	public List<Component> Components { get; private set; }
	private Queue<Component> _toAdd;
	private Queue<(Component, bool)> _toRemove;

	public Entity Entity;

	public int Count => Components.Count;

	public bool Disposed { get; private set; }

	public ComponentList(Entity entity) 
	{ 
		Components = new();
		_toAdd = new();
		_toRemove = new();
		Entity = entity;
	}

	public void Add(Component obj)
	{
		_toAdd.Enqueue(obj);
	}

	public void Remove(Component obj, bool disposable)
	{
		if(!Components.Contains(obj)) return;

		_toRemove.Enqueue((obj,disposable));
	}

	public bool Contains(Component obj)
	{
		return Components.Contains(obj);
	}

	public Component Find(Predicate<Component> predicate)
	{
		return Components.Find(predicate);
	}

	public List<Component> FindAll(Predicate<Component> predicate)
	{
		return Components.FindAll(predicate);
	}

	public T GetComponentByType<T>() where T : Component 
	{
		return (T)Components.Find(e => e is T);
	}

	public List<T> GetAllComponentsByType<T>() where T : Component 
	{
		return Components.FindAll(e => e is T).ConvertAll<T>(e => (T)e);
	}

	public void ProcessAddAndRemove()
	{
		for(int i=0; i<_toAdd.Count; i++)
		{
			var obj = _toAdd.Dequeue();
			Components.Add(obj);
			obj.Active();
			obj.Added(Entity);
		}

		for(int i=0; i<_toRemove.Count; i++)
		{
			var tup = _toRemove.Dequeue();
			var obj = tup.Item1;
			var disposable = tup.Item2;

			Components.Remove(obj);
			obj.Desactive();
			obj.Removed();
			if(disposable) obj.Dispose();
		}
	}

    IEnumerator<Component> IEnumerable<Component>.GetEnumerator()
    {
		return Components.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
		return Components.GetEnumerator();
    }

    public void Dispose()
    {
		if(!Disposed)
		{
			Disposed = true;
			foreach(Component c in Components)
			{
				c.Dispose();
			}
			Components.Clear();
		}

		GC.SuppressFinalize(this);
    }
}
