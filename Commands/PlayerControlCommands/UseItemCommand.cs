using System.Diagnostics;

namespace AwesomeRPG.Commands
{
    public class UseItemCommand : ICommand
    {
        private Player _currentPlayer;
        private Weapons _weapon;

        public UseItemCommand(Weapons weapon)
        {
            _currentPlayer = Player.Instance;
            _weapon = weapon;
        }

        public void Execute()
        {
            _currentPlayer.UseEquipment(_weapon);
            Debug.WriteLine(_currentPlayer.Position);
        }
    }
}
