using System;
using AwesomeRPG.Characters;
using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AwesomeRPG;

/// <summary>
/// Game Over State  
/// </summary>
public class WinState(Game1 game) : IGameState
{
    public GameState CurrentState { get => GameState.start; }
    private Game1 game = game;
    RootElement rootUIElement;

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
        SpriteFont spriteFont = game.Content.Load<SpriteFont>("Fonts\\MyFont");
        
        RectElement rect = new RectElement(rootUIElement, Color.Khaki);
        rect.OffsetAndSize = game.GetScreenRect();
        rootUIElement.AddChild(rect);
        //Text element construction
        String textString = String.Format("You win! Final Level: {0}. Press escape to return to the start.", Player.Instance.PlayerStats.GetLevel());
        Color textColor = Color.Black;
        TextElement textElem = new(rootUIElement, spriteFont, textString, textColor)
        {
            OffsetAndSize = game.GetScreenRect(),
            HorizontalTextAlign = TextElement.TextAlign.Center,
            VerticalTextAlign = TextElement.TextAlign.Center
        };
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