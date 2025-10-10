using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AwesomeRPG.Characters;
using AwesomeRPG.Commands;
using AwesomeRPG.Controllers;
using AwesomeRPG.Sprites;
using AwesomeRPG.Map;

namespace AwesomeRPG;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Dictionary<string,ISprite> _spriteDict = [];
    public List<ICharacter> _characterSet = [];
    public int currentEnemy;
    private int _chosenSprite = 0;
    private List<IController> _controllersList = [];
    public Tilemap Tilemap;
    public List<int> Tiles;
    public Player Player { get; private set; }

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
    /// <param name="spriteName">The name of the sprite in the dictionary</param> 
    /// <param name="newSprite">The ISprite of the new sprite being given</param> 
    public void ChangeGameSpriteToNewSprite(string spriteName, ISprite newSprite)
    {
        _spriteDict[spriteName] = newSprite;
    }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //Can change title as we see fit
        Window.Title = "AwesomeRPG";

        _graphics.PreferredBackBufferWidth = 1024;
        _graphics.PreferredBackBufferHeight = 768;
        //See Game.TargetElapsedTime if we'd like to change refresh rate
    }

    protected override void Initialize()
    {
        _controllersList.Add(new MouseController(this, SetChosenSprite));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        //Create sprite factories
        MapItemSpriteFactory.LoadAllTextures(Content, _spriteBatch);
        ItemSpriteFactory.LoadAllTextures(Content, _spriteBatch);

        //Create item(Probably could be improved, _spriteDict might not be needed anymore)
        _spriteDict.Add("item", MapItemSpriteFactory.CreatePotionSprite());

        //World Creation
        Tilemap = Tilemap.FromFile(Content, "TileImages\\test_tiles_definition.xml");

        //TEMP should be removed in the future, player shouldn't know about sprites
        //Because KeyboardController needs player and Player needs sprite its here
        Player = new Player(Content,_spriteBatch);
        CharacterSpriteFactory.Instance.LoadAllTextures(Content, _spriteBatch);
        _controllersList.Add(new KeyboardController(this));

        //NPC creation
        _characterSet.Add(new CharacterEnemyMoblin(new Vector2(300, 350), Util.Cardinal.up));
        _characterSet.Add(new CharacterEnemyArmos(new Vector2(300, 350), Util.Cardinal.down));
        _characterSet.Add(new CharacterEnemyLynel(new Vector2(300, 350), Util.Cardinal.right));
        _characterSet.Add(new CharacterKris());
        currentEnemy = 0;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (IController controller in _controllersList) {
            controller.Update();
        }

        Player.Update(gameTime);

        _characterSet[currentEnemy].Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        Tilemap.Draw(_spriteBatch);
        Player.Draw(gameTime);
        _characterSet[currentEnemy].Draw(gameTime);

        foreach (ISprite sprite in _spriteDict.Values)
        {
            sprite.Draw(gameTime, new Vector2(500, 200));
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
