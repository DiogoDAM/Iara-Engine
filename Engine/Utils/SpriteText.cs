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

	public Vector2 TextSize => Font.SpriteFont.MeasureString(Text);

	public SpriteText()
	{
		Transform = new();
		Font = new();
	}

	public SpriteText(SpriteFont spriteFont, int size, string text)
	{
		Transform = new();
		Font = new(spriteFont, size);
		Text = text;
	}

	public SpriteText(Font font, string text)
	{
		Transform = new();
		Font = font;
		Text = text;
	}

	public SpriteText(Font font, string text, Transform parent)
	{
		Transform = new();
		Transform.Parent = parent;
		Font = font;
		Text = text;
	}

	public void CenterOrigin()
	{
		Origin.X = TextSize.X * .5f;
		Origin.Y = TextSize.Y * .5f;
	}

	public void Draw()
	{
		IaraGame.SpriteBatch.DrawString(Font.SpriteFont, Text, Transform.GlobalPosition, Color, Transform.GlobalRotation, Origin, Transform.GlobalScale, Flip, LayerDepth);
	}

}
