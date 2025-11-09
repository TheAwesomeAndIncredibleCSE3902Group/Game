using System;

namespace AwesomeRPG.Controllers;

public interface IController
{
    // Check for inputs, etc.
    public void Update(Game1.GameState gameState);
}
