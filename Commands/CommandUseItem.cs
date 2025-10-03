using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.Util;

namespace Sprint0.Commands
{
    public class CommandUseItem : ICommand
    {
        private Player currentPlayer;

        public CommandUseItem(Game1 game)
        {
            currentPlayer = game.Player;
        }

        public void Execute()
        {
            currentPlayer.PStateMachine.UseEquipment();
        }
    }
}
