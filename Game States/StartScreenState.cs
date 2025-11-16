using System;
using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AwesomeRPG;

/// <summary>
/// Start screen state does not inherit IGameState
/// </summary>
public class StartScreenState : IGameState
{
    public GameState CurrentState { get => GameState.start; }
    private Game1 game;
    RootElement rootUIElement;
    public StartScreenState(Game1 game)
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
        RectElement rect = new RectElement(rootUIElement, Color.BurlyWood);
        rect.OffsetAndSize = game.GetScreenRect();
        rootUIElement.AddChild(rect);

        //Text element construction
        String textString = "Press space to start game!";
        Color textColor = LerpTextColors(gameTime);
        TextElement textElem = new TextElement(rootUIElement, spriteFont, textString, textColor);
        textElem.OffsetAndSize = game.GetScreenRect();
        textElem.HorizontalTextAlign = TextElement.TextAlign.Center;
        textElem.VerticalTextAlign = TextElement.TextAlign.Center;
        rootUIElement.AddChild(textElem);
    }

    private Color LerpTextColors(GameTime gameTime)
    {
        const float frequency = 0.5f;
        double totalSeconds = gameTime.TotalGameTime.TotalSeconds * frequency;
        //Create a sin wave that varies from [0,1] with a specified frequency
        float lerp = (float)Math.Cos(Math.Tau * (totalSeconds - (int)totalSeconds)) / 2f + 0.5f;
        return Color.Lerp(Color.DarkBlue, Color.BlueViolet, lerp);
    }

    public void Update(GameTime gameTime)
    {
        ProcessInput();
    }
    
    private void ProcessInput()
    {
        KeyboardState keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Space))
        {
            ChangeToOverworldState();
        }
    }

    public void ChangeToBattleState() { }

    public void ChangeToGameOverState() { }
    
    public void ChangeToStartState() { }

    public void ChangeToOverworldState()
    {
        //Sets the game.StateClass to a new OverworldState, so this is now orphaned (ie killed)
        game.InitializeOverworldAndControllers();
    }

    public bool TransitionAllowedTo(GameState state)
    {
        return state switch
        {
            GameState.battle => false,
            GameState.overworld => true,
            _ => false
        };
    }
}