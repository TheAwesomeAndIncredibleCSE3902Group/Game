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
        private Player _currentPlayer;
        private IEquipment.Weapons _weapon;

        public CommandUseItem(IEquipment.Weapons weapon)
        {
            _currentPlayer = Player.Instance;
            _weapon = weapon;
        }

        public void Execute()
        {
            _currentPlayer.PStateMachine.UseEquipment(_weapon);
            _currentPlayer.PStateMachine.ChangeStateItemUse();
        }
    }
}
