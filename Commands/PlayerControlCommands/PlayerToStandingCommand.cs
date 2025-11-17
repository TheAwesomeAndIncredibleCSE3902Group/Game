using System;

namespace AwesomeRPG.Commands;

public class PlayerToStandingCommand : ICommand
{
    private PlayerOverworld currentPlayer;

    public PlayerToStandingCommand()
    {
        currentPlayer = PlayerOverworld.Instance;
    }
    
    public void Execute()
    {
        currentPlayer.PStateMachine.ChangeStateStanding();
    }
}
