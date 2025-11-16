using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using System;
using AwesomeRPG.Map;
using System.Diagnostics;

namespace AwesomeRPG.Characters;

public class CharacterEnemyMoblin : CharacterEnemyBase
{
    //Time is tracked in milliseconds
    private int attackCD;
    private int timeTillAttack;
    private const int recoverCD = 1000;
    private int timeTillRecover;
    public CharacterEnemyMoblin(Vector2 position, Cardinal direction) : base(position, direction)
    {
        const int minCD = 2000; //2 seconds
        const int maxCD = 4000; // 4 seconds
        attackCD = new Random().Next(minCD,maxCD);
        timeTillAttack = attackCD;
        timeTillRecover = recoverCD;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (_moving)
        {
            HandleAttacking(gameTime);
        }//TODO: NASTY
        else
        {
            HandleRecovery(gameTime);
        }
    }

    private bool DecrementTimer(GameTime gameTime, ref int time)
    {
        bool finishedCD = false;
        time -= gameTime.ElapsedGameTime.Milliseconds;
        if(time <= 0)
        {
            finishedCD = true;
        }
        return finishedCD;
    }

    private void HandleAttacking(GameTime gameTime)
    {
        if (DecrementTimer(gameTime, ref timeTillAttack))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _moving = false;
        timeTillRecover = recoverCD;
        ChangeAttackDirectionalSprite(Direction);
        RoomAtlas.Instance.AddProjectile(new MoblinFire(Position,Direction));
    }

    private void HandleRecovery(GameTime gameTime)
    {
        if (DecrementTimer(gameTime, ref timeTillRecover))
        {
            Recover();
        }
    }

    private void Recover()
    {
        _moving = true;
        timeTillAttack = attackCD;
        ChangeMovingDirectionalSprite(Direction);
    }

    #region Sprite Swapping
    private void ChangeMovingDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => CharacterSpriteFactory.Instance.MoblinSpriteUp(),
            Cardinal.down => CharacterSpriteFactory.Instance.MoblinSpriteDown(),
            Cardinal.left => CharacterSpriteFactory.Instance.MoblinSpriteLeft(),
            Cardinal.right => CharacterSpriteFactory.Instance.MoblinSpriteRight(),
            _ => CharacterSpriteFactory.Instance.MoblinSpriteDown()
        };
    }

    private void ChangeAttackDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => CharacterSpriteFactory.Instance.MoblinAttackSpriteUp(),
            Cardinal.down => CharacterSpriteFactory.Instance.MoblinAttackSpriteDown(),
            Cardinal.left => CharacterSpriteFactory.Instance.MoblinAttackSpriteLeft(),
            Cardinal.right => CharacterSpriteFactory.Instance.MoblinAttackSpriteRight(),
            _ => CharacterSpriteFactory.Instance.MoblinAttackSpriteDown()
        };
    }

    public override void ChangeDirectionalSprite(Cardinal direction)
    {
        if (_moving)
        {
            ChangeMovingDirectionalSprite(direction);
        }
        else
        {
            ChangeAttackDirectionalSprite(direction);
        }
    }
    #endregion

}