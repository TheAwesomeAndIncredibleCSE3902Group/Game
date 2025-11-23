using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class DamagePlayerCommand : ICommand
    {
        private PlayerOverworld currentPlayer;

        public DamagePlayerCommand()
        {
            currentPlayer = Player.Instance.PlayerOverworld;
        }

        public void Execute()
        {
            PlayerSoundFactory.PlayLinkHurtSoundEffect();
            currentPlayer.TakeDamage();
        }

    }
}
