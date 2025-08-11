using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class Sprite
{
	public Transform Transform;
	public TextureRegion Region;

	public Vector2 Origin = Vector2.Zero;
	public float LayerDepth = 0f;
	public SpriteEffects Flip = SpriteEffects.None;
	public Color Color = Color.White;

	public int Width { get { return Region.SourceRectangle.Width; } set { Region.SourceRectangle.Width = value; } }
	public int Height { get { return Region.SourceRectangle.Height; } set { Region.SourceRectangle.Height = value; } }

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

	public void Draw()
	{
		IaraGame.SpriteBatch.Draw(Region.Texture, Transform.GlobalPosition, Region.SourceRectangle, Color, Transform.Rotation, Origin, Transform.Scale, Flip, LayerDepth);
	}
}
