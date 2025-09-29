using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.Sprites;


public class CharacterSpriteFactory
{
    private Texture2D _enemySpriteSheet;
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
        _spriteBatch = spriteBatch;
    }

    public AnimatableSprite MoblinSprite()
    {
        Rectangle sourceRec = new Rectangle(0, 0, 16, 16);
        return new AnimatableSprite(_spriteBatch, _enemySpriteSheet, sourceRec);
    }
    


    
}