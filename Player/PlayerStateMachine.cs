using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using System;
using System.Diagnostics;
using static Sprint0.Util;
using static Sprint0.Player;

namespace Sprint0;

public class PlayerStateMachine
{

    private IPlayer currentPlayer;
    private Cardinal currentDirection;
    private int currentHealth;
    private int currentMaxHealth;
    private IEquipment activeEquipment;
    public enum States { Standing, Walking, SwordAttack, ItemUse, Damaged };
    private States currentState;
    private PlayerSpriteFactory spriteFactory;

    private float linkVelocity;

    public PlayerStateMachine()
    {
        currentDirection = Util.Cardinal.down;
        currentState = States.Standing;
        currentHealth = 3;
        currentMaxHealth = currentHealth;
        linkVelocity = 0;

        spriteFactory = new PlayerSpriteFactory();
        //TODO: REMOVE THIS, TEST FOR ARROW SPAWNING
        activeEquipment = Player.Instance.Equipment[0];
    }

    public void LoadPlayer(ContentManager content, SpriteBatch spriteBatch)
    {
        spriteFactory.LoadPlayer(content, spriteBatch);
        spriteFactory.ChangeDirection(currentDirection);
    }

    public Cardinal Direction
    {
        get
        {
            return currentDirection;
        }
    }

    public States GetState()
    {
        return currentState;
    }

    public int Health
    {
        get
        {
            return currentHealth;
        }
    }

    public int MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
    }

    public void ChangeDirection(Cardinal newDirection)
    {
        spriteFactory.ChangeDirection(newDirection);
        currentDirection = newDirection;
    }

    public void ChangeStateStanding()
    {
        if(currentState == States.SwordAttack)
            spriteFactory.ChangeSpriteStanding();
        currentState = States.Standing;
    }

    public void ChangeStateWalking()
    {
        if (currentState == States.Standing)
        {
            currentState = States.Walking;
        }
    }

    public void ChangeStateSwordAttack()
    {
        if (currentState == States.Standing)
        {
            currentState = States.SwordAttack;
        }
    }

    public void ChangeStateItemUse()
    {
        if (currentState == States.Standing)
        {
            currentState = States.ItemUse;
        }
    }

    public void ChangeStateDamaged()
    {
        if (currentState == States.Standing)
        {
            currentState = States.Damaged;
        }
    }

    //Equipment might not need to be part of state, but it does drive the sprite, so I put it here for now
    public IEquipment ActiveEquipment
    {
        get
        {
            return activeEquipment;
        }
        set
        {
            activeEquipment = value;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public void UseEquipment()
    {
        activeEquipment.Use();
    }

    public void Update(GameTime gt)
    {
        switch (currentState)
        {
            //case States.Standing: break;
            case States.Walking:
                spriteFactory.ChangeSpriteWalking();
                break;

            case States.Standing:
                spriteFactory.ChangeSpriteStanding();
                break;

            case States.Damaged:
                spriteFactory.ChangeSpriteDamaged();
                break;

            default:
                spriteFactory.ChangeSpriteItemUse();
                break;
        }
    }
    public void Draw(GameTime gt, Vector2 position)
    {
        spriteFactory.Draw(gt,position);
    }
}