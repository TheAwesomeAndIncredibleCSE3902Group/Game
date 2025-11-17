using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class DamagePlayerCommand : ICommand
    {
        private PlayerOverworld currentPlayer;

        public DamagePlayerCommand()
        {
            currentPlayer = PlayerOverworld.Instance;
        }

        public void Execute()
        {
            currentPlayer.TakeDamage();
        }

    }
}
