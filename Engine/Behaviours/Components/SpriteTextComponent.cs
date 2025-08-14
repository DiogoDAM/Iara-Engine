using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public sealed class SpriteTextComponent : Component 
{
	public SpriteText SpriteText;

	public SpriteTextComponent()
	{
	}

	public void CreateSprite(Font font, string text, Transform parent)
	{
		SpriteText = new(font, text, parent);
	}

	public void CenterOrigin()
	{
		SpriteText.CenterOrigin();
	}

	public Transform GetTransform()
	{
		return SpriteText.Transform;
	}

	public void FlipH()
	{
		SpriteText.Flip = (SpriteText.Flip == SpriteEffects.None) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
	}

	public void FlipV()
	{
		SpriteText.Flip = (SpriteText.Flip == SpriteEffects.None) ? SpriteEffects.FlipVertically : SpriteEffects.None;
	}

	public override void Draw()
	{
		SpriteText.Draw();
	}
}
