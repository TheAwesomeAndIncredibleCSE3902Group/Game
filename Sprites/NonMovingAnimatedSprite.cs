using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public class NonMovingAnimatedSprite : SpriteBase
{
    public NonMovingAnimatedSprite(SpriteBatch spriteBatch, Texture2D spriteTexture, Func<int> GetChosenSpriteFunc)
    {
        GetChosenSprite = GetChosenSpriteFunc;
        SetUpSprite(
            spriteBatch,
            spriteTexture,
            new Point(10, 300),
            new Vector2(4.0f, 4.0f),
            [
                new SourceRectangle(0, 38, 30, 37, 5, 0),
                new SourceRectangle(30, 38, 41, 37, 5, 0),
                new SourceRectangle(71, 37, 50, 37, 6, 0),
                new SourceRectangle(76, 0, 54, 37, 6, 0),
                new SourceRectangle(130, 0, 56, 37, 6, 0),
                new SourceRectangle(121, 37, 47, 37, 6, 0),
                new SourceRectangle(0, 75, 36, 37, 6, 0),
                new SourceRectangle(36, 75, 36, 37, 6, 0),
                new SourceRectangle(72, 74, 27, 37, 4, 0),
                new SourceRectangle(99, 74, 35, 37, 3, 0),
                new SourceRectangle(134, 74, 34, 37, 2, 0)
            ],
            0.12
        );
    }
    override public void Update(GameTime gameTime)
    {
        Enabled = GetChosenSprite() == 1;
    }
}
