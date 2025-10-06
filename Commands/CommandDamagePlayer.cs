using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.Util;

namespace Sprint0.Commands
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
            currentPlayer.PStateMachine.ChangeStateDamaged();
        }

    }
}
