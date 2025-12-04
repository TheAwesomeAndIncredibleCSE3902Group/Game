using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Controllers;
using AwesomeRPG.Sprites;
using AwesomeRPG.UI.ElementFactories;
using AwesomeRPG.UI;
using AwesomeRPG.UI.Events;
using AwesomeRPG.Commands.BattleCommands;
using AwesomeRPG.Commands;
using AwesomeRPG.Map;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using System.Diagnostics;

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

        StateClass = new StartScreenState(this);
        //InitializeOverworldAndControllers();
        InitializeUI();
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

    // UI creation! This will eventually be moved to one of the battle state classes.
    private void InitializeUI()
    {
        var spriteFont = Content.Load<SpriteFont>("Fonts\\MyFont");
        RootUIElement = new RootElement(_spriteBatch);

        var rectBorderFactory = new RectElementFactory(RootUIElement);
        var battleUiBoardBorder = rectBorderFactory.CreateNew(new Color(40, 0, 40));
        battleUiBoardBorder.OffsetAndSize = new Rectangle(8, 528, 1008, 234);

        var rectBgFactory = new RectElementFactory(RootUIElement);
        var battleUiBoardBg = rectBgFactory.CreateNew(new Color(80, 0, 80));
        battleUiBoardBg.OffsetAndSize = new Rectangle(10, 530, 1004, 230);

        RootUIElement.AddChild(battleUiBoardBorder);
        RootUIElement.AddChild(battleUiBoardBg);

        var battleText = new TextElementFactory(RootUIElement);
        var battleTextElem = battleText.CreateNew(spriteFont, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur ut facilisis libero. Fusce nec eleifend turpis. Curabitur condimentum dapibus nisl. Ut metus sapien, auctor et justo non, condimentum gravida risus. Donec varius pellentesque felis non ultricies. Quisque fermentum, augue eu pellentesque dictum, ante sapien elementum enim, sed ultricies mauris dui ut lorem. Donec vitae semper enim, sed ornare libero.", Color.White);
        battleTextElem.OffsetAndSize = new Rectangle(20, 540, 984, 210);
        RootUIElement.AddChild(battleTextElem);

        List<Element> buttons = new List<Element>();
        for (int i = 0; i < 5; i++)
        {
            var buttonFactory = new ButtonElementFactory(RootUIElement);
            var currentButtonToAdd = buttonFactory.CreateNew(spriteFont, this, new Rectangle(20 + (i / 3) * 365, 540 + (i % 3) * 75, 350, 60), Color.Purple, Color.White, "Action " + i);
            // TODO: temporary edit for sprint 4 submission. Uncomment later!
            // currentButtonToAdd.AssociatedCommand = new RegularAttackBattleCommand(0);
            buttons.Add(currentButtonToAdd);
            RootUIElement.AddChild(currentButtonToAdd);
        }
        InitOverworldButton(spriteFont, buttons);
        // buttons[5].IsVisible = false;

        // TODO: Yes this code is very ass but it is temporary for sprint 4 submission!!!!!
        RootUIElement.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            if (((InputUIEventParams)e).Controls.Contains(UIControl.Interact)) {
                // If in battle and Interact button pressed.
                if (BattleScene.Instance.CurrentlyInBattle)
                {
                    int indexOfSelectedButton = buttons.IndexOf(RootUIElement.UIState.SelectedElement);
                    if (battleTextElem.IsVisible) {
                        // If we are on an enemy now, we make it do its action and update the text string.
                        if (BattleScene.Instance.CurrentBattle is IEnemyBattle)
                        {
                            ((IEnemyBattle)BattleScene.Instance.CurrentBattle).TakeTurn();
                            battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.TurnText;
                        }
                        else
                        {
                            // But if we are on a player now, 
                            // We will show the buttons again. and hide battle text
                            foreach (Element button in buttons)
                            {
                                button.IsVisible = true;
                                button.MakeSelectable();
                            }
                            RootUIElement.UIState.SelectionIndex = 0;
                            battleTextElem.IsVisible = false;
                        }
                        
                        BattleScene.Instance.NextTurn(); // Move on to the next enemy or the player.
                        
                    }
                    if (BattleScene.Instance.CurrentBattle is PlayerBattle)
                    {
                        if (indexOfSelectedButton < 5 && indexOfSelectedButton >= 0)
                        {
                            // We are pressing one of the buttons and are acting as player.
                            // For now we will attack lowest index alive enemy!
                            int idx = -1;
                            foreach (IEnemyBattle enemyBattle in BattleScene.Instance.EnemyList)
                            {
                                idx++;
                                if (!enemyBattle.IsFainted)
                                {
                                    break;
                                }
                            }
                            ((PlayerBattle)BattleScene.Instance.CurrentBattle).Attack(idx);

                            // Update battleText with turn text (player)
                            if (BattleScene.Instance.CurrentBattle.TurnText != null)
                            {
                                battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.TurnText;
                            }

                            BattleScene.Instance.NextTurn();

                            // Hide buttons and show battle text
                            foreach (Element button in buttons)
                            {
                                button.IsVisible = false;
                                button.MakeUnselectable();
                            }
                            RootUIElement.UIState.SelectionIndex = -1;
                            battleTextElem.IsVisible = true;
                        }
                    }
                } else
                {
                    StateClass.ChangeToOverworldState();
                }
            }
        });
        

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

    /// <summary>
    /// This is a little test helper to make a 6th button for returning to the overworld state
    /// </summary>
    private void InitOverworldButton(SpriteFont spriteFont, List<Element> buttons)
    {
        //This is the 6th button
        int i = 5;
        var buttonFactory = new ButtonElementFactory(RootUIElement);
        var currentButtonToAdd = buttonFactory.CreateNew(spriteFont, this, new Rectangle(20 + (i / 3) * 365, 540 + (i % 3) * 75, 350, 60), Color.Purple, Color.White, "Return to Overworld");
        // TODO: Set associated command via attributes
        currentButtonToAdd.Attributes["associated_command"] = new BattleStateToOverworldCommand();
        buttons.Add(currentButtonToAdd);
        RootUIElement.AddChild(currentButtonToAdd);
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
        //RootUIElement.Draw(gameTime);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    /// <summary>
    /// TODO: change to StateClass = StateClass.ToBattleState();
    /// </summary>
    public static void TransitionToBattleState(CharacterEnemyBase[] enemies)
    {
        StateClass.ChangeToBattleState(enemies);
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
