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
    public enum States { Standing, Walking, SwordAttack, ItemUse, Damaged };
    private States currentState;
    private PlayerSpriteFactory spriteFactory;

    public PlayerStateMachine()
    {
        currentDirection = Util.Cardinal.down;
        currentState = States.Standing;
        currentHealth = 3;
        currentMaxHealth = currentHealth;

        spriteFactory = new PlayerSpriteFactory();
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public void UseEquipment(IEquipment.Weapons weapon)
    {
        Player.Instance.Equipment[weapon].Use();
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