using System;
using System.Collections.Generic;
using System.Diagnostics;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Characters;
using AwesomeRPG.Commands;
using AwesomeRPG.Controllers;
using AwesomeRPG.UI;
using AwesomeRPG.UI.ElementFactories;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.BattleMechanics.BattleEnemies;

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
    private OverworldState _overworldState;
    private Game1 game;
    //Ideally enemies and enemySprites would be combined into a BattleEnemy
    private CharacterEnemyBase _enemy;
    private CharacterEnemyBase.CType[] _enemies;
    private CharacterBattleSprite[] _enemySprites;
    private string _enemyType;
    public GameState CurrentState { get => GameState.battle; }

    //BattleState can only be made from an OverworldState
    public BattleState(OverworldState overState, Game1 game, CharacterEnemyBase enemy)
    {
        this.game = game;
        this._overworldState = overState;
        this._enemy = enemy;
        _enemyType = enemy.Name;
        InitializeBattle();
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (RootUIElement == null)
        {
            InitializeUI(spriteBatch);            
        }
        RootUIElement.Draw(gameTime);
        foreach (CharacterBattleSprite enemy in _enemySprites)
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
        _enemy.TryDestroy();

        //TODO: do changes to player, NPCs (ie health), and enemies
        // overworldState.RootUIElement = this.RootUIElement;
        RootUIElement = null;
        game.SetStateClass(_overworldState);
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
        SetupBattle.Initialize(_enemyType);
        BuildBattlePanel();
    }
    
    //Little test method to test displaying multiple enemies
    private void GetEnemies()
    {
        int num = BattleScene.Instance.EnemyList.Count;
        _enemies = new CharacterEnemyBase.CType[num];

        for (int i = 0; i < num; i++)
        {
            _enemies[i] = (BattleScene.Instance.EnemyList[i] as IEnemyBattle).Type;
        }

    }

    private void BuildBattlePanel()
    {
        GetEnemies();

        //TODO: background
        if (_enemies.Length > 0)
        {
            Vector2[] enemyOffsets = FindEnemyOffsets(_enemies.Length);
            _enemySprites = new CharacterBattleSprite[_enemies.Length];

            for (int i = 0; i < _enemies.Length; i++)
                _enemySprites[i] = new CharacterBattleSprite(_enemies[i], enemyOffsets[i]);

        }
        else
        {
            Debug.WriteLine("ERROR _enemies is null");
        }
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

        var textElementFactory = new TextElementFactory(RootUIElement);
        var animSpriteElementFactory = new AnimSpriteElementFactory(RootUIElement);

        var rectFactory = new RectElementFactory(RootUIElement);
        var battleUiBoardBorder = rectFactory.CreateNew(new Color(40, 0, 40));
        battleUiBoardBorder.OffsetAndSize = new Rectangle(8, 528, 1008, 234);

        var battleUiBoardBg = rectFactory.CreateNew(new Color(80, 0, 80));
        battleUiBoardBg.OffsetAndSize = new Rectangle(10, 530, 1004, 230);

        RootUIElement.AddChild(battleUiBoardBorder);
        RootUIElement.AddChild(battleUiBoardBg);

        var allyHealthContainer = new Element(RootUIElement);
        allyHealthContainer.OffsetAndSize = new Rectangle(745, 540, 260, 210);
        for (int i = 0; i < 4; i++)
        {
            var allyHealth = new Element(RootUIElement);
            allyHealth.OffsetAndSize = new Rectangle(0, i * 55, 260, 45);

            var currentBgRect = rectFactory.CreateNew(new Color(0, 0, 0, 0.2f));
            currentBgRect.OffsetAndSize = new Rectangle(0, 0, 260, 45);
            allyHealth.AddChild(currentBgRect);

            if (i < BattleScene.Instance.AllyList.Count)
            {
                allyHealth.Attributes["associated_battle"] = BattleScene.Instance.AllyList[i];

                var currentHealthBarBg = rectFactory.CreateNew(new Color(100, 0, 0));
                currentHealthBarBg.OffsetAndSize = new Rectangle(46, 27, 208, 12);
                allyHealth.AddChild(currentHealthBarBg);

                var currentHealthBarFg = rectFactory.CreateNew(new Color(0, 200, 0));
                currentHealthBarFg.OffsetAndSize = new Rectangle(46, 27, 111, 12); //TODO: set width according to current health
                allyHealth.AddChild(currentHealthBarFg);

                var currentTextElem = textElementFactory.CreateNew(game.DefaultSpriteFont, BattleScene.Instance.AllyList[i].Name, Color.White);
                currentTextElem.OffsetAndSize = new Rectangle(46, 0, 208, 20);
                allyHealth.AddChild(currentTextElem);
                
                var playerIcon = animSpriteElementFactory.CreateNew();
                // TODO: add the anim sprite element of the player's battle icon here
                playerIcon.OffsetAndSize = new Rectangle(6,7, 32, 32);
                allyHealth.AddChild(playerIcon);
                
            }

            allyHealthContainer.AddChild(allyHealth);
        }
        RootUIElement.AddChild(allyHealthContainer);

        var battleText = new TextTyperElementFactory(RootUIElement);
        var battleTextElem = battleText.CreateNew(game.DefaultSpriteFont, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur ut facilisis libero. Fusce nec eleifend turpis. Curabitur condimentum dapibus nisl. Ut metus sapien, auctor et justo non, condimentum gravida risus. Donec varius pellentesque felis non ultricies. Quisque fermentum, augue eu pellentesque dictum, ante sapien elementum enim, sed ultricies mauris dui ut lorem. Donec vitae semper enim, sed ornare libero.", Color.White);
        battleTextElem.OffsetAndSize = new Rectangle(20, 540, 984, 210);
        RootUIElement.AddChild(battleTextElem);

        battleTextElem.IsVisible = false;

        var beforeFirstFrame = true;
        
        RootUIElement.AddActionOnUIEvent(UIEvent.Update, (e) =>
        {
            if (beforeFirstFrame) beforeFirstFrame = false;
        });

        var buttonContainer = new Element(RootUIElement);
        var buttonFactory = new ButtonElementFactory(RootUIElement);
        for (int i = 0; i < 6; i++)
        {
            var currentButtonToAdd = buttonFactory.CreateNew(game.DefaultSpriteFont, game, new Rectangle(20 + (i / 3) * 365, 540 + (i % 3) * 75, 350, 60), Color.Purple, Color.White, "Action " + i);
            currentButtonToAdd.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
            {
                var eventParams = (InputUIEventParams)e;
            });
            buttonContainer.AddChild(currentButtonToAdd);
        }

        RootUIElement.AddChild(buttonContainer);
        
        RootUIElement.UIState.SelectionIndex = 0;

        RootUIElement.AddActionOnUIEvent(UIEvent.ButtonDown, (e) =>
        {
            // Ensure that if arrow key is being held on the first frame. If so, exit prevent updating the selectoin index.
            // Prevents player from changing which button is selected if they're walking into enemy before starting battle
            if (beforeFirstFrame) return;
            
            var eventParams = (InputUIEventParams)e;
            // System.Console.WriteLine("This is a test!!");
            if (eventParams.Controls.Contains(UIControl.MoveDown))
            {
                RootUIElement.UIState.SelectionIndex += 1;
                UISoundFactory.PlayGrazeSoundEffect();
            }
            if (eventParams.Controls.Contains(UIControl.MoveUp))
            {
                RootUIElement.UIState.SelectionIndex -= 1;
                UISoundFactory.PlayGrazeSoundEffect();
            }
            if (eventParams.Controls.Contains(UIControl.MoveRight))
            {
                RootUIElement.UIState.SelectionIndex += 3;
                UISoundFactory.PlayGrazeSoundEffect();
            }
            if (eventParams.Controls.Contains(UIControl.MoveLeft))
            {
                RootUIElement.UIState.SelectionIndex -= 3;
                UISoundFactory.PlayGrazeSoundEffect();
            }
        });

        var buttons = buttonContainer.GetChildren();

        void SwitchToBattleText()
        {
            battleTextElem.Attributes["currently_drawn_char"] = 0;
            battleTextElem.Attributes["started_typing_time"] = null;
            foreach (Element elem in buttons)
            {
                elem.IsVisible = false;
                elem.MakeUnselectable();
            }
            battleTextElem.IsVisible = true;
            allyHealthContainer.IsVisible = false;
            battleTextElem.MakeSelectable();
            RootUIElement.UIState.SelectionIndex = 0;
            UISoundFactory.PlaySelectSoundEffect();
        }

        void SwitchToButtons()
        {
            battleTextElem.IsVisible = false;
            battleTextElem.MakeUnselectable();
            foreach (Element elem in buttons)
            {
                elem.IsVisible = true;
                elem.MakeSelectable();
            }
            allyHealthContainer.IsVisible = true;
            RootUIElement.UIState.SelectionIndex = 0;
            UISoundFactory.PlaySelectSoundEffect();
        }

        void DoNextTurn()
        {
            BattleScene.Instance.NextTurn();
            foreach (CharacterBattleSprite enemy in _enemySprites) { enemy.Hurt = false; }
            if (BattleScene.Instance.CurrentBattle.IsFriend)
            {
                SwitchToButtons();
            } else
            {
                (BattleScene.Instance.CurrentBattle as IEnemyBattle).TakeTurn();
                if (BattleScene.Instance.CurrentBattle.TurnText == null)
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.ToString() + ": TurnText string is null...";
                    
                } else
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.TurnText;
                }
                SwitchToBattleText();
            }
        }

        buttons[0].AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            if (((InputUIEventParams)e).Controls.Contains(UIControl.Interact))
            {
                int target = 0;
                for (int i = 0; i < BattleScene.Instance.EnemyList.Count; i++) { if (!BattleScene.Instance.EnemyList[i].IsFainted) { target = i; break; } }
                (BattleScene.Instance.CurrentBattle as PlayerBattle).Attack(target);
                _enemySprites[target].Hurt = true;

                if (BattleScene.Instance.CurrentBattle.TurnText == null)
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.ToString() + ": TurnText string is null...";

                }
                else
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.TurnText;
                }
                SwitchToBattleText();
            }
        });
        buttons[1].AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            if (((InputUIEventParams)e).Controls.Contains(UIControl.Interact))
            {
                (BattleScene.Instance.CurrentBattle as PlayerBattle).Heal();
                if (BattleScene.Instance.CurrentBattle.TurnText == null)
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.ToString() + ": TurnText string is null...";

                }
                else
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.TurnText;
                }
                SwitchToBattleText();
            }
        });
        buttons[2].AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            if (((InputUIEventParams)e).Controls.Contains(UIControl.Interact))
            {
                int target = 0;
                for (int i = 0; i < BattleScene.Instance.EnemyList.Count; i++) { if (!BattleScene.Instance.EnemyList[i].IsFainted) { target = i; break; } }
                switch (BattleScene.Instance.CurrentBattle.Name)
                {
                    case "Link":
                        (BattleScene.Instance.CurrentBattle as LinkBattle).SwordStab(target);
                        _enemySprites[target].Hurt = true;
                        break;
                    case "Old Lady":
                        (BattleScene.Instance.CurrentBattle as OldLadyBattle).WiseAdvice(target);
                        _enemySprites[target].Hurt = true;
                        break;
                    case "Zelda":
                        (BattleScene.Instance.CurrentBattle as ZeldaBattle).LightArrow(target);
                        _enemySprites[target].Hurt = true;
                        break;
                    case "Merchant":
                        (BattleScene.Instance.CurrentBattle as MerchantBattle).ThrowGold(target);
                        _enemySprites[target].Hurt = true;
                        break;
                }
                if (BattleScene.Instance.CurrentBattle.TurnText == null)
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.ToString() + ": TurnText string is null...";

                }
                else
                {
                    battleTextElem.Attributes["text_string"] = BattleScene.Instance.CurrentBattle.TurnText;
                }
                SwitchToBattleText();
            }
        });
        battleTextElem.AddActionOnUIEvent(UIEvent.ButtonDown, (e) =>
        {
            var eventParams = (InputUIEventParams) e;
            if (eventParams.Controls.Contains(UIControl.Return))
            {
                // Skip typing text
                battleTextElem.Attributes["started_typing_time"] = (long) 0;
            }
        });
        battleTextElem.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            var eventParams = (InputUIEventParams) e;
            if (eventParams.Controls.Contains(UIControl.Interact))
            {
                DoNextTurn();
            }
        });
        buttons[5].AddActionOnUIEvent(UIEvent.ButtonPress, (e) =>
        {
            if (((InputUIEventParams) e).Controls.Contains(UIControl.Interact))
            {
                Game1.StateClass.ChangeToOverworldState();
            }
        });
    }
}
