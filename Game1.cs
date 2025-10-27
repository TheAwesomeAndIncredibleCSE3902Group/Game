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
using AwesomeRPG.Collision;

namespace AwesomeRPG;

public class Game1 : Game
{
    //Monogame required
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    //Controls Variables
    private List<IController> _controllersList = [];

    //Object Variables
    private Dictionary<string,ISprite> _spriteDict = [];
    private int _chosenSprite = 0;
    public Player Player { get; private set; }

    //Collision Variables
    private List<CollisionObject> _movingCollisionObjects = new();
    public List<CollisionObject> NonMovingCollisionObjects { get; set; } = new();
    private AllCollisionHandler _allCollisionHandler;

    //Map Variables
    public RoomAtlas RoomAtlas { get; private set; }
    public RoomMap RoomMap { get; set; }
    public List<int> Tiles { get; set; }
    

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
        _allCollisionHandler = new AllCollisionHandler();
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


        //NPC creation
        CharacterSpriteFactory.Instance.LoadAllTextures(Content, _spriteBatch);

        //World Creation
        RoomMap = MapParser.Instance.RoomMapFromXML(Content, "MapItems\\Level0-0.xml", new Vector2(3, 3));
        RoomAtlas = new RoomAtlas(new AtlasInitializer().InitializeAtlasWStartingRoom(Content,RoomMap));
        _controllersList.Add(new MouseController(this, RoomAtlas));
        NonMovingCollisionObjects = RoomMap._nonMovingCollisionObjects;

        //Player declaration
        //TODO: PROBABLY WANNA HAVE A METHOD IN EACH LEVEL WHICH HANDLES ADDING THINGS TO COLLISION LIST
        Player = new Player(Content,_spriteBatch);
        _movingCollisionObjects.Add(Player);
        _controllersList.Add(new KeyboardController(this));

    }

    private void HandleCollisions()
    {
        // This is solely detecting player collisions with everything because the
        // movingCollisionObjects list has only the player and nothing else added.
        // Might be good to separate the player out into it's own collision object
        // to simplify the interactions between the player and everything not just
        // for interactability with the world but also for battle mechanics with
        // turn order and any AoE damage on both sides.
        for (int i = 0; i< _movingCollisionObjects.Count; i++)
        {
            foreach (CollisionObject nonMovingObject in NonMovingCollisionObjects)
            {
                CollisionInfo collision = _movingCollisionObjects[i].DetectCollision(nonMovingObject);
                _allCollisionHandler.HandleCollision(collision);
            }
            for (int j = i+1; j < _movingCollisionObjects.Count; j++)
            {
                CollisionInfo collision = _movingCollisionObjects[i].DetectCollision(_movingCollisionObjects[j]);
                _allCollisionHandler.HandleCollision(collision);
            }
        }
    }
    protected override void Update(GameTime gameTime)
    {
        //Time can be slowed like this
        //gameTime = new GameTime(gameTime.TotalGameTime / 2f, gameTime.ElapsedGameTime / 2f);
        
        foreach (IController controller in _controllersList) {
            controller.Update();
        }

        Player.Update(gameTime);

        RoomMap.Update(gameTime);
        HandleCollisions();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        RoomMap.Draw(_spriteBatch, gameTime);
        Player.Draw(gameTime);

        foreach (ISprite sprite in _spriteDict.Values)
        {
            sprite.Draw(gameTime, new Vector2(500, 200));
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    /// <summary>
    /// This ain't do nothin rn
    /// </summary>
    public static void TransitionToBattleState()
    {
        Debug.WriteLine("Battle State moment");
    }

}
