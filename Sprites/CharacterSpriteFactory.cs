using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using System.Reflection.Metadata.Ecma335;

namespace AwesomeRPG.Sprites;


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

    /*
    Enemy sprites are ordered in this way:
        Each frame is 16x16
        No spacing horizontally (ie for different frames and directions of same enemy)
        One pixel of spacing between different enemies
        Horizontal ordering is right1, right2, left1, left2, down1, down2, up1, up2
            Armos has only down and up sprites. Those are alligned to the left
            Kris is on the other sheet
    */

    public AnimatableSprite MoblinSpriteRight() => BuildEnemySprite(new Rectangle(0, 0, 16, 16));
    public AnimatableSprite MoblinSpriteLeft() => BuildEnemySprite(new Rectangle(32, 0, 16, 16));
    public AnimatableSprite MoblinSpriteDown() => BuildEnemySprite(new Rectangle(64, 0, 16, 16));
    public AnimatableSprite MoblinSpriteUp() => BuildEnemySprite(new Rectangle(96, 0, 16, 16));
    public AnimatableSprite MoblinAttackSpriteRight() => BuildEnemySprite(new Rectangle(0, 64, 16, 16));
    public AnimatableSprite MoblinAttackSpriteLeft() => BuildEnemySprite(new Rectangle(32, 64, 16, 16));
    public AnimatableSprite MoblinAttackSpriteDown() => BuildEnemySprite(new Rectangle(64, 64, 16, 16));
    public AnimatableSprite MoblinAttackSpriteUp() => BuildEnemySprite(new Rectangle(96, 64, 16, 16));

    public AnimatableSprite ArmosSpriteDown() => BuildEnemySprite(new Rectangle(0, 17, 16, 16));
    public AnimatableSprite ArmosSpriteUp() => BuildEnemySprite(new Rectangle(32, 17, 16, 16));

    public AnimatableSprite LynelSpriteRight() => BuildEnemySprite(new Rectangle(0, 34, 16, 16));
    public AnimatableSprite LynelSpriteLeft() => BuildEnemySprite(new Rectangle(32, 34, 16, 16));
    public AnimatableSprite LynelSpriteDown() => BuildEnemySprite(new Rectangle(64, 34, 16, 16));
    public AnimatableSprite LynelSpriteUp() => BuildEnemySprite(new Rectangle(96, 34, 16, 16));

    public AnimatableSprite KrisSprite()
    {
        Rectangle sourceRec = new Rectangle(21, 119, 32, 32);
        return new AnimatableSprite(_spriteBatch, _krisSpriteSheet, sourceRec);
    }

    /// <summary>
    /// Builds a sprite with 2 frames and no spacing, starting at sourceRec
    /// </summary>
    /// <param name="sourceRec"></param>
    /// <returns></returns>
    private AnimatableSprite BuildEnemySprite(Rectangle sourceRec) => new(_spriteBatch, _enemySpriteSheet, sourceRec, 2, 250, new Point(0,0));
}