using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace IaraEngine;

public class SpriteText
{
	public Transform Transform;

	public Vector2 Origin = Vector2.Zero;
	public Color Color = Color.White;
	public float LayerDepth = 0f;
	public SpriteEffects Flip = SpriteEffects.None;

	public string Text;
	public Font Font;

	public int TextSize => Text.Length * Font.Size;

	public SpriteText()
	{
		Transform = new();
		Font = new();
	}

	public SpriteText(string text, SpriteFont spriteFont, int size)
	{
		Transform = new();
		Font = new(spriteFont, size);
		Text = text;
	}

	public SpriteText(string text, Font font)
	{
		Transform = new();
		Font = font;
		Text = text;
	}

	public void CenterOrigin()
	{
		Origin.X = TextSize * .5f;
		Origin.Y = Font.Size * .5f;
	}

	public void Draw()
	{
		IaraGame.SpriteBatch.DrawString(Font.SpriteFont, Text, Transform.Position, Color, Transform.Rotation, Origin, Transform.Scale, Flip, LayerDepth);
	}

}
