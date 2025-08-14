using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public sealed class SpriteComponent : Component
{
	public Sprite Sprite;

	public SpriteComponent() : base() 
	{
	}

	public void CreateSprite(TextureRegion region, Transform parent)
	{
		Sprite = new(region);
		Sprite.Transform.Parent = parent;
	}

	public Transform GetTransform()
	{
		return Sprite.Transform;
	}

	public void FlipH()
	{
		Sprite.Flip = (Sprite.Flip == SpriteEffects.None) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
	}

	public void FlipV()
	{
		Sprite.Flip = (Sprite.Flip == SpriteEffects.None) ? SpriteEffects.FlipVertically : SpriteEffects.None;
	}

	public override void Draw()
	{
		Sprite.Draw();
	}
}
