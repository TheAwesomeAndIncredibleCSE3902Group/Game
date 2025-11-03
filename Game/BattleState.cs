using System.Collections.Generic;
using AwesomeRPG.Controllers;
using Microsoft.Xna.Framework;

namespace AwesomeRPG;

public class BattleState : IGameState
{
    //public float TimeScale { get; private set; }
    private List<IController> controllersList = new();


    public void Draw(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public BattleState ToBattleState()
    {
        return this;
    }

    public OverworldState ToOverworldState()
    {
        throw new System.NotImplementedException();
    }

}