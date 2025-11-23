using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class MovePlayerCommand : ICommand
    {
        private PlayerOverworld currentPlayer;
        private Cardinal inputDirection;

        public MovePlayerCommand(Cardinal direction) 
        {
            currentPlayer = Player.Instance.PlayerOverworld;
            inputDirection = direction;
        }

        public void Execute()
        {
            currentPlayer.Move(inputDirection);
        }

    }
}
