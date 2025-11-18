using System;
using System.Collections.Generic;
using AwesomeRPG.Characters;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Elements;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// Made once per sessions and only modified thereafter.
/// Handles the updating and drawing of all overworld things: Rooms (which includes Characters), Player, and Collisions.
/// </summary>
public class OverworldState : IGameState
{
    //Will eventually be used as a global scalar for time (ie affects everything in the Overworld)
    public float TimeScale { get; private set; } = 1;
    private Game1 game;
    public GameState CurrentState { get => GameState.overworld; }


    /// <summary>
    /// Requires Content already loaded and Player fully constructed
    /// </summary>
    /// <param name="contentManager"></param>
    /// <param name="player"></param>
    /// <exception cref="NotImplementedException"></exception>
    public OverworldState(ContentManager contentManager, PlayerOverworld player, Game1 game)
    {
        this.game = game;

        CreateWorld(contentManager);

        RoomAtlas.Instance.AddPlayer(player);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        //Scale gameTime by TimeScale
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);

        RoomAtlas.Instance.CurrentRoom.Draw(spriteBatch, gameTime);
        //TODO: draw HUD here
        DrawPlayerHUD(gameTime,spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);

        RoomAtlas.Instance.CurrentRoom.Update(gameTime);
        //TODO: refresh HUD here

    }

    public void ChangeToBattleState(CharacterEnemyBase[] enemies)
    {
        //This will have to convert any relevant data to its battle representation
        //And return a new BattleState
        game.SetStateClass(new BattleState(this, game, enemies));
    }

    public void ChangeToOverworldState() { }

    public void ChangeToStartState() { }
    
    public void ChangeToGameOverState() { }

    private void CreateWorld(ContentManager contentManager)
    {
        RoomAtlas.Instance.SetAtlas(AtlasInitializer.InitializeAtlas(contentManager));
        RoomAtlas.Instance.CurrentRoom = RoomAtlas.Instance.GetRoom(0,0);
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

    private void DrawPlayerHUD(GameTime gameTime,SpriteBatch spriteBatch)
    {
        RootElement rootUIElement = new RootElement(spriteBatch);

        //Ensure the font is loaded
        var spriteFont = game.Content.Load<SpriteFont>("Fonts\\MyFont");

        //Text element construction
        String textString = Player.Instance.PlayerStats.GetHealth().ToString();
        TextElement textElem = new TextElement(rootUIElement, spriteFont, textString, Color.White);
        textElem.OffsetAndSize = game.GetScreenRect();
        textElem.HorizontalTextAlign = TextElement.TextAlign.Left;
        textElem.VerticalTextAlign = TextElement.TextAlign.Right;
        rootUIElement.AddChild(textElem);
        rootUIElement.Draw(gameTime);
    }
}