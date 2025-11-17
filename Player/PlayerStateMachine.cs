using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

public class PlayerStateMachine
{
    public Cardinal Direction { get; private set; }
    public enum States { Standing, Walking, SwordAttack, ItemUse, Damaged };
    private States currentState;
    private States previousState;
    private PlayerSpriteFactory spriteFactory;
    private float timeSinceStateChange;
    private const float hurtTime = 0.5f;
    private const float itemUseTime = 0.5f;

    public PlayerStateMachine()
    {
        Direction = Cardinal.down;
        currentState = States.Standing;

        spriteFactory = new PlayerSpriteFactory();
    }

    public void LoadPlayer(ContentManager content, SpriteBatch spriteBatch)
    {
        spriteFactory.LoadPlayer(content, spriteBatch);
        spriteFactory.ChangeDirection(Direction);
        spriteFactory.ChangeSpriteStanding();
    }

    public States GetCurrentState()
    {
        return currentState;
    }

    public void ChangeDirection(Cardinal newDirection)
    {
        spriteFactory.ChangeDirection(newDirection);
        Direction = newDirection;
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

    public static void UseEquipment(Weapons weapon)
    {
        PlayerOverworld.Instance.Equipment[weapon].Use();
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
        else if (currentState == States.Walking && !PlayerOverworld.Instance.HasMovedThisFrame)
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