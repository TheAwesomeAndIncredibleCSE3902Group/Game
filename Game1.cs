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
    private SpriteFont _spriteFont;
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

        _spriteFont = Content.Load<SpriteFont>("Fonts/MyFont");
        _spriteList.Add(new TextSprite(
            _spriteBatch,
            _spriteFont,
            "Credits:\nProgram made by: Eli Lambrych\nSprites provided by contributors of The Spriters Resource:\nhttps://www.spriters-resource.com/pc_computer/deltarune/asset/110448/\n(Note: I created my own spritesheet from the SR spritesheet)\nArtwork by Toby Fox, Temmie Chang, and DELTARUNE team",
            20, 20, Color.White
        ));

        Texture2D spriteTexture = Content.Load<Texture2D>("SpriteImages/kris_custom");
        _spriteList.Add(new MovingAnimatedSprite(_spriteBatch, spriteTexture, GetChosenSprite));
        _spriteList.Add(new NonMovingAnimatedSprite(_spriteBatch, spriteTexture, GetChosenSprite));
        _spriteList.Add(new NonMovingNonAnimatedSprite(_spriteBatch, spriteTexture, GetChosenSprite));
        _spriteList.Add(new MovingNonAnimatedSprite(_spriteBatch, spriteTexture, GetChosenSprite));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        foreach (IController controller in _controllersList) {
            controller.Update();
        }

        foreach (ISprite currentSprite in _spriteList)
        {
            currentSprite.Update(gameTime);
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
            currentSprite.Draw(gameTime);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
