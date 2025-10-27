using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Sprites;

public interface ISprite
{
    public ulong MillisecondsBetweenFrames { get; set; }
    public int Width { get; }
    public int Height { get;  }
    public void Draw(GameTime gameTime, Vector2 position);
}
