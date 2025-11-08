using System.Collections.Generic;
using AwesomeRPG.Controllers;
using Microsoft.Xna.Framework;

namespace AwesomeRPG;

public class BattleState : IGameState
{
    //This could still be used in case we want different text scrolling times or etc
    //public float TimeScale { get; private set; }
    private List<IController> controllersList = new();
    private OverworldState overState;

    //BattleState can only be made from an OverworldState
    public BattleState(OverworldState overState)
    {
        
        this.overState = overState;
    }

    public void Draw(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {
        foreach (IController controller in controllersList) {
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
        //This will have to convert all relevant data to Overworld representation
        //And then return a (cached? Overworld state)

        //TODO: do changes to player, NPCs (ie health), and enemies
        return overState;
        throw new System.NotImplementedException();
    }

}