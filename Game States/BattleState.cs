using System.Collections.Generic;
using AwesomeRPG.Controllers;
using AwesomeRPG.UI.Elements;
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

    //Root of the UI
    private RootElement RootUIElement { get; set; }

    //Caches the last OverworldState. This makes returning to the overworld much easier
    private OverworldState overworldState;
    private Game1 game;
    public Game1.GameState CurrentState { get => Game1.GameState.battle; }

    //BattleState can only be made from an OverworldState
    public BattleState(OverworldState overState, Game1 game)
    {
        this.game = game;
        this.overworldState = overState;
        this.RootUIElement = game.RootUIElement;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        RootUIElement.Draw(gameTime);
        //Drawing is currently unimplemented for the Battle State
        throw new System.NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {

        throw new System.NotImplementedException();
    }

    public void ChangeToBattleState() { }
    public void ChangeToStartState() { }
    public void ChangeToGameOverState() { }

    public void ChangeToOverworldState()
    {
        throw new System.NotImplementedException();
        //This will have to convert all relevant data to Overworld delta
        //Use that delta to modify the Overworld state
        //And then return to that Overworld state

        //TODO: do changes to player, NPCs (ie health), and enemies
        game.SetStateClass(overworldState);
    }

    public bool TransitionAllowedTo(Game1.GameState state)
    {
        return state switch
        {
            Game1.GameState.battle => true,
            Game1.GameState.overworld => true,
            _ => false
        };
    }

}