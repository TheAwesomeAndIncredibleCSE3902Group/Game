using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public class NonMovingNonAnimatedSprite : SpriteBase
{
    public NonMovingNonAnimatedSprite(SpriteBatch spriteBatch, Texture2D spriteTexture, Func<int> GetChosenSpriteFunc)
    {
        GetChosenSprite = GetChosenSpriteFunc;
        SetUpSprite(
            spriteBatch,
            spriteTexture,
            new Point(400, 200),
            new Vector2(4.0f, 4.0f),
            [
                new SourceRectangle(0, 112, 21, 37, 0, 0)
            ],
            0
        );
    }
    override public void Update(GameTime gameTime)
    {
        Enabled = GetChosenSprite() == 0;
    }
}
