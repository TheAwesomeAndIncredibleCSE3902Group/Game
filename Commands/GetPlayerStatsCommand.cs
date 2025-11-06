using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{

    //Why does this command exist, just use the Player singleton
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
