using System;

namespace AwesomeRPG.Commands;

public class PlayerToStandingCommand : ICommand
{
    private PlayerOverworld currentPlayer;

    public PlayerToStandingCommand()
    {
        currentPlayer = Player.Instance.PlayerOverworld;
    }
    
    public void Execute()
    {
        currentPlayer.PStateMachine.ChangeStateStanding();
    }
}
