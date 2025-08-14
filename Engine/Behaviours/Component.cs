using System;

namespace IaraEngine;

public abstract class Component : IDisposable
{
	public Entity Entity;

	public string Name;

	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }
	public bool Disposed { get; protected set; }

	public Component()
	{
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void Draw()
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

	public virtual void Added(Entity e)
	{
		Entity = e;
	}

	public virtual void Removed()
	{
		Entity = null;
	}

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
				Entity = null;
				Disposed = true;
			}
		}
	}
}
