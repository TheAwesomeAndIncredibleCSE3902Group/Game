using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class DamagePlayerCommand : ICommand
    {
        private Player currentPlayer;

        public DamagePlayerCommand(Game1 game)
        {
            currentPlayer = game.Player;
        }

        public void Execute()
        {
            currentPlayer.TakeDamage();
        }

    }
}
