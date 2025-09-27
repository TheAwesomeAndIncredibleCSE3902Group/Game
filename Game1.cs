using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Controllers;
using Sprint0.Sprites;

namespace Sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<ISprite> _spriteList = [];
    private int _chosenSprite = 0;
    private List<IController> _controllersList = [];
    public void SetChosenSprite(int val)
    {
        _chosenSprite = val;
    }
    public int GetChosenSprite()
    {
        return _chosenSprite;
    }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _controllersList.Add(new KeyboardController());
        _controllersList.Add(new MouseController(this, SetChosenSprite));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        Texture2D spriteTexture = Content.Load<Texture2D>("SpriteImages/kris_custom");

        // _spriteList.Add(new AnimatableSprite(_spriteBatch, spriteTexture, new Rectangle(0, 0, 24, 24)));
        _spriteList.Add(new AnimatableSprite(
            _spriteBatch,
            spriteTexture,
            new int[,] {
                { 10, 10, 15, 15, 20, 10 },
                { 25, 25, 13, 24, 10, 200}
            },
            1000
        ));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        foreach (IController controller in _controllersList) {
            controller.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        foreach (ISprite currentSprite in _spriteList)
        {
            currentSprite.Draw(gameTime, new Vector2(10, 10));
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
