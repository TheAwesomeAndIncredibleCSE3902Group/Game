using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class DamagePlayerCommand : ICommand
    {
        private Player currentPlayer;

        public DamagePlayerCommand()
        {
            currentPlayer = Player.Instance;
        }

        public void Execute()
        {
            currentPlayer.TakeDamage();
        }

    }
}
