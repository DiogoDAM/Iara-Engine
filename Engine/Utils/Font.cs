using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class Font
{
	public int Size;
	public SpriteFont SpriteFont;

	public Font() { }

	public Font(SpriteFont font, int size)
	{
		SpriteFont = font;
		Size = size;
	}
}
