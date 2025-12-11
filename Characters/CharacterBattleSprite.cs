using System;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using static AwesomeRPG.Characters.CharacterEnemyBase;
using AwesomeRPG.UI;

namespace AwesomeRPG.Characters;

public class CharacterBattleSprite
{
    public bool Hurt
    {
        get => hurt;
        set
        {
            hurt = value;
            Sprite.Color = hurt ? new Color(255, 120, 120) : Color.White;
        }
    }
    private bool hurt = false;
    public AnimatableSprite Sprite { get; private set; }
    public Element Element { get; set; }
    private Vector2 Position { get; set; }
    private CType type;


    public CharacterBattleSprite(CType type, Vector2 pos)
    {
        this.type = type;
        Position = pos;
        SetSprite();
        Sprite.RandomizeAnimationStart();
    }

    public void Draw(GameTime gameTime)
    {
        Sprite.Draw(gameTime, Position);
    }

    /// <summary>
    /// Right now this simply gets the Down sprite for this enemy type
    ///     In the future it could be expanded to switch to Attacking sprites
    /// </summary>
    /// <param name="attacking"></param>
    public void SetSprite(bool attacking = false)
    {
        EnemySpriteFactory csf = EnemySpriteFactory.Instance;
        Sprite = type switch
        {
            CType.armos => csf.ArmosSpriteDown(),
            CType.lynel => csf.LynelSpriteDown(),
            CType.moblin => csf.MoblinSpriteDown(),
            CType.boss => csf.BossSprite(),
            _ => csf.ArmosSpriteDown()
        };

        Sprite.MillisecondsBetweenFrames = (ulong)Util.BattleStaticAnimationMilliseconds;
        Sprite.SetScale(Util.BattleScale);
    }
}
