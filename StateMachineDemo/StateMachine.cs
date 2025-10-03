using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateMachineDemo;


    public class StateMachine
    {
        private IState currentState;
        public void Initialize()
        {
            currentState = new IdleState();
            currentState.Enter(this);
        }

        public void TransitionState(IState newState)
        {
            currentState = newState;
        }

        public void Draw(GameTime gt, Vector2 positon)
        {
            currentState.Draw();
        }

        public void Update(GameTime gt)
        {
            currentState.Update();
        }
    }

