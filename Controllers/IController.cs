using System;

namespace AwesomeRPG.Controllers;

public interface IController
{
    // Check for inputs, etc.
    public void Update(GameState gameState);
}
