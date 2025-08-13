using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace IaraEngine;

public class TextureAtlas : ITextureRegion
{
	private Dictionary<string, TextureRegion> _regions;

	public Texture2D Texture { get; set; }
	public Rectangle SourceRectangle { get { return Texture.Bounds; } }

	public TextureAtlas(Texture2D texture)
	{
		Texture = texture;
		_regions = new();
	}

	public void AddRegion(string name, int x, int y, int w, int h)
	{
		_regions.Add(name, new TextureRegion(Texture, x, y, w, h));
	}


	public TextureRegion GetRegion(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.GetRegion() atlas don't have the key: {name} for TextureRegion");

		return _regions[name];
	}

	public Sprite CreateSprite(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.CreateSprite() atlas don't have the key: {name}");

		return new Sprite(_regions[name]);
	}

	public AnimatedSprite CreateAnimatedSprite(string name, int sourcewidth, int sourceheight, bool isLoop=false, float defaultFramesDuration=0.1f)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.CreateAnimatedSprite() atlas don't have the key: {name} to a TextureRegion");

		return new AnimatedSprite(
				_regions[name],
				_regions[name].SourceRectangle.X,
				_regions[name].SourceRectangle.Y,
				sourcewidth,
				sourceheight,
				_regions[name].SourceRectangle.Width,
				_regions[name].SourceRectangle.Height,
				isLoop,
				defaultFramesDuration);
	}

	public AnimatedSprite CreateAnimatedSprite(string name, int sourcewidth, int sourceheight, bool isLoop=false, params float[] framesDuration)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"IaraEngine :: TextureAtlas.CreateAnimatedSprite() atlas don't have the key: {name} to a TextureRegion");

		return new AnimatedSprite(
				_regions[name],
				_regions[name].SourceRectangle.X,
				_regions[name].SourceRectangle.Y,
				sourcewidth,
				sourceheight,
				_regions[name].SourceRectangle.Width,
				_regions[name].SourceRectangle.Height,
				isLoop,
				framesDuration);
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

		return atlas;
	}
}
