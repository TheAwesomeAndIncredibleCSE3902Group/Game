using System.Diagnostics;

namespace AwesomeRPG.Commands
{
    public class UseItemCommand : ICommand
    {
        private PlayerOverworld _currentPlayer;
        private Weapons _weapon;

        public UseItemCommand(Weapons weapon)
        {
            _currentPlayer = Player.Instance.PlayerOverworld;
            _weapon = weapon;
        }

        public void Execute()
        {
            _currentPlayer.UseEquipment(_weapon);
        }
    }
}
