using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using static Sprint0.Commands.CommandSwitchMapItemSprite;

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

    public static ISprite CreateDirectionalArrowSprite(Util.Cardinal direction)
    {
        Rectangle sourceRec;
        switch (direction)
        {
            case Util.Cardinal.down:
                sourceRec = new Rectangle(125, 195, 5, 16);
                break;
            case Util.Cardinal.up:
                sourceRec = new Rectangle(185,195, 5, 16);
                break;
            case Util.Cardinal.left:
                sourceRec = new Rectangle(150, 200, 16, 5);
                break;
            default:
                sourceRec = new Rectangle(210, 200, 16, 5);
                break;
        }
        
        return new AnimatableSprite(sb, spriteSheet, sourceRec);
    }

    public static ISprite CreateDirectionalSwordSprite(Util.Cardinal direction)
    {
        Rectangle sourceRec;
        switch (direction)
        {
            case Util.Cardinal.down:
                sourceRec = new Rectangle(4, 255, 7, 16);
                break;
            case Util.Cardinal.up:
                sourceRec = new Rectangle(64, 255, 7, 16);
                break;
            case Util.Cardinal.left:
                sourceRec = new Rectangle(30, 259, 16, 7);
                break;
            default:
                sourceRec = new Rectangle(90, 259, 16, 7);
                break;
        }
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
