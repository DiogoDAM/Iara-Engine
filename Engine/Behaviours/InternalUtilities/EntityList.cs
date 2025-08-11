using System;
using System.Collections;
using System.Collections.Generic;

namespace IaraEngine;

public class EntityList : IEnumerable, IEnumerable<Entity>, IDisposable
{
	public List<Entity> Entities { get; private set; }
	private Queue<Entity> _toAdd;
	private Queue<(Entity, bool)> _toRemove;

	public int Count => Entities.Count;

	public bool Disposed { get; private set; }

	public EntityList() 
	{ 
		Entities = new();
		_toAdd = new();
		_toRemove = new();
	}

	public void Add(Entity obj)
	{
		_toAdd.Enqueue(obj);
	}

	public void Remove(Entity obj, bool disposable)
	{
		if(!Entities.Contains(obj)) return;

		_toRemove.Enqueue((obj,disposable));
	}

	public bool Contains(Entity obj)
	{
		return Entities.Contains(obj);
	}

	public Entity Find(Predicate<Entity> predicate)
	{
		return Entities.Find(predicate);
	}

	public List<Entity> FindAll(Predicate<Entity> predicate)
	{
		return Entities.FindAll(predicate);
	}

	public T GetEntityType<T>() where T : Entity 
	{
		return (T)Entities.Find(e => e is T);
	}

	// public List<T> GetAllEntities<T>() where T : Entity 
	// {
	// 	return Entities.FindAll(e => e is T);
	// }

	public void ProcessAddAndRemove()
	{
		for(int i=0; i<_toAdd.Count; i++)
		{
			var obj = _toAdd.Dequeue();
			Entities.Add(obj);
			obj.Active();
			obj.Added();
		}

		for(int i=0; i<_toRemove.Count; i++)
		{
			var tup = _toRemove.Dequeue();
			var obj = tup.Item1;
			var disposable = tup.Item2;

			Entities.Remove(obj);
			obj.Desactive();
			obj.Removed();
			if(disposable) obj.Dispose();
		}
	}

    IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator()
    {
		return Entities.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
		return Entities.GetEnumerator();
    }

    public void Dispose()
    {
		if(!Disposed)
		{
			Disposed = true;
			Entities.Clear();
		}

		GC.SuppressFinalize(this);
    }
}
