using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public class MovingAnimatedSprite : SpriteBase
{
    public MovingAnimatedSprite(SpriteBatch spriteBatch, Texture2D spriteTexture, Func<int> GetChosenSpriteFunc)
    {
        GetChosenSprite = GetChosenSpriteFunc;
        SetUpSprite(
            spriteBatch,
            spriteTexture,
            new Point(250, 200),
            new Vector2(4.0f, 4.0f),
            [
                new SourceRectangle(0, 0, 19, 37, 0, 0),
                new SourceRectangle(19, 0, 19, 36, 0, 1),
                new SourceRectangle(38, 0, 19, 37, 0, 0),
                new SourceRectangle(57, 0, 19, 36, 0, 1),
            ],
            0.26
        );
    }
    int direction = 1;
    override public void Update(GameTime gameTime)
    {
        Enabled = GetChosenSprite() == 3;
        if (Enabled)
        {
            _position.X += (int)(direction * (gameTime.ElapsedGameTime.TotalMilliseconds * 0.25));
            if (_position.X > 450 || _position.X < 250)
            {
                direction *= -1;
                IsFlippedHorizontal = !IsFlippedHorizontal;
            }
        }
    }
}
