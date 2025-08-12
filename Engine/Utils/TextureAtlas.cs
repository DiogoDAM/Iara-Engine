using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace IaraEngine;

public class TextureAtlas : ITextureRegion
{
	private Dictionary<string, TextureRegion> _regions;
	private Dictionary<string, TextureSheet> _sheets;

	public Texture2D Texture { get; set; }

	public TextureAtlas(Texture2D texture)
	{
		Texture = texture;
		_regions = new();
		_sheets = new();
	}

	public void AddRegion(string name, int x, int y, int w, int h)
	{
		_regions.Add(name, new TextureRegion(Texture, x, y, w, h));
	}

	public void AddSheet(string name, int x, int y, int sourceWidth, int sourceHeight, int totalWidth, int totalHeight)
	{
		_sheets.Add(name, new TextureSheet(Texture, x, y, sourceWidth, sourceHeight, totalWidth, totalHeight));
	}

	public TextureRegion GetRegion(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.GetRegion() atlas don't have the key: {name} for TextureRegion");

		return _regions[name];
	}

	public TextureSheet GetSheet(string name)
	{
		if(!_sheets.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.GetSheet() atlas don't have the key: {name} for a TextureSheet");

		return _sheets[name];
	}

	public Sprite CreateSprite(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.CreateSprite() atlas don't have the key: {name}");

		return new Sprite(_regions[name]);
	}

	public static TextureAtlas CreateFromFile(ContentManager content, string filename)
	{
		TextureAtlas atlas;

		string filepath = Path.Combine(content.RootDirectory, filename);

		XDocument doc = XDocument.Load(filepath);
		XElement root = doc.Root;

		string texturePath = root.Element("Texture").Value;
		atlas = new(content.Load<Texture2D>(texturePath));

		var regions = root.Element("Regions").Elements("Region");

		if(regions != null)
		{
			foreach(var region in regions)
			{
				atlas.AddRegion(
						region.Attribute("name").Value,
						int.Parse(region.Attribute("x")?.Value ?? "0"),
						int.Parse(region.Attribute("y")? .Value ?? "0"),
						int.Parse(region.Attribute("width")?.Value ?? "0"),
						int.Parse(region.Attribute("height")?.Value ?? "0")
						);
			}
		}

		var spriteSheets = root.Element("TextureSheets").Elements("TextureSheet");

		if(spriteSheets != null)
		{
			foreach(var spriteSheet in spriteSheets)
			{
				atlas.AddSheet(
						spriteSheet.Attribute("name")?.Value,
						int.Parse(spriteSheet.Attribute("x")?.Value ?? "0"),
						int.Parse(spriteSheet.Attribute("y")?.Value ?? "0"),
						int.Parse(spriteSheet.Attribute("source-width")?.Value ?? "0"),
						int.Parse(spriteSheet.Attribute("source-height")?.Value ?? "0"),
						int.Parse(spriteSheet.Attribute("total-width")?.Value ?? "0"),
						int.Parse(spriteSheet.Attribute("total-height")?.Value ?? "0")
						);
			}
		}

		return atlas;
	}
}
