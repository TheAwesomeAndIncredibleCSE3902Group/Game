using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;
using Sprint0.Controllers;
using Sprint0.Sprites;
using Sprint0.Tiles;

namespace Sprint0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Dictionary<string,ISprite> _spriteDict = [];
    private int _chosenSprite = 0;
    private List<IController> _controllersList = [];
    private Tilemap _tilemap;
    private IPlayer player;

    public void SetChosenSprite(int val)
    {
        _chosenSprite = val;
    }
    public int GetChosenSprite()
    {
        return _chosenSprite;
    }

    /// <summary>
    /// Changes the sprite in spritelist of a given name with a new sprite
    /// </summary>
    /// <param name="spriteName"></param> The name of the sprite in the dictionary
    /// <param name="newSprite"></param> The ISprite of the new sprite being given
    public void ChangeGameSpriteToNewSprite(string spriteName, ISprite newSprite)
    {
        _spriteDict[spriteName] = newSprite;
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
        _controllersList.Add(new KeyboardController(this));
        _controllersList.Add(new MouseController(this, SetChosenSprite));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D spriteTexture = Content.Load<Texture2D>("SpriteImages/kris_custom");
        OverworldItemSpriteFactory.Instance.LoadAllTextures(Content, _spriteBatch);
        // _spriteList.Add(new AnimatableSprite(_spriteBatch, spriteTexture, new Rectangle(0, 0, 24, 24)));
        _spriteDict.Add(
            "kris",new AnimatableSprite(
            _spriteBatch,
            spriteTexture,
            new int[,] {
                { 10, 10, 15, 15, 20, 10 },
                { 25, 25, 13, 24, 10, 200}
            },
            1000));

        _spriteDict.Add("item", OverworldItemSpriteFactory.Instance.CreatePotionSprite());

        _tilemap = Tilemap.FromFile(Content, "TileImages\\test_tiles_definition.xml");

        ItemSpriteFactory.LoadAllTextures(Content, _spriteBatch);
        player = new Player();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        foreach (IController controller in _controllersList) {
            controller.Update();
        }

        player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        _tilemap.Draw(_spriteBatch);

        foreach (ISprite currentSprite in _spriteDict.Values)
        {
            currentSprite.Draw(gameTime, new Vector2(50, 50));
        }

        player.Draw(gameTime);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
