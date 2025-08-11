using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class TextureRegion
{
	public Rectangle SourceRectangle;
	public Texture2D Texture;

	public TextureRegion() { }

	public TextureRegion(Texture2D texture, int x, int y, int width, int height)
	{
		Texture = texture;
		SourceRectangle = new Rectangle(x, y, width, height);
	}

	public TextureRegion(Texture2D texture, Rectangle rect)
	{
		Texture = texture;
		SourceRectangle = rect;
	}

	public void Draw(Vector2 pos, Color color)
	{
		IaraGame.SpriteBatch.Draw(Texture, pos, SourceRectangle, color);
	}
}
