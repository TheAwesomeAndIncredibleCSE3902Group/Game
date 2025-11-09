using System.Collections.Generic;
using AwesomeRPG.Controllers;
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

    // Temporarily commented out for Sprint3 submission
    // public RootElement RootUIElement{get; private set; }

    private List<IController> controllersList = new();
    //Caches the last OverworldState. This makes returning to the overworld much easier
    private OverworldState overState;

    //BattleState can only be made from an OverworldState
    public BattleState(OverworldState overState)
    {

        this.overState = overState;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        //Drawing is currently unimplemented for the Battle State
        throw new System.NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {
        foreach (IController controller in controllersList)
        {
            controller.Update(Game1.GameState.battle);
        }
        throw new System.NotImplementedException();
    }

    public BattleState ToBattleState()
    {
        return this;
    }

    public OverworldState ToOverworldState()
    {
        //This will have to convert all relevant data to Overworld delta
        //Use that delta to modify the Overworld state
        //And then return to that Overworld state

        //TODO: do changes to player, NPCs (ie health), and enemies
        return overState;
        throw new System.NotImplementedException();
    }

}