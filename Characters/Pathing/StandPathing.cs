using System;
using AwesomeRPG;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

public class StandPathing : IPathingScheme
{
    private Cardinal direction;

    public StandPathing(Cardinal startDirection)
    {
        this.direction = startDirection;
    }

    public Cardinal GetDirection() => direction;

    public bool TrySetDirection(Cardinal direction)
    {
        this.direction = direction;
        //Commented out to fix enemies getting stuck bouncing between two close walls
        //timeSinceLastSwap = 0;
        return true;
    }

    public void Update(GameTime gameTime)
    {
    }
}