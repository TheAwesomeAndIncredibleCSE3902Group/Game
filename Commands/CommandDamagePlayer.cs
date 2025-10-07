using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class CommandDamagePlayer : ICommand
    {
        private Player currentPlayer;

        public CommandDamagePlayer(Game1 game)
        {
            currentPlayer = game.Player;
        }

        public void Execute()
        {
            currentPlayer.TakeDamage();
        }

    }
}
