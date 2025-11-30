namespace AwesomeRPG.Commands;

/// <summary>
/// Command for switching to the game over state. Mostly exists for debugging.
/// </summary>
public class GameOverCommand : ICommand
{
    public GameOverCommand() {}

    public void Execute() 
    {
        Game1.TransitionToGameOverState();
    }
}

