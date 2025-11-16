using System;

namespace AwesomeRPG.Commands;

public class PlayerToStandingCommand : ICommand
{
    private Player currentPlayer;

    public PlayerToStandingCommand()
    {
        currentPlayer = Player.Instance;
    }
    
    public void Execute()
    {
        currentPlayer.PStateMachine.ChangeStateStanding();
    }
}
