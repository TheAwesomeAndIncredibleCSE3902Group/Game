using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class UseItemCommand : ICommand
    {
        private Player _currentPlayer;
        private IEquipment.Weapons _weapon;

        public UseItemCommand(IEquipment.Weapons weapon)
        {
            _currentPlayer = Player.Instance;
            _weapon = weapon;
        }

        public void Execute()
        {
            _currentPlayer.UseEquipment(_weapon);
        }
    }
}
