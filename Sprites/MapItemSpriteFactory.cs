using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public class MapItemSpriteFactory
{
    private static Texture2D _itemSpriteSheet;
    private static SpriteBatch _spriteBatch;

    private static MapItemSpriteFactory instance = new MapItemSpriteFactory();

    public static void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
    {
        _itemSpriteSheet = content.Load<Texture2D>("SpriteImages/misc_items");
        _spriteBatch = spriteBatch;
    }

    public static ISprite CreateCandleSprite()
    {
        Rectangle candleSourceRect = new Rectangle(160, 0, 8, 16);
        return new AnimatableSprite(_spriteBatch, _itemSpriteSheet, candleSourceRect);
    }

    public static ISprite CreatePotionSprite()
    {
        Rectangle potionSourceRect = new Rectangle(80, 0, 8, 16);
        return new AnimatableSprite(_spriteBatch, _itemSpriteSheet, potionSourceRect);
    }

    public static ISprite CreateRupeeSprite()
    {
        Rectangle rupeeSourceRect = new Rectangle(72, 0, 8, 16);
        return new AnimatableSprite(_spriteBatch, _itemSpriteSheet, rupeeSourceRect);
    }

}

