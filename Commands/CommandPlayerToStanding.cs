using System;

namespace AwesomeRPG.Commands;

public class CommandPlayerToStanding : ICommand
{
    private Player currentPlayer;

    public CommandPlayerToStanding(Game1 game)
    {
        currentPlayer = game.Player;
    }
    
    public void Execute()
    {
        currentPlayer.PStateMachine.ChangeStateStanding();
    }
}
