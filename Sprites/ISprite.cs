using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public interface ISprite
{
    public Point Position { get; set; }
    public Vector2 Scale { get; set; }
    public bool Enabled { get; set; }
    public double AnimationSpeed { get; set; }
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}
