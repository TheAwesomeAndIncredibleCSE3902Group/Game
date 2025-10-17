using System;
using System.Diagnostics;
using System.Numerics;

namespace AwesomeRPG;

/// <summary>
/// This can be a nice storage bin for enums and math that might be needed by many classes
/// Never change anything in here at runtime
/// Be very careful adding more properties, as they can be hidden anywhere that this is imported statically
/// </summary>
public static class Util
{
    public enum Cardinal { up, right, down, left }

    public static float Root2 { get => 1.41421356237f; }

    /// <summary>
    /// Converts a cardinal direction into it's Vector2 unit vector equivalent
    /// </summary>
    /// <param name="direction">Cardinal Direction to be converted</param> 
    /// <returns>Unit Vector Equivalent</returns> 
    public static Vector2 CardinalToUnitVector(Cardinal direction)
    {
        switch (direction)
        {
            case Cardinal.up:
                return -Vector2.UnitY;
            case Cardinal.right:
                return Vector2.UnitX;
            case Cardinal.down:
                return Vector2.UnitY;
            case Cardinal.left:
                return -Vector2.UnitX;
            default:
                Debug.WriteLine("CardinalToUnitVector in Util has made an oopsie");
                return Vector2.Zero;

        }
    }

    //*** These are extension methods! You can run them right off a Cardinal, as if it was a full-blooded class. ***

    public static Cardinal Opposite(this Cardinal cardinal)
    {
        return cardinal switch
        {
            Cardinal.down => Cardinal.up,
            Cardinal.up => Cardinal.down,
            Cardinal.left => Cardinal.right,
            Cardinal.right => Cardinal.left,
            _ => throw new ArgumentException("Invalid Cardinal!")
        };
    }

    public static Cardinal Rotate(this Cardinal cardinal, bool clockwise = true)
    {
        if (clockwise)
            return (Cardinal)((int)(cardinal + 1) % 4);
        else
            return (Cardinal)((int)(cardinal - 1) % 4);
    }

    public static Cardinal ToCard(this Collision.CollisionInfo.CollisionDirection direction)
    {
        if (direction == Collision.CollisionInfo.CollisionDirection.None)
            throw new ArgumentException("CollisionDirection cannot be null!");

        return direction switch
            {
                Collision.CollisionInfo.CollisionDirection.Bottom => Cardinal.down,
                Collision.CollisionInfo.CollisionDirection.Top => Cardinal.up,
                Collision.CollisionInfo.CollisionDirection.Right => Cardinal.right,
                Collision.CollisionInfo.CollisionDirection.Left => Cardinal.left
            };
    }
}
