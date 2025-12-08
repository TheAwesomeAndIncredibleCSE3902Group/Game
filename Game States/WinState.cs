using System;
using AwesomeRPG.Characters;
using AwesomeRPG.UI.ElementFactories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AwesomeRPG.UI;

namespace AwesomeRPG;

/// <summary>
/// Game Over State  
/// </summary>
public class WinState(Game1 game) : IGameState
{
    public GameState CurrentState { get => GameState.start; }
    private Game1 game = game;
    public RootElement RootUIElement { get; private set; }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (RootUIElement == null)
        {
            InitUI(spriteBatch, gameTime);            
        }
        RootUIElement.Draw(gameTime);
    }

    private void InitUI(SpriteBatch spriteBatch, GameTime gameTime)
    {
        RootUIElement = new RootElement(spriteBatch);

        //Ensure the font is loaded
        SpriteFont spriteFont = game.Content.Load<SpriteFont>("Fonts\\MyFont");
        
        var rectFactory = new RectElementFactory(RootUIElement);
        var rect = rectFactory.CreateNew(Color.Khaki);
        rect.OffsetAndSize = Util.ScreenRect;
        RootUIElement.AddChild(rect);
        //Text element construction
        String textString = String.Format("You win! Final Level: {0}. Press escape to return to the start.", Player.Instance.Party[0].GetLevel());
        Color textColor = Color.Black;
        var textFactory = new TextElementFactory(RootUIElement);
        var textElem = textFactory.CreateNew(spriteFont, textString, textColor);
        textElem.OffsetAndSize = Util.ScreenRect;
        textElem.Attributes["horizontal_align"] = TextElementFactory.TextAlign.Center;
        textElem.Attributes["vertical_align"] = TextElementFactory.TextAlign.Center;
        RootUIElement.AddChild(textElem);
    }

    public void Update(GameTime gameTime)
    {
        ProcessInput();
    }
    
    private void ProcessInput()
    {
        KeyboardState keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Escape))
        {
            ChangeToStartState();
        }
    }

    public void ChangeToBattleState(CharacterEnemyBase enemy) { }

    public void ChangeToOverworldState() { }

    public void ChangeToGameOverState() { }
    public void ChangeToWinState() { }

    public void ChangeToStartState()
    {
        game.SetStateClass(new StartScreenState(game));
    }

    public bool TransitionAllowedTo(GameState state)
    {
        return state switch
        {
            GameState.battle => false,
            GameState.overworld => false,
            GameState.start => true,
            _ => false
        };
    }
}