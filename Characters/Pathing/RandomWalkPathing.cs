using System;
using AwesomeRPG;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

public class RandomWalkPathing : IPathingScheme
{
    //How long this tries to walk in one direction
    private const float directionDuration = 0.75f;
    private readonly Cardinal startDirection;
    private Cardinal direction;
    private float timeSinceLastSwap;
    private Random random;

    public RandomWalkPathing(Cardinal startDirection)
    {
        this.startDirection = startDirection;
        this.direction = startDirection;
        random = new Random();
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
        timeSinceLastSwap += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timeSinceLastSwap > directionDuration)
        {
            timeSinceLastSwap = 0;
            
            direction = (Cardinal)random.Next(0, 4);
        }
    }
}