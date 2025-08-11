using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace IaraEngine;

public class IaraGame : Game 
{
	private static IaraGame s_Instance;
	public static IaraGame Instance;

	public new static GraphicsDevice GraphicsDevice;
	public static SpriteBatch SpriteBatch;
	public new static ContentManager Content;
	public static GraphicsDeviceManager Graphics;

	public static float DeltaTime { get; private set; }

	public string Title => Window.Title;
	public int WindowWidth => Graphics.PreferredBackBufferWidth;
	public int WindowHeight => Graphics.PreferredBackBufferHeight;
	public bool IsFullscreen => Graphics.IsFullScreen;

	public static Input Input { get; private set; }

	public static Font DefaultFont;

	public IaraGame(string title, int ww, int wh, bool isFullscreen=false)
	{
		s_Instance = this;

		Graphics = new(this);

		Content = base.Content;

		Input = new();

		Content.RootDirectory = "Content";

		Window.Title = title;
		ResizeWindow(ww, wh, isFullscreen);

		IsMouseVisible = true;
	}

	protected override void Initialize()
	{
		base.Initialize();

		GraphicsDevice = base.GraphicsDevice;
		SpriteBatch = new SpriteBatch(GraphicsDevice);
	}

	protected override void Update(GameTime gameTime)
	{
		DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

		Input.Update();

		base.Update(gameTime);
	}

	public void ResizeWindow(int ww, int wh, bool isFullsreen)
	{
		Graphics.PreferredBackBufferWidth = ww;
		Graphics.PreferredBackBufferHeight = wh;
		Graphics.IsFullScreen = isFullsreen;
		Graphics.ApplyChanges();
	}
}
