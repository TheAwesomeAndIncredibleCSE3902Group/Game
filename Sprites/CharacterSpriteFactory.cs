using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Tiles;


public class CharacterSpriteFactory
{
    private Texture2D _itemSpriteSheet;
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
        _itemSpriteSheet = content.Load<Texture2D>("TODO");// TODO!!!!
        _spriteBatch = spriteBatch;
    }

    public ISprite KrisSprite(ContentManager content, SpriteBatch spriteBatch)
    {
        _itemSpriteSheet = content.Load<Texture2D>("TODO");// TODO!!!!
        _spriteBatch = spriteBatch;
    }
    
}