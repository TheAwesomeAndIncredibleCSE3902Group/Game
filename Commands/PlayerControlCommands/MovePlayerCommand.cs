using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class MovePlayerCommand : ICommand
    {
        private Player currentPlayer;
        private Cardinal inputDirection;

        public MovePlayerCommand(Game1 game, Cardinal direction) 
        {
            currentPlayer = game.Player;
            inputDirection = direction;
        }

        public void Execute()
        {
            currentPlayer.Move(inputDirection);
        }

    }
}
