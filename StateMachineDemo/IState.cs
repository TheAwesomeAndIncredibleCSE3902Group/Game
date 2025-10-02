using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateMachineDemo;

public interface IState
{
    public enum States {idle,moving}
    public void Enter(StateMachine sm);
    public void Update();
    public void Draw();
    public void Exit();
}
