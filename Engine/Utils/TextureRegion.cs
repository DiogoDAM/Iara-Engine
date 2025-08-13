using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class TextureRegion : ITextureRegion
{
	public Texture2D Texture { get; set; }
	public Rectangle SourceRectangle { get; set; }

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
