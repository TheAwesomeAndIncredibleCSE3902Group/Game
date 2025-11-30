namespace AwesomeRPG.Commands;

/// <summary>
/// Command for switching to the win state. Mostly exists for debugging.
/// </summary>
public class WinCommand : ICommand
{
    public WinCommand() {}

    public void Execute() 
    {
        Game1.TransitionToWinState();
    }
}

