using System;
using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        //Yeah, initilizes the UI every frame.
        //This was the cleanest solution
        InitUI(spriteBatch, gameTime);
        rootUIElement.Draw(gameTime);
    }

    private void InitUI(SpriteBatch spriteBatch, GameTime gameTime)
    {
        rootUIElement = new RootElement(spriteBatch);

        //Ensure the font is loaded
        var spriteFont = game.Content.Load<SpriteFont>("Fonts\\MyFont");

        //Background, because I'm sorry but the green was so ugly
        RectElement rect = new RectElement(rootUIElement, Color.Black);
        rect.OffsetAndSize = game.GetScreenRect();
        rootUIElement.AddChild(rect);

        //Text element construction
        String textString = "Game Over! press Enter to return to title";
        Color textColor = Color.White;
        TextElement textElem = new(rootUIElement, spriteFont, textString, textColor);
        textElem.OffsetAndSize = game.GetScreenRect();
        textElem.HorizontalTextAlign = TextElement.TextAlign.Center;
        textElem.VerticalTextAlign = TextElement.TextAlign.Center;
        rootUIElement.AddChild(textElem);
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

    public void ChangeToBattleState() { }

    public void ChangeToOverworldState() { }

    public void ChangeToGameOverState() { }

    public void ChangeToStartState()
    {
        game.Reset();
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