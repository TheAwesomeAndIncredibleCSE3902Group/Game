using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using System;
using System.Diagnostics;
using static AwesomeRPG.Util;
using static AwesomeRPG.Player;

namespace AwesomeRPG;

public class PlayerStateMachine
{

    private IPlayer currentPlayer;
    private Cardinal currentDirection;
    private int currentHealth;
    private int currentMaxHealth;
    public enum States { Standing, Walking, SwordAttack, ItemUse, Damaged };
    private States currentState;
    private States previousState;
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
        spriteFactory.ChangeSpriteStanding();
    }

    public Cardinal Direction
    {
        get
        {
            return currentDirection;
        }
    }

    public States GetCurrentState()
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
        ChangeStateStanding();
        spriteFactory.ChangeDirection(newDirection);
        currentDirection = newDirection;
    }

    public void ChangeStateStanding()
    {
        /*
        if(currentState == States.SwordAttack)
            spriteFactory.ChangeSpriteStanding();
        */
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
            Console.WriteLine("State changed to damaged");
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

    /// <summary>
    /// Updates the drawn sprite of Link if the previous state is different than the current state.
    /// Otherwise this keeps the same sprite state drawn of Link.
    /// </summary>
    /// <param name="gt"> The game time of the current game where Link is playable in</param>
    public void Update(GameTime gt)
    {
        if (currentState != previousState)
        {
            switch (currentState)
            {
                case States.Walking:
                    spriteFactory.ChangeSpriteWalking();
                    break;

                case States.ItemUse:
                    spriteFactory.ChangeSpriteItemUse();
                    break;

                case States.Damaged:
                    Console.WriteLine("Spritefactory called");
                    spriteFactory.ChangeSpriteDamaged();
                    break;

                case States.Standing:
                    spriteFactory.ChangeSpriteStanding();
                    break;
            }
            previousState = currentState;
        }
    }
    public void Draw(GameTime gt, Vector2 position)
    {
        spriteFactory.Draw(gt,position);
    }
}