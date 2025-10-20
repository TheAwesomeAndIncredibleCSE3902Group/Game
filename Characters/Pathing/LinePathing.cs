using System;
using AwesomeRPG;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

public class LinePathing : IPathingScheme
{
    //How long this tries to walk in one direction
    private const float directionDuration = 3;
    private readonly Cardinal startDirection;
    private Cardinal direction;
    private float timeSinceLastSwap;

    public LinePathing(Cardinal startDirection)
    {
        this.startDirection = startDirection;
        this.direction = startDirection;
    }

    public Cardinal GetDirection() => direction;

    public void Update(GameTime gameTime)
    {
        timeSinceLastSwap += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timeSinceLastSwap > directionDuration)
        {
            timeSinceLastSwap = 0;
            direction = direction.Opposite();
        }
    }
}