using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateMachineDemo;


    public class StateMachine
    {
        private Dictionary<PlayerTest.States, IState> playerStates;
        private IState currentState;
        public void Initialize()
        {
            playerStates[PlayerTest.States.Walking] = new WalkingState();
            playerStates[PlayerTest.States.Idle] = new IdleState();

            currentState = new WalkingState();
            currentState.Enter(this);
        }

        public void TransitionState(IState newState)
        {
            currentState = newState;
        }

        public void Draw()
        {
            currentState.Draw();
        }

        public void Update()
        {
            currentState.Update();
        }
    }

