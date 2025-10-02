using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using System;
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

    private Texture2D linkSpriteSheet;
    private SpriteBatch linkSpriteBatch;
    private ISprite linkSprite;
    private Rectangle currentFrame;
    private int rectangleXPosition;
    private int rectangleYPosition;
    private int rectangleWidth;
    private int rectangleHeight;
    public PlayerStateMachine()
    {
        currentDirection = Util.Cardinal.down;
        currentState = States.Standing;
        currentHealth = 3;
        currentMaxHealth = currentHealth;
        linkVelocity = 0;

        // Link, on the legendofzelda_link_sheet.png, is 15 by 15 pixels in size
        rectangleXPosition = 0;
        rectangleYPosition = 0;
        rectangleWidth = 15;
        rectangleHeight = 15;

        currentFrame = new Rectangle(rectangleXPosition, rectangleYPosition, rectangleWidth, rectangleHeight);
    }

    public void LoadPlayer(ContentManager content, SpriteBatch spriteBatch)
    {
        linkSpriteSheet = content.Load<Texture2D>("SpriteImages/legendofzelda_link_sheet");
        linkSpriteBatch = spriteBatch;
        linkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, currentFrame); 
        // If Link could be scaled, he should be scaled by a factor of 3.0f - 5.0f so that he can be visible on screen.
        // Preferably 3.0f.
    }

    public Cardinal Direction
    {
        get
        {
            return currentDirection;
        }
    }

    public ISprite GetSprite()
    {
        return linkSprite;
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
        switch (currentDirection)
        {
            case Cardinal.down:
                switch (newDirection)
                {
                    case Cardinal.left: rectangleXPosition += 30; break;
                    case Cardinal.up: rectangleXPosition += 60; break;
                    case Cardinal.right: rectangleXPosition += 90; break;
                }
                break;

            case Cardinal.left:
                switch (newDirection)
                {
                    case Cardinal.down: rectangleXPosition -= 30; break;
                    case Cardinal.up: rectangleXPosition += 30; break;
                    case Cardinal.right: rectangleXPosition += 60; break;
                }
                break;

            case Cardinal.up:
                switch (newDirection)
                {
                    case Cardinal.down: rectangleXPosition -= 60; break;
                    case Cardinal.left: rectangleXPosition -= 30; break;
                    case Cardinal.right: rectangleXPosition += 30; break;
                }
                break;

            case Cardinal.right:
                switch (newDirection)
                {
                    case Cardinal.down: rectangleXPosition -= 90; break;
                    case Cardinal.left: rectangleXPosition -= 60; break;
                    case Cardinal.up: rectangleXPosition -= 30; break;
                }
                break;
        }

        currentDirection = newDirection;
    }

    public void ChangeStateStanding()
    {
        if ((currentDirection == Cardinal.left || currentDirection == Cardinal.right) && currentState == States.SwordAttack)
        {
            rectangleXPosition += 6;
        }
        rectangleYPosition = 0;
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
                if (rectangleYPosition == 30)
                {
                    rectangleYPosition -= 30;
                }
                else
                {
                    rectangleYPosition += 30;
                }
                break;

            case States.SwordAttack:
                // figure out recangle dimensions soon
                if (currentDirection == Cardinal.up || currentDirection == Cardinal.down)
                {
                    rectangleYPosition = 84;
                    rectangleWidth = 15;
                    rectangleHeight = 28;
                }
                else
                {
                    rectangleYPosition = 90;
                    rectangleWidth = 28;
                    rectangleHeight = 15;

                    if (currentDirection == Cardinal.left)
                    {
                        rectangleXPosition = 24;
                    }
                    else
                    {
                        rectangleXPosition = 84;
                    }
                }
                break;

            case States.ItemUse:
                if (rectangleYPosition == 60)
                {
                    rectangleYPosition -= 60;
                }
                else
                {
                    rectangleYPosition += 60;
                }
                break;

            case States.Damaged:
                // decorator type stuff goes here to 'damage' Link
                break;

            default:
                rectangleWidth = 15;
                rectangleHeight = rectangleWidth;
                break; // if Link is standing, this resets Link to standing normally in his 15 by 15 spritemap
        }
        currentFrame = new Rectangle(rectangleXPosition, rectangleYPosition, rectangleWidth, rectangleHeight);
        linkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, currentFrame);
    }

    public void Draw(GameTime gt, Vector2 position)
    {
        linkSprite.Draw(gt,position);
    }
}