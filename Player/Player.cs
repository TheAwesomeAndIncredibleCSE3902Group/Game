using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using AwesomeRPG.Collision;
using System.Reflection.Metadata.Ecma335;

namespace AwesomeRPG;

public class Player : CollisionObject
{
    //Singleton pattern seems acceptable for the player
    public static Player Instance { get; private set; }
    public Cardinal FacingDirection => PStateMachine.Direction;
    public PlayerStateMachine PStateMachine { get; private set; }

    public Dictionary<IEquipment.Weapons, IEquipment> Equipment { get; } = new();
    public Dictionary<IEquipment.Projectiles, Projectile> spawnedProjectiles { get; set; } = new();

    //Whether this has moved yet this frame. Please be careful of any timing issues / race conditions with the Controllers.
    public bool HasMovedThisFrame { get; set; }

    //In pixels per second
    private float movementSpeed = 240;
    //Cache a reference to GameTime for movement. Gets updated at every Update() call
    private GameTime gt = new GameTime();


    public Player(ContentManager content, SpriteBatch _spriteBatch)
    {
        Instance = this;
        InitializeEquipment();
        //Throwing in a random position so the sprite isn't halfway off the screen or something
        Vector2 startingPos = new Vector2(500, 250);
        Position = startingPos;

        int spriteSize = 15;
        Collider = new CollisionRect(this, spriteSize * GlobalScale, spriteSize * GlobalScale);
        ObjectType = CollisionObjectType.Player;

        PStateMachine = new PlayerStateMachine();
        PStateMachine.LoadPlayer(content, _spriteBatch);
    }

    public void Draw(GameTime gt)
    {
        PStateMachine.Draw(gt, Position);

        foreach (Projectile projectile in spawnedProjectiles.Values)
        {
            projectile.Draw(gt);
        }
    }

    public void Update(GameTime gt)
    {
        this.gt = gt;
        HasMovedThisFrame = false;

        foreach (Projectile projectile in spawnedProjectiles.Values)
        {
            projectile.Update(gt);
        }

        PStateMachine.Update(gt);
    }


    /// <summary>
    /// This is the target for any Move Command. 
    /// Requests IPlayerState change direction and change to WalkingState if necessary, and finally updates position.
    /// </summary>
    public void Move(Cardinal direction)
    {
        if (HasMovedThisFrame)
            return;

        PStateMachine.ChangeStateWalking();

        PlayerStateMachine.States newState = PStateMachine.GetCurrentState();
        if (newState == PlayerStateMachine.States.Walking 
            || newState == PlayerStateMachine.States.Damaged 
            || newState == PlayerStateMachine.States.ItemUse)
        {
            if (PStateMachine.Direction != direction)
                PStateMachine.ChangeDirection(direction);

            //Grabbing the direction from PlayerState here ensures that IPlayerState is the ultimate authority
            Cardinal newDirection = PStateMachine.Direction;
            Position += (float)(gt.ElapsedGameTime.TotalSeconds * movementSpeed) * Util.CardinalToUnitVector(newDirection);
            //Position += movementSpeed * Util.CardinalToUnitVector(newDirection);
        }

        HasMovedThisFrame = true;
    }

    public void TakeDamage(int amount = 1)
    {
        PStateMachine.ChangeStateDamaged();
    }

    public void UseEquipment(IEquipment.Weapons requestedEQ)
    {
        if (!Equipment.TryGetValue(requestedEQ, out IEquipment playerEQ))
        {
            Console.WriteLine("Tried to use an Equipment that doesn't exist!");
            return;
        }

        PStateMachine.ChangeStateItemUse();
        playerEQ.Use();
    }

    public float GetMovementSpeedPerSecond() => movementSpeed;

    //Declares values for all equipment
    private void InitializeEquipment()
    {
        Equipment.Add(IEquipment.Weapons.bow, new Bow());
        Equipment.Add(IEquipment.Weapons.boomerangSack, new BoomerangSack());
        Equipment.Add(IEquipment.Weapons.swordSheathe, new SwordSheathe());
        Equipment.Add(IEquipment.Weapons.superSwordSheathe, new SuperSwordSheathe());
    }
}
