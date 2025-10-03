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
        private int equipIdx;

        public CommandUseItem(int equipmentIndex)
        {
            currentPlayer = Player.Instance;
            equipIdx = equipmentIndex;
        }

        public void Execute()
        {
            //EVIL ITEM USAGE CHANGE ONCE STATE MACHINE DROP PROLLY
            currentPlayer.PStateMachine.ActiveEquipment = currentPlayer.Equipment[equipIdx];
            currentPlayer.PStateMachine.UseEquipment();
        }
    }
}
