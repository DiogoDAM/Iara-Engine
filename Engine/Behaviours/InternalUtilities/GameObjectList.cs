using System;
using System.Collections;
using System.Collections.Generic;

namespace IaraEngine;
public class GameObjectList : IEnumerable, IEnumerable<GameObject>, IDisposable {

	public List<GameObject> Objects { get; private set; }
	private Queue<GameObject> _toAdd;
	private Queue<(GameObject, bool)> _toRemove;

	public int Count => Objects.Count;

	public bool Disposed { get; private set; }

	public GameObjectList() 
	{ 
		Objects = new();
		_toAdd = new();
		_toRemove = new();
	}

	public void Add(GameObject obj)
	{
		_toAdd.Enqueue(obj);
	}

	public void Remove(GameObject obj, bool disposable)
	{
		if(!Objects.Contains(obj)) return;

		_toRemove.Enqueue((obj,disposable));
	}

	public bool Contains(GameObject obj)
	{
		return Objects.Contains(obj);
	}

	public GameObject Find(Predicate<GameObject> predicate)
	{
		return Objects.Find(predicate);
	}

	public List<GameObject> FindAll(Predicate<GameObject> predicate)
	{
		return Objects.FindAll(predicate);
	}

	public T GetGameObjectByType<T>() where T : GameObject 
	{
		return (T)Objects.Find(e => e is T);
	}

	public List<T> GetAllGameObjectsByType<T>() where T : GameObject 
	{
		return Objects.FindAll(e => e is T).ConvertAll<T>(e => (T)e);
	}

	public T GetGameObjectByTag<T>(string tag) where T : GameObject
	{
		return (T)Objects.Find(e => e.Tag == tag);
	}

	public void ProcessAddAndRemove()
	{
		for(int i=0; i<_toAdd.Count; i++)
		{
			var obj = _toAdd.Dequeue();
			Objects.Add(obj);
			obj.Active();
			obj.Added();
		}

		for(int i=0; i<_toRemove.Count; i++)
		{
			var tup = _toRemove.Dequeue();
			var obj = tup.Item1;
			var disposable = tup.Item2;

			Objects.Remove(obj);
			obj.Desactive();
			obj.Removed();
			if(disposable) obj.Dispose();
		}
	}

    IEnumerator<GameObject> IEnumerable<GameObject>.GetEnumerator()
    {
		return Objects.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
		return Objects.GetEnumerator();
    }

    public void Dispose()
    {
		if(!Disposed)
		{
			Disposed = true;
			foreach(GameObject g in Objects)
			{
				g.Dispose();
			}
			Objects.Clear();
		}

		GC.SuppressFinalize(this);
    }
}
