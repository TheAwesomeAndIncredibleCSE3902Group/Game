using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Sprites;

public class TeamSpriteFactory
{
    private static Texture2D _teamSpriteSheet;
    private static SpriteBatch _spriteBatch;

    public static TeamSpriteFactory Instance = new TeamSpriteFactory();
    private TeamSpriteFactory() { }

    public void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
    {
        _teamSpriteSheet = content.Load<Texture2D>("SpriteImages/teammates");
        _spriteBatch = spriteBatch;
    }

    public static ISprite CreateZeldaSprite()
    {
        Rectangle zeldaSourceRect = new Rectangle(0, 0, 16, 16);
        return new AnimatableSprite(_spriteBatch, _teamSpriteSheet, zeldaSourceRect);
    }

    public static ISprite CreateMerchantSprite()
    {
        Rectangle merchantSourceRect = new Rectangle(16, 0, 16, 16);
        return new AnimatableSprite(_spriteBatch, _teamSpriteSheet, merchantSourceRect);
    }

    public static ISprite CreateOldSprite()
    {
        Rectangle oldSourceRect = new Rectangle(32, 0, 16, 16);
        return new AnimatableSprite(_spriteBatch, _teamSpriteSheet, oldSourceRect);
    }

}

