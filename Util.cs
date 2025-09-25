using System;

namespace Sprint0;

/// <summary>
/// This can be a nice storage bin for enums and math that might be needed by many classes
/// Never change anything in here at runtime
/// </summary>
public static class Util
{
    public enum Cardinal { up, right, down, left }
    public static float Root2 { get => 1.41421356237f; }
}
