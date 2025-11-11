using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

public class PlayerStateMachine
{

    private Cardinal currentDirection;
    private int currentHealth;
    private int currentMaxHealth;
    public float currentDamageIntake { get; set; }
    public enum States { Standing, Walking, SwordAttack, ItemUse, Damaged };
    private States currentState;
    private States previousState;
    private PlayerSpriteFactory spriteFactory;
    private float timeSinceStateChange;
    private const float hurtTime = 0.5f;
    private const float itemUseTime = 0.5f;

    public PlayerStateMachine()
    {
        currentDirection = Util.Cardinal.down;
        currentState = States.Standing;
        currentHealth = 3;
        currentMaxHealth = currentHealth;
        currentDamageIntake = 1;

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
        if (currentState == States.Standing || currentState == States.Damaged || currentState == States.Walking)
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
        currentHealth -= (int)(amount * currentDamageIntake);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public static void UseEquipment(IEquipment.Weapons weapon)
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
            UpdateSprite();
            previousState = currentState;
            timeSinceStateChange = 0;
        }
        else
            AdvanceStateTime(gt);
    }

    /// <summary>
    /// Handles the logic for all time-limited states. Also handles the transition from Walking to Standing
    /// </summary>
    /// <param name="gt"></param>
    private void AdvanceStateTime(GameTime gt)
    {
        timeSinceStateChange += (float)gt.ElapsedGameTime.TotalSeconds;

        if (currentState == States.Damaged && timeSinceStateChange > hurtTime)
            ChangeStateStanding();

        //This is coupled with Player, plus requires the correct order between the Controllers and This. Neither of which do I love
        else if (currentState == States.Walking && !Player.Instance.HasMovedThisFrame)
            ChangeStateStanding();

        else if (currentState == States.ItemUse && timeSinceStateChange > itemUseTime)
            ChangeStateStanding();
    }

    //This could be a Dictionary if we wanted to collapse this a bit
    private void UpdateSprite()
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
                spriteFactory.ChangeSpriteDamaged();
                break;

            case States.Standing:
                spriteFactory.ChangeSpriteStanding();
                break;
        }
    }
    
    public void Draw(GameTime gt, Vector2 position)
    {
        spriteFactory.Draw(gt,position);
    }
}