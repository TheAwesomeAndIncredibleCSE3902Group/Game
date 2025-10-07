using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using static AwesomeRPG.PlayerStateMachine;

namespace AwesomeRPG;

public class Player
{
    //Singleton pattern seems acceptable for the player
    public static Player Instance { get; private set; }
    public Cardinal FacingDirection => PStateMachine.Direction;
    public Vector2 Position { get; set; }

    public PlayerStateMachine PStateMachine { get; private set; }

    public Dictionary<IEquipment.Weapons,IEquipment> Equipment { get; } = new();
    public Dictionary<IEquipment.Projectiles, Projectile> spawnedProjectiles { get; set; } = new();

    //Whether this has moved yet this frame. Please be careful of any timing issues / race conditions with the Controllers.
    private bool hasMoved;
    
    //In pixels per tick. Might change to pixels per second later
    private float movementSpeed = 2;


    public Player(ContentManager content, SpriteBatch _spriteBatch)
    {
        Instance = this;
        InitializeEquipment();
        //Throwing in a random position so the sprite isn't halfway off the screen or something
        Vector2 startingPos = new Vector2(300, 300);
        Position = startingPos;

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
        hasMoved = false;

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
        if (hasMoved)
            return;

        if (PStateMachine.Direction != direction)
        {
            PStateMachine.ChangeDirection(direction);
        }

        PStateMachine.ChangeStateWalking();

        if (PStateMachine.GetCurrentState() == States.Walking)
        {
            //Grabbing the direction from PlayerState here ensures that IPlayerState is the ultimate authority
            Cardinal newDirection = PStateMachine.Direction;
            Position += movementSpeed * Util.CardinalToUnitVector(newDirection);
        }

        hasMoved = true;
    }

    public void TakeDamage(int amount = 1)
    {
        PStateMachine.ChangeStateDamaged();
    }

    public void UseEquipment(IEquipment.Weapons eq)
    {
        if (!Equipment.ContainsKey(eq))
        {
            Console.WriteLine("Tried to use an Equipment that doesn't exist!");
            return;
        }
        
        PStateMachine.ChangeStateItemUse();
        Equipment[eq].Use();
    }

    //Declares values for all equipment
    private void InitializeEquipment()
    {
        Equipment.Add(IEquipment.Weapons.bow, new Bow());
        Equipment.Add(IEquipment.Weapons.boomerangSack, new BoomerangSack());
        Equipment.Add(IEquipment.Weapons.swordSheathe, new SwordSheathe());
        Equipment.Add(IEquipment.Weapons.superSwordSheathe, new SuperSwordSheathe());
    }
}