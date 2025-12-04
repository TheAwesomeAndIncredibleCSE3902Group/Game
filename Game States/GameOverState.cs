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
public class GameOverState : IGameState
{
    public GameState CurrentState { get => GameState.start; }
    private Game1 game;
    RootElement rootUIElement;
    public GameOverState(Game1 game)
    {
        this.game = game;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (rootUIElement == null)
        {
            InitUI(spriteBatch, gameTime);            
        }
        rootUIElement.Draw(gameTime);
    }

    private void InitUI(SpriteBatch spriteBatch, GameTime gameTime)
    {
        rootUIElement = new RootElement(spriteBatch);

        //Ensure the font is loaded
        var spriteFont = game.Content.Load<SpriteFont>("Fonts\\MyFont");

        //Black background
        var rectFactory = new RectElementFactory(rootUIElement);
        var rect = rectFactory.CreateNew(Color.Black);
        rect.OffsetAndSize = Util.ScreenRect;
        rootUIElement.AddChild(rect);

        //Text element construction
        String textString = "Game Over! Press Escape to return to title.";
        Color textColor = Color.White;
        var textFactory = new TextElementFactory(rootUIElement);
        var textElem = textFactory.CreateNew(spriteFont, textString, textColor);
        textElem.OffsetAndSize = Util.ScreenRect;
        textElem.Attributes["horizontal_align"] = TextElementFactory.TextAlign.Center;
        textElem.Attributes["vertical_align"] = TextElementFactory.TextAlign.Center;
        rootUIElement.AddChild(textElem);
    }

    public void Update(GameTime gameTime)
    {
        ProcessInput();
        rootUIElement.Update(gameTime);
    }
    
    private void ProcessInput()
    {
        KeyboardState keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Escape))
        {
            ChangeToStartState();
        }
    }

    public void ChangeToBattleState(CharacterEnemyBase[] enemies) { }

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