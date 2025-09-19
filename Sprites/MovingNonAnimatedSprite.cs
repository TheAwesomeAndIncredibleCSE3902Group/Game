using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public class MovingNonAnimatedSprite : SpriteBase
{
    int offsetX = 400;
    int offsetY = 200;
    double circularPosition = 0;
    public MovingNonAnimatedSprite(SpriteBatch spriteBatch, Texture2D spriteTexture, Func<int> GetChosenSpriteFunc)
    {
        GetChosenSprite = GetChosenSpriteFunc;
        SetUpSprite(
            spriteBatch,
            spriteTexture,
            new Point(offsetX, offsetY),
            new Vector2(4.0f, 4.0f),
            [
                new SourceRectangle(21, 112, 32, 38, 0, 0)
            ],
            0
        );
    }
    override public void Update(GameTime gameTime)
    {
        Enabled = GetChosenSprite() == 2;
        if (Enabled)
        {
            circularPosition += gameTime.ElapsedGameTime.Milliseconds / 350.0;
            _position.X = offsetX + (int)(Math.Cos(circularPosition) * 200.0);
            _position.Y = offsetY + (int)(Math.Sin(circularPosition) * 100.0);
            circularPosition %= Math.PI * 2;
        }
    }
}
