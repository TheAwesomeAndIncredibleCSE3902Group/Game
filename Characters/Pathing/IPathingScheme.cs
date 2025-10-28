using System;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

/// <summary>
/// The Character polls IPathingScheme.GetDirection() every frame
/// </summary>
public interface IPathingScheme
{
    public void Update(GameTime gt);
    public Cardinal GetDirection();
    public bool TrySetDirection(Cardinal direction);
}
