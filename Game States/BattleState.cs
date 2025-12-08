using System;
using System.Collections.Generic;
using System.Diagnostics;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.Characters;
using AwesomeRPG.Commands;
using AwesomeRPG.Controllers;
using AwesomeRPG.UI;
using AwesomeRPG.UI.ElementFactories;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG;

/// <summary>
/// Will be made every time the player switches to Battle State
/// </summary>
public class BattleState : IGameState
{
    //This could still be used in case we want different text scrolling times or etc
    //public float TimeScale { get; private set; }

    //Root of the UI. 
    //Currently the UI is initialized at the beginning in Game1 and safely stored in OverworldState.
    public RootElement RootUIElement { get; private set; }

    //Caches the last OverworldState. This makes returning to the overworld much easier
    private OverworldState overworldState;
    private Game1 game;
    //Ideally enemies and enemySprites would be combined into a BattleEnemy
    private CharacterEnemyBase enemy;
    private CharacterEnemyBase[] enemies;
    private CharacterBattleSprite[] enemySprites;

    private string enemyType;
    public GameState CurrentState { get => GameState.battle; }

    //BattleState can only be made from an OverworldState
    public BattleState(OverworldState overState, Game1 game, CharacterEnemyBase enemy)
    {
        this.game = game;
        this.overworldState = overState;
        this.enemy = enemy;
        enemyType = enemy.Name;
        InitializeBattle();
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (RootUIElement == null)
        {
            InitializeUI(spriteBatch);            
        }
        RootUIElement.Draw(gameTime);
        foreach (CharacterBattleSprite enemy in enemySprites)
            enemy.Draw(gameTime);
    }

    public void Update(GameTime gameTime)
    {
        GameSoundFactory.PlayBattleSceneTheme(gameTime);
        RootUIElement.Update(gameTime);
    }

    public void ChangeToBattleState(CharacterEnemyBase enemy) { }
    public void ChangeToStartState() { }
    public void ChangeToGameOverState() { }
    public void ChangeToWinState() { }

    public void ChangeToOverworldState()
    {
        //throw new System.NotImplementedException();
        //This will have to convert all relevant data to Overworld delta
        //Use that delta to modify the Overworld state
        //And then return to that Overworld state
        GameSoundFactory.StopBattleSceneTheme();
        //PlayerSoundFactory.PlayVictoryFanfare();
        enemy.TryDestroy();

        //TODO: do changes to player, NPCs (ie health), and enemies
        overworldState.RootUIElement = this.RootUIElement;
        game.SetStateClass(overworldState);
    }

    public bool TransitionAllowedTo(GameState state)
    {
        return state switch
        {
            GameState.battle => true,
            GameState.overworld => true,
            _ => false
        };
    }

    private void InitializeBattle()
    {
        SetupBattle.Initialize(enemyType);
        BuildBattlePanel();
    }
    
    //Little test method to test displaying multiple enemies
    private void TESTDupeEnemies()
    {
        const int num = 3;

        CharacterEnemyBase[] oldEnemies = enemies;
        enemies = new CharacterEnemyBase[num];

        for (int i = 0; i < num; i++)
        {
            enemies[i] = oldEnemies[0];
        }
        enemies[1] = new CharacterEnemyMoblin(Vector2.Zero, Util.Cardinal.down);
    }

    private void BuildBattlePanel()
    {
        enemies = [enemy];
        TESTDupeEnemies();

        //TODO: background

        Vector2[] enemyOffsets = FindEnemyOffsets(enemies.Length);
        enemySprites = new CharacterBattleSprite[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
            enemySprites[i] = new CharacterBattleSprite(enemies[i].Type, enemyOffsets[i]);

        enemySprites[1].Hurt = true;
    }
    

    /// <summary>
    /// This is a simple and very hacky aligner for up to three enemies
    ///     If you need more than that then we'll have to make a proper solution
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    private static Vector2[] FindEnemyOffsets(int enemies)
    {
        const int enemyWidth = 15;
        const int verticalPadding = 50;
        const int horizontalPadding = 50;

        if (enemies > 3)
            Debug.WriteLine("Warning! FindEnemyOffsets does not work for more than 3 enemies!");

        Rectangle screenRect = Util.ScreenRect;
        Vector2[] offset = new Vector2[enemies];

        offset[0] = new Vector2(horizontalPadding, verticalPadding);
        int thirdHorizontal = screenRect.Width - horizontalPadding - enemyWidth * Util.BattleScale;
        int secondHorizontal = horizontalPadding + (int)((thirdHorizontal - horizontalPadding) / 2f);

        if (enemies == 2)
            offset[1] = new Vector2(thirdHorizontal, verticalPadding);
        else if (enemies == 3)
        {
            offset[1] = new Vector2(secondHorizontal, verticalPadding);
            offset[2] = new Vector2(thirdHorizontal, verticalPadding);
        }

        return offset;
    }

    private void InitializeUI(SpriteBatch spriteBatch)
    {
        RootUIElement = new RootElement(spriteBatch);

        var rectBorderFactory = new RectElementFactory(RootUIElement);
        var battleUiBoardBorder = rectBorderFactory.CreateNew(new Color(40, 0, 40));
        battleUiBoardBorder.OffsetAndSize = new Rectangle(8, 528, 1008, 234);

        var rectBgFactory = new RectElementFactory(RootUIElement);
        var battleUiBoardBg = rectBgFactory.CreateNew(new Color(80, 0, 80));
        battleUiBoardBg.OffsetAndSize = new Rectangle(10, 530, 1004, 230);

        RootUIElement.AddChild(battleUiBoardBorder);
        RootUIElement.AddChild(battleUiBoardBg);

        var battleText = new TextTyperElementFactory(RootUIElement);
        var battleTextElem = battleText.CreateNew(game.DefaultSpriteFont, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur ut facilisis libero. Fusce nec eleifend turpis. Curabitur condimentum dapibus nisl. Ut metus sapien, auctor et justo non, condimentum gravida risus. Donec varius pellentesque felis non ultricies. Quisque fermentum, augue eu pellentesque dictum, ante sapien elementum enim, sed ultricies mauris dui ut lorem. Donec vitae semper enim, sed ornare libero.", Color.White);
        battleTextElem.OffsetAndSize = new Rectangle(20, 540, 984, 210);
        RootUIElement.AddChild(battleTextElem);

        List<Element> buttons = new List<Element>();
        for (int i = 0; i < 6; i++)
        {
            var buttonFactory = new ButtonElementFactory(RootUIElement);
            var currentButtonToAdd = buttonFactory.CreateNew(game.DefaultSpriteFont, game, new Rectangle(20 + (i / 3) * 365, 540 + (i % 3) * 75, 350, 60), Color.Purple, Color.White, "Action " + i);
            buttons.Add(currentButtonToAdd);
            RootUIElement.AddChild(currentButtonToAdd);
        }

        var command = new BattleStateToOverworldCommand();
        buttons[0].AddActionOnUIEvent(UIEvent.ButtonDown, (e) => command.Execute());

        
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

        buttons[0].AddActionOnUIEvent(UIEvent.ButtonPress, (e) =>
        {
            if (((InputUIEventParams) e).Controls.Contains(UIControl.Interact))
            {
                battleTextElem.IsVisible = true;
                battleTextElem.Attributes["currently_drawn_char"] = 0;
                battleTextElem.Attributes["started_typing_time"] = null;
            }
        });

        
    }
}
