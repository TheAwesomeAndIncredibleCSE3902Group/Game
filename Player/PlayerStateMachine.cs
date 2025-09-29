using System;
using Microsoft.Xna.Framework;
using static Sprint0.Util;

namespace Sprint0;

public class PlayerStateMachine : IPlayerState
{

    private IPlayer currentPlayer;
    private Cardinal currentDirection;
    private int currentHealth;
    private int currentMaxHealth;
    private IEquipment activeEquipment;

    public enum States { Standing, Walking, SwordAttack, ItemUse, Damaged };
    private States currentState;
    private float linkVelocity;


    public PlayerStateMachine()
    {
        currentDirection = Util.Cardinal.down;
        currentState = States.Standing;
        currentHealth = 3;
        currentMaxHealth = currentHealth;
        linkVelocity = 0;
    }

    public Cardinal Direction
    {
        get
        {
            return currentDirection;
        }
        set
        {
            currentDirection = value;
        }
    }

    public States getState()
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

    public void ChangeDirection(Cardinal direction)
    {
        currentDirection = direction;
    }

    public void ChangeStateStanding()
    {
        currentState = States.Standing;
    }

    public void ChangeStateWalking()
    {
        currentState = States.Walking;
    }

    public void ChangeStateSwordAttack()
    {
        currentState = States.SwordAttack;
    }

    public void ChangeStateItemUse()
    {
        currentState = States.ItemUse;
    }

    public void ChangeStateDamaged()
    {
        currentState = States.Damaged;
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

    public void TakeDamage(int amount = 1)
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

    // figure out soon
    public void Update(GameTime gt, Cardinal newDirection)
    {

    }
}