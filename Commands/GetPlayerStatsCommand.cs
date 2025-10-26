using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    class GetPlayerStatsCommand
    {
        private Player _currentPlayer;

        public GetPlayerStatsCommand(Game1 game)
        {
            _currentPlayer = game.Player;
        }

        public int Execute()
        {
            // Current player stat right now
            return _currentPlayer.PStateMachine.Health;
        }
    }
}
