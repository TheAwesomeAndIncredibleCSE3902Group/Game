using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using System;

namespace AwesomeRPG.Characters;

public class CharacterEnemyMoblin : CharacterEnemyBase
{
    //Time is tracked in milliseconds
    private int attackCD;
    private int remainingTime;
    public CharacterEnemyMoblin(Vector2 position, Cardinal direction) : base(position, direction)
    {
        int minCD = 2000; //2 seconds
        int maxCD = 4000; // 4 seconds
        attackCD = new Random().Next(minCD,maxCD);
        remainingTime = attackCD;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(_moving) HandleAttacking(gameTime); //TODO: NASTY
    }

    private bool CheckCooldown(GameTime gameTime)
    {
        bool finishedCD = false;
        remainingTime -= gameTime.ElapsedGameTime.Milliseconds;
        if(remainingTime <= 0)
        {
            finishedCD = true;
        }
        return finishedCD;
    }

    private void HandleAttacking(GameTime gameTime)
    {
        if(CheckCooldown(gameTime))
        {
            Attack();
        }
    }

    private void Attack()
    {
        //Necesito mi universal projectileList antes de completar.
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