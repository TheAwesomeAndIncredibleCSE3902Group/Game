using System;

namespace AwesomeRPG.Commands;

public class PlayerToStandingCommand : ICommand
{
    private Player currentPlayer;

    public PlayerToStandingCommand(Game1 game)
    {
        currentPlayer = game.Player;
    }
    
    public void Execute()
    {
        currentPlayer.PStateMachine.ChangeStateStanding();
    }
}
