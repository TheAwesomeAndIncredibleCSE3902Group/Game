using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Sprites;

public interface ISprite
{
    public ulong MillisecondsBetweenFrames { get; set; }
    public void Draw(GameTime gameTime, Vector2 position);
}
