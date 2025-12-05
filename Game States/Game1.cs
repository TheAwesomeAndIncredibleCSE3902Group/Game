using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Controllers;
using AwesomeRPG.Sprites;
using AwesomeRPG.UI;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;

namespace AwesomeRPG;

public enum GameState { start, overworld, battle, gameover, win}
public class Game1 : Game
{
    public static IGameState StateClass { get; private set; }
    
    //Monogame required
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //Controls Variables
    private List<IController> _controllersList = new();

    public RootElement RootUIElement;
    public SpriteFont DefaultSpriteFont;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //Can change title as we see fit
        Window.Title = "AwesomeRPG";

        _graphics.PreferredBackBufferWidth = 1024;
        _graphics.PreferredBackBufferHeight = 768;
        Util.ScreenRect = new Rectangle
        (
            0,
            0,
            _graphics.PreferredBackBufferWidth,
            _graphics.PreferredBackBufferHeight
        );
        //See Game.TargetElapsedTime if we'd like to change refresh rate
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //Create sprite factories; load textures
        MapItemSpriteFactory.LoadAllTextures(Content, _spriteBatch);
        ProjectileSpriteFactory.LoadAllTextures(Content, _spriteBatch);
        CharacterSpriteFactory.Instance.LoadAllTextures(Content, _spriteBatch);

        //Create sound factories; load sound effects
        GameSoundFactory.LoadAndSetUpAllThemes(Content);
        PlayerSoundFactory.LoadAndSetUpAllPlayerSounds(Content);
        ItemSoundFactory.LoadAndSetUpAllItemSounds(Content);
        EnemySoundFactory.LoadAndSetUpAllEnemySounds(Content);
        
        DefaultSpriteFont = Content.Load<SpriteFont>("Fonts\\MyFont");

        StateClass = new StartScreenState(this);
        //InitializeOverworldAndControllers();
    }

    public void InitializeOverworldAndControllers()
    {
        //Player must be declared before the Overworld
        PlayerOverworld pOverworld = new PlayerOverworld(Content, _spriteBatch);
        PlayerStats pStats = new PlayerStats
        (
            maxHealth: 50, specialPointCount: 5,
            speed: 5, attack: 10, defense: 5,
            weaponUse: 5, specialAttack: 5, specialDefense: 5, luck: 100
        );
        new Player(pStats, pOverworld);

        StateClass = new OverworldState(Content, Player.Instance.PlayerOverworld, this);

        _controllersList =
        [
            new KeyboardController(this),
            new KeyboardUIController(this),
            new MouseController(this),
        ];
    }

    public void Reset()
    {
        InitializeOverworldAndControllers();
    }

    protected override void Update(GameTime gameTime)
    {
        //Time can be slowed like this
        //gameTime = new GameTime(gameTime.TotalGameTime / 2f, gameTime.ElapsedGameTime / 2f);

        foreach (IController controller in _controllersList)
            controller.Update(StateClass.CurrentState);

        StateClass.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGreen);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        StateClass.Draw(_spriteBatch, gameTime);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    /// <summary>
    /// TODO: change to StateClass = StateClass.ToBattleState();
    /// </summary>
    public static void TransitionToBattleState(CharacterEnemyBase enemy)
    {
        StateClass.ChangeToBattleState(enemy);
    }

    public static void TransitionToOverworldState()
    {
        StateClass.ChangeToOverworldState();
    }

    public static void TransitionToGameOverState()
    {
        StateClass.ChangeToGameOverState();
    }

    public static void TransitionToWinState()
    {
        StateClass.ChangeToWinState();
    }
    
    /// <summary>
    /// This should ONLY be run by the States themselves
    /// </summary>
    /// <param name="newState"></param>
    public void SetStateClass(IGameState newState)
    {
        StateClass = newState;
    }

}
