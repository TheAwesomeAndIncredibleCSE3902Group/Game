using System;
using System.Collections.Generic;
using AwesomeRPG.Characters;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI;
using System.Diagnostics;
using AwesomeRPG.UI.ElementFactories;
//using System.Drawing;

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
    public RootElement RootUIElement { get; set; }


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
        GameSoundFactory.PlayOverworldMapTheme(gameTime);
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);

        RoomAtlas.Instance.CurrentRoom.Update(gameTime);
        //TODO: refresh HUD here

    }

    public void ChangeToBattleState(CharacterEnemyBase enemy)
    {
        //This will have to convert any relevant data to its battle representation
        //And return a new BattleState
        GameSoundFactory.StopOverworldMapTheme();
        game.SetStateClass(new BattleState(this, game, enemy));
    }

    public void ChangeToOverworldState() { }

    public void ChangeToStartState() { }
    
    public void ChangeToGameOverState()
    {
        game.SetStateClass(new GameOverState(game));
    }
    
    public void ChangeToWinState()
    {
        game.SetStateClass(new WinState(game));
    }

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
        var spriteFont = game.Content.Load<SpriteFont>("Fonts\\ZeldaFont");
        TextElementFactory textElementFactory = new TextElementFactory(rootUIElement);
        string healthString = GetPartyHealthString();
        Element textElem = textElementFactory.CreateNew(spriteFont, 
                                                        healthString, 
                                                        Color.White,
                                                        TextElementFactory.TextAlign.Left,
                                                        TextElementFactory.TextAlign.Right);
        textElem.OffsetAndSize = Util.ScreenRect;
        rootUIElement.AddChild(textElem);
         rootUIElement.Draw(gameTime);
    }

    private string GetPartyHealthString()
    {
        string healthString = "";
        var partyHealths = Player.Instance.Party;
        foreach(var member in partyHealths)
        {
            healthString += $"{member.Type}: {member.GetHealth()}HP\n";
        }
        return healthString;
    }
}