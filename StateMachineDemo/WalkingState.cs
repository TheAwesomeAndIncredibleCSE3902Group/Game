using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint0.Sprites;
using static Sprint0.Commands.CommandSwitchOverworldItemSprite;
using static Sprint0.Util;

namespace StateMachineDemo;


public class WalkingState : IState
{
    StateMachine _sm;
    float moveSpeed = 2f;
    Cardinal _dir;
    public WalkingState(Cardinal dir)
    {
        _dir = dir;
    }

    public void Enter(StateMachine sm)
    {
        _sm = sm;
    }
    public void Update()
    {
        // Sprite work can be resolved with spriteFactory
        // Lacking time to figure out how I would change directions and switch back to idle
        PlayerTest.Instance.Position += moveSpeed * Util.CardinalToUnitVector(_dir);
        bool noInput = true;
        if(noInput)
        {
            _sm.TransitionState(new IdleState());
        }
    }

    public void Draw()
    {
        MapItemSpriteFactory.Instance.CreateCandleSprite();
    }
    public void Exit()
    {
        
    }
}
