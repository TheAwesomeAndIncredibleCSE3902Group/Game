using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0;

/// <summary>
/// Factory to make sprites for all Items, Projectiles, and etc.
/// Technically can make any sprite, but it seems wise to limit its scope.
/// </summary>
public static class ItemSpriteFactory
{
    private static Texture2D spriteSheet;
    //Having a singular, non-changing SpriteBatch for each sprite may not be ideal. But any alternative requires more coupling with Game.
    private static SpriteBatch sb;

    public static ISprite CreateArrowSprite()
    {
        Rectangle sourceRec = new Rectangle(172, 4, 6, 4);
        return new AnimatableSprite(sb, spriteSheet, sourceRec);
    }

    public static ISprite CreateLinkSprite()
    {
        Rectangle sourceRec = new Rectangle(0, 0, 15, 15);
        return new AnimatableSprite(sb, spriteSheet, sourceRec);
    }

    public static void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
    {
        spriteSheet = content.Load<Texture2D>("SpriteImages/legendofzelda_link_sheet");
        sb = spriteBatch;

        //Use this if/once we have a dedicated sprite sheet for items
        //spriteSheet = content.Load<Texture2D>("items");
    }

}
