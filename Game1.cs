using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Characters;
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

    public Player Player { get; private set; }
    
    // Temporarily commented out for Sprint3 submission
    // public RootElement RootUIElement;

    //Collision Variables, this needs to be improved sloppy solution for now
    public static List<CollisionObject> MovingCollisionObjects { get; private set; } = new();
    public List<CollisionObject> NonMovingCollisionObjects { get; private set; } = new();
    private AllCollisionHandler _allCollisionHandler;

    //Map Variables
    public List<int> Tiles { get; set; }

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

        //NPC creation
        CharacterSpriteFactory.Instance.LoadAllTextures(Content, _spriteBatch);

        //World Creation
        MapParser.Instance.LoadParser(this, RoomAtlas.Instance);
        RoomAtlas.Instance.CurrentRoom = MapParser.Instance.RoomMapFromXML(Content, "MapItems\\Level0-0.xml", new Vector2(3, 3));
        RoomAtlas.Instance.SetAtlas(new AtlasInitializer().InitializeAtlas(Content));
        NonMovingCollisionObjects = RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects;
        MovingCollisionObjects = RoomAtlas.Instance.CurrentRoom._movingCollisionObjects;

        //Player declaration
        //TODO: PROBABLY WANNA HAVE A METHOD IN EACH LEVEL WHICH HANDLES ADDING THINGS TO COLLISION LIST
        Player = new Player(Content, _spriteBatch);
        MovingCollisionObjects.Add(Player);
        _controllersList.Add(new KeyboardController(this));
        _controllersList.Add(new MouseController(this));


        TestEnemyCollision();

        // Temporarily commented out for Sprint3 submission

        // UI creation
        // var spriteFont = Content.Load<SpriteFont>("Fonts\\MyFont");
        // RootUIElement = new RootElement(_spriteBatch);
        // RootUIElement.AddChild(ButtonComponent.Create(RootUIElement, spriteFont, this, new Rectangle(400, 50, 300,100)));

        // RootUIElement.UIState.AddActionOnUIControlEvent(UIControl.MoveDown, UIControlEvent.ButtonPress, () =>
        // {
        //     RootUIElement.UIState.SelectionIndex += 1;
        // });
        // RootUIElement.UIState.AddActionOnUIControlEvent(UIControl.MoveUp, UIControlEvent.ButtonPress, () =>
        // {
        //     RootUIElement.UIState.SelectionIndex -= 1;
        // });
    }
    
    private void TestEnemyCollision()
    {
        LinePathing pathing = new LinePathing(Util.Cardinal.up);
        CharacterEnemyArmos enemy = new CharacterEnemyArmos(new Vector2(100, 150), Util.Cardinal.up);
        enemy.Pathing = pathing;
        RoomAtlas.Instance.CurrentRoom.Characters.Add(enemy);

        //RoomMap._movingCollisionObjects.Add(enemy);
        MovingCollisionObjects.Add(enemy);
    }

    private void HandleCollisions()
    {
        // This is solely detecting player collisions with everything because the
        // movingCollisionObjects list has only the player and nothing else added.
        // Might be good to separate the player out into it's own collision object
        // to simplify the interactions between the player and everything not just
        // for interactability with the world but also for battle mechanics with
        // turn order and any AoE damage on both sides.
        for (int i = 0; i< MovingCollisionObjects.Count; i++)
        {
            foreach (CollisionObject nonMovingObject in NonMovingCollisionObjects)
            {
                CollisionInfo collision = MovingCollisionObjects[i].DetectCollision(nonMovingObject);
                _allCollisionHandler.HandleCollision(collision);
            }

            for (int j = i+1; j < MovingCollisionObjects.Count; j++)
            {
                CollisionInfo collision = MovingCollisionObjects[i].DetectCollision(MovingCollisionObjects[j]);
                _allCollisionHandler.HandleCollision(collision);
            }
        }
        ClearProjectiles();
        ClearPickups();
    }

    //Vile code, made by the most deprived of man. May this be fixed next sprint
    private void ClearProjectiles()
    {
        if(Player.spawnedProjectiles.Count == 0)
        {
            for(int i = 0; i < MovingCollisionObjects.Count; i++)
            {
                if(MovingCollisionObjects[i].ObjectType == CollisionObjectType.PlayerProjectile)
                {
                    MovingCollisionObjects.RemoveAt(i);
                }
            }
        }
    }


    //Vile code, made by the most deprived of man. May this be fixed next sprint
    private int prevPickups = 2;
    private void ClearPickups()
    {
        if (prevPickups != 0 && RoomAtlas.Instance.CurrentRoom.Pickups.Count != prevPickups)
        {
            for (int i = 0; i < NonMovingCollisionObjects.Count; i++)
            {
                if (NonMovingCollisionObjects[i].ObjectType == CollisionObjectType.Pickup)
                {
                    NonMovingCollisionObjects.RemoveAt(i);
                }
            }
            prevPickups--;
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

        RoomAtlas.Instance.CurrentRoom.Update(gameTime);
        HandleCollisions();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        RoomAtlas.Instance.CurrentRoom.Draw(_spriteBatch, gameTime);
        Player.Draw(gameTime);

        // Temporarily commented out for Sprint3 submission
        // RootUIElement.Draw(gameTime);

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
