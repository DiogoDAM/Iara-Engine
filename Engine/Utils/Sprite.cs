using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace IaraEngine;

public class Sprite : IDisposable
{
	public Transform Transform;
	public TextureRegion Region;

	public Vector2 Origin = Vector2.Zero;
	public float LayerDepth = 0f;
	public SpriteEffects Flip = SpriteEffects.None;
	public Color Color = Color.White;

	public int Width { get { return Region.SourceRectangle.Width; }  }
	public int Height { get { return Region.SourceRectangle.Height; }  }

	public bool Disposed { get; protected set; }

	public Sprite() { }

	public Sprite(TextureRegion texture)
	{
		Region = texture;
		Transform = new();
	}

	public Sprite(TextureRegion texture, Vector2 pos)
	{
		Region = texture;
		Transform = new(pos);
	}

	public Sprite(TextureRegion texture, Transform transform)
	{
		Region = texture;
		Transform = new();
		Transform.Parent = transform;
	}

	public void LookAt(Vector2 target)
	{
		Transform.LookAt(target);
	}

	public void CenterOrigin()
	{
		Origin = new Vector2(Width, Height) * .5f;
	}

	public virtual void Draw()
	{
		IaraGame.SpriteBatch.Draw(Region.Texture, Transform.GlobalPosition, Region.SourceRectangle, Color, Transform.Rotation, Origin, Transform.Scale, Flip, LayerDepth);
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
				Transform = null;
				Region = null;
				Disposed = true;
			}
		}
	}
}
