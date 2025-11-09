using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Controllers;
using AwesomeRPG.Sprites;
using AwesomeRPG.Map;
using AwesomeRPG.Collision;
using AwesomeRPG.UI.Elements;
using AwesomeRPG.UI.Components;
using AwesomeRPG.UI;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG;

public class Game1 : Game
{
    public enum GameState { overworld, battle }
    public IGameState gameState { get; private set; }
    
    //Monogame required
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    //Controls Variables
    private List<IController> _controllersList = [];

    public Player Player { get; private set; }
    
    // Temporarily commented out for Sprint3 submission
    public RootElement RootUIElement;

    //Collision Variables, this needs to be improved sloppy solution for now
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
        ProjectileSpriteFactory.LoadAllTextures(Content, _spriteBatch);

        //NPC creation
        CharacterSpriteFactory.Instance.LoadAllTextures(Content, _spriteBatch);

        //World Creation
        RoomAtlas.Instance.SetAtlas(new AtlasInitializer().InitializeAtlas(Content));
        RoomAtlas.Instance.CurrentRoom = RoomAtlas.Instance.GetRoom(0,0);

        //Player declaration
        //TODO: PROBABLY WANNA HAVE A METHOD IN EACH LEVEL WHICH HANDLES ADDING THINGS TO COLLISION LIST
        Player = new Player(Content, _spriteBatch);
        RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Add(Player);
        _controllersList.Add(new KeyboardController(this));
        _controllersList.Add(new KeyboardUIController(this));
        _controllersList.Add(new MouseController(this));

        // Temporarily commented out for Sprint3 submission

        // UI creation! This will eventually be moved to one of the battle state classes.
        var spriteFont = Content.Load<SpriteFont>("Fonts\\MyFont");
        RootUIElement = new RootElement(_spriteBatch);

        var battleUiBoardBorder = new RectElement(RootUIElement, new Color(40, 0, 40));
        battleUiBoardBorder.OffsetAndSize = new Rectangle(8, 528, 1008, 234);

        var battleUiBoardBg = new RectElement(RootUIElement, new Color(80, 0, 80));
        battleUiBoardBg.OffsetAndSize = new Rectangle(10, 530, 1004, 230);

        RootUIElement.AddChild(battleUiBoardBorder);
        RootUIElement.AddChild(battleUiBoardBg);

        List<CommandElement> buttons = new List<CommandElement>();
        for (int i = 0; i < 6; i++)
        {
            var currentButtonToAdd = ButtonComponent.Create(RootUIElement, spriteFont, this, new Rectangle(20 + (i / 3) * 365, 540 + (i % 3) * 75, 350, 60), Color.Purple, Color.White, "Action " + i);

            buttons.Add(currentButtonToAdd);
            RootUIElement.AddChild(currentButtonToAdd);
        }
        // buttons[5].IsVisible = false;

        RootUIElement.UIState.SelectionIndex = 0;

        RootUIElement.AddActionOnUIEvent(UIEvent.ButtonDown, (e) =>
        {
            var eventParams = (InputUIEventParams)e;
            // System.Console.WriteLine("This is a test!!");
            if (eventParams.Controls.Contains(UIControl.MoveDown))
            {
                RootUIElement.UIState.SelectionIndex += 1;
            }
            if (eventParams.Controls.Contains(UIControl.MoveUp))
            {
                RootUIElement.UIState.SelectionIndex -= 1;
            }
            if (eventParams.Controls.Contains(UIControl.MoveRight))
            {
                RootUIElement.UIState.SelectionIndex += 3;
            }
            if (eventParams.Controls.Contains(UIControl.MoveLeft))
            {
                RootUIElement.UIState.SelectionIndex -= 3;
            }
        });
        
    }

    private void HandleCollisions()
    {
        for (int i = 0; i< RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Count; i++)
        {
            foreach (CollisionObject nonMovingObject in RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects)
            {
                CollisionInfo collision = RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[i].DetectCollision(nonMovingObject);
                _allCollisionHandler.HandleCollision(collision);
            }

            for (int j = i+1; j < RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Count; j++)
            {
                CollisionInfo collision = RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[i].DetectCollision(RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[j]);
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
            for(int i = 0; i < RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Count; i++)
            {
                if(RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[i].ObjectType == CollisionObjectType.PlayerProjectile)
                {
                    RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.RemoveAt(i);
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
            for (int i = 0; i < RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects.Count; i++)
            {
                if (RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects[i].ObjectType == CollisionObjectType.Pickup)
                {
                    RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects.RemoveAt(i);
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
            controller.Update(GameState.overworld);
        }

        Player.Update(gameTime);

        RoomAtlas.Instance.CurrentRoom.Update(gameTime);
        HandleCollisions();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGreen);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        {       //Will be taken by OverworldState
            RoomAtlas.Instance.CurrentRoom.Draw(_spriteBatch, gameTime);
            Player.Draw(gameTime);

        RootUIElement.Draw(gameTime);

            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }

    /// <summary>
    /// This ain't do nothin rn, fix to interact with game state machine once it's ready
    /// </summary>
    public static void TransitionToBattleState()
    {
        // Debug.WriteLine("Battle State moment");
    }

}
