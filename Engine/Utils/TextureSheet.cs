using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IaraEngine;

public class TextureSheet : ITextureRegion
{
	public Texture2D Texture { get; set; }

	private Rectangle[] _sources;

	public int Width;
	public int Height;

	public int Size { get; private set; }

	public TextureSheet() { }

	public TextureSheet(Texture2D texture, int x, int y, int swidth, int sheight, int width, int height)
	{
		Texture = texture;

		Width = swidth;
		Height = sheight;

		int rows = height / sheight;
		int columns = width / swidth;

		Size = rows * columns;

		_sources = new Rectangle[Size];

		for(int row=0; row<rows; row++)
		{
			for(int column=0; column<columns; column++)
			{
				_sources[row * columns + column] = new Rectangle(x + (Width * column), y + (Height * row), Width, Height);
			}
		}
	}

	public TextureRegion GetRegion(int index)
	{
		if(index < 0 || index >= Size) throw new IndexOutOfRangeException("IaraEngine :: TextureSheet.GetRegion() index is out of bounds");

		TextureRegion region = new(Texture, _sources[index]);
		return region;
	}

	public TextureRegion[] GetRegions(params int[] indexes)
	{
		TextureRegion[] regions = new TextureRegion[indexes.Length];

		for(int i=0; i<indexes.Length; i++)
		{
			int index = indexes[i];
			if(index < 0 || index >= Size) throw new IndexOutOfRangeException("IaraEngine :: TextureSheet.GetRegions() an index is out of bounds");

			regions[i] = new TextureRegion(Texture, _sources[index]);
		}

		return regions;
	}
}
