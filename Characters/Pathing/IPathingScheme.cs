using System;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

public interface IPathingScheme
{
    public void Update(GameTime gt);
    public Cardinal GetDirection();
}
