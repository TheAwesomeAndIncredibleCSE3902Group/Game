using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateMachineDemo;


public class WalkingState : IState
{
    StateMachine _sm;
    public void Enter(StateMachine sm)
    {
        _sm = sm;
    }
    public void Update()
    {
        //Walking state logic;
        if(true)
        {
            _sm.TransitionState(new IdleState());
        }
    }

    public void Draw()
    {
       //Sprite Logic;
    }
    public void Exit()
    {
        ;
    }
}
