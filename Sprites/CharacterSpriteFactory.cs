using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.Sprites;


public class CharacterSpriteFactory
{
    private Texture2D _enemySpriteSheet;
    private Texture2D _krisSpriteSheet;
    private SpriteBatch _spriteBatch;

    private static CharacterSpriteFactory instance = new CharacterSpriteFactory();

    public static CharacterSpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }
    private CharacterSpriteFactory()
    {

    }

    public void LoadAllTextures(ContentManager content, SpriteBatch spriteBatch)
    {
        _enemySpriteSheet = content.Load<Texture2D>("SpriteImages/enemy_sprites");
        _krisSpriteSheet = content.Load<Texture2D>("SpriteImages/kris_custom");
        _spriteBatch = spriteBatch;
    }

    public AnimatableSprite MoblinSprite()
    {
        Rectangle sourceRec = new Rectangle(0, 0, 16, 16);
        return new AnimatableSprite(_spriteBatch, _enemySpriteSheet, sourceRec, 4, 250, new Point(0,0));
    }

    public AnimatableSprite ArmosSprite()
    {
        Rectangle sourceRec = new Rectangle(0, 16, 16, 16);
        return new AnimatableSprite(_spriteBatch, _enemySpriteSheet, sourceRec, 2, 250, new Point(0,0));
    }

    public AnimatableSprite LynelSprite()
    {
        Rectangle sourceRec = new Rectangle(0, 32, 16, 16);
        return new AnimatableSprite(_spriteBatch, _enemySpriteSheet, sourceRec, 2, 250, new Point(0,0));
    }

    public AnimatableSprite KrisSprite()
    {
        Rectangle sourceRec = new Rectangle(21, 119, 32, 32);
        return new AnimatableSprite(_spriteBatch, _krisSpriteSheet, sourceRec);
    }
}