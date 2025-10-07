using System;
using static Sprint0.Util;

namespace Sprint0.Commands
{
    public class CommandMovePlayer : ICommand
    {
        private Player currentPlayer;
        private Cardinal inputDirection;

        public CommandMovePlayer(Game1 game, Cardinal direction) 
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
