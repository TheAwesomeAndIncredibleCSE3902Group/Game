using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateMachineDemo;

public interface IState
{
    public void Enter(StateMachine sm);
    public void Update();
    public void Draw();
    public void Exit();
}
