using System;
using System.Formats.Asn1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Commands.SwitchMapItemSpriteCommand;

namespace AwesomeRPG;

/// <summary>
/// Factory to make sprites for all Items, Projectiles, and etc.
/// Technically can make any sprite, but it seems wise to limit its scope.
/// </summary>
public static class ItemSpriteFactory
{
    private static Texture2D zeldaSheet;
    private static Texture2D miscSheet;
    //Having a singular, non-changing SpriteBatch for each sprite may not be ideal. But any alternative requires more coupling with Game.
    private static SpriteBatch sb;

    /// <summary>
    /// Creates an arrow sprite in the direction given as the argument
    /// </summary>
    /// <param name="direction">Direction to direct sprite</param>
    /// <returns></returns>
    public static ISprite CreateArrowSprite(Util.Cardinal direction)
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
        
        return new AnimatableSprite(sb, zeldaSheet, sourceRec);
    }

    /// <summary>
    /// Creates an sword sprite in the direction given as the argument
    /// </summary>
    /// <param name="direction">Direction to direct sprite</param>
    /// <returns></returns>
    public static ISprite CreateSwordSprite(Util.Cardinal direction)
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
        return new AnimatableSprite(sb, zeldaSheet, sourceRec);
    }

    /// <summary>
    /// Creates an swordbeam sprite in the direction given as the argument
    /// </summary>
    /// <param name="direction">Direction to direct sprite</param>
    /// <returns></returns>
    public static ISprite CreateSwordBeamSprite(Util.Cardinal direction)
    {
        Rectangle sourceRec;
        switch (direction)
        {
            case Util.Cardinal.down:
                sourceRec = new Rectangle(315, 173, 16, 12);
                break;
            case Util.Cardinal.up:
                sourceRec = new Rectangle(315, 151, 16, 12);
                break;
            case Util.Cardinal.left:
                sourceRec = new Rectangle(300, 160, 12, 16);
                break;
            default:
                sourceRec = new Rectangle(334, 160, 12, 16);
                break;
        }
        return new AnimatableSprite(sb, zeldaSheet, sourceRec);
    }

    /// <summary>
    /// Creates a boomerang sprite which then rotates
    /// </summary>
    /// <returns></returns>
    public static ISprite CreateBoomerangSprite()
    {
        Rectangle sourceRec = new Rectangle(70, 40, 8, 8);
        uint frameCount = 4;
        ulong msDelay = 125;
        Point gapSize = new Point(2, 0);
        return new AnimatableSprite(sb, miscSheet, sourceRec, frameCount, msDelay,gapSize);
    }

    public static ISprite CreateFireSprite()
    {
        Rectangle sourceRec = new Rectangle(64, 80, 16, 16);
        uint frameCount = 2;
        ulong msDelay = 125;
        return new AnimatableSprite(sb, miscSheet, sourceRec, frameCount, msDelay, null);
    }

    public static void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
    {
        zeldaSheet = content.Load<Texture2D>("SpriteImages/legendofzelda_link_sheet");
        miscSheet = content.Load<Texture2D>("SpriteImages/misc_items");
        sb = spriteBatch;

        //Use this if/once we have a dedicated sprite sheet for items
        //spriteSheet = content.Load<Texture2D>("items");
    }

}
