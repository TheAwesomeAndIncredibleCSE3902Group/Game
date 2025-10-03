using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace StateMachineDemo;


public class IdleState : IState
{
    StateMachine _sm;

    public void Enter(StateMachine sm)
    {
        _sm = sm;
    }

    public void Update()
    {
        
    }

    public void Draw()
    {
        MapItemSpriteFactory.Instance.CreatePotionSprite();
    }
    public void Exit()
    {

    }
}
