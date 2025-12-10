using System;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using static AwesomeRPG.Characters.CharacterEnemyBase;

namespace AwesomeRPG.Characters;

public class CharacterBattleSprite
{
    public bool Hurt
    {
        get => hurt;
        set
        {
            hurt = value;
            sprite.Color = hurt ? new Color(255, 120, 120) : Color.White;
        }
    }
    private bool hurt = false;
    private AnimatableSprite sprite;
    private Vector2 Position { get; set; }
    private CType type;


    public CharacterBattleSprite(CType type, Vector2 pos)
    {
        this.type = type;
        Position = pos;
        SetSprite();
        sprite.RandomizeAnimationStart();
    }

    public void Draw(GameTime gameTime)
    {
        sprite.Draw(gameTime, Position);
    }

    /// <summary>
    /// Right now this simply gets the Down sprite for this enemy type
    ///     In the future it could be expanded to switch to Attacking sprites
    /// </summary>
    /// <param name="attacking"></param>
    public void SetSprite(bool attacking = false)
    {
        EnemySpriteFactory csf = EnemySpriteFactory.Instance;
        sprite = type switch
        {
            CType.armos => csf.ArmosSpriteDown(),
            CType.lynel => csf.LynelSpriteDown(),
            CType.moblin => csf.MoblinSpriteDown(),
            CType.boss => csf.BossSprite(),
            _ => csf.ArmosSpriteDown()
        };

        sprite.MillisecondsBetweenFrames = (ulong)Util.BattleStaticAnimationMilliseconds;
        sprite.SetScale(Util.BattleScale);
    }
}
