using System;
using System.Diagnostics;
using System.Numerics;

namespace Sprint0;

/// <summary>
/// This can be a nice storage bin for enums and math that might be needed by many classes
/// Never change anything in here at runtime
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
        switch(direction)
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
}
