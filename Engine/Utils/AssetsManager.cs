using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace IaraEngine;

public class AssetsManager
{
	private Dictionary<string, ITextureRegion> _textures;

	public AssetsManager()
	{
		_textures = new();
	}

	public void CreateTexture(string textureName, ITextureRegion texture)
	{
		if(texture == null) throw new ArgumentNullException("IaraEngine :: AssetsManager.CreateTexture<T>() texture is null");
		if(String.IsNullOrEmpty(textureName)) throw new ArgumentNullException("IaraEngine :: AssetsManager.CreateTexture<T>() textureName is null or empty");

		_textures.Add(textureName, texture);
	}

	public T LoadTexture<T>(string textureName) where T : ITextureRegion
	{
		if(String.IsNullOrEmpty(textureName)) throw new ArgumentNullException("IaraEngine :: AssetsManager.LoadTexture<T>() textureName is null or empty");

		if(!_textures.ContainsKey(textureName)) throw new KeyNotFoundException($"IaraEngine :: AssetsMananger.LoadTexture<T>() don't have textureName: {textureName}");

		return (T)_textures[textureName];
	}
}
