using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using static Sprint0.Util;
using static Sprint0.PlayerStateMachine;

namespace Sprint0;

public class Player
{
    //Singleton pattern seems acceptable for the player
    public static Player Instance { get; private set; }
    public Cardinal FacingDirection => PStateMachine.Direction;
    public Vector2 Position { get; set; }

    public PlayerStateMachine PStateMachine { get; private set; }

    public Dictionary<IEquipment.Weapons,IEquipment> Equipment { get; } = new();
    public Dictionary<IEquipment.Projectiles, Projectile> spawnedProjectiles { get; set; } = new();
    
    //In pixels per tick. Might change to pixels per second later
    float movementSpeed = 2;


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
        foreach (Projectile projectile in spawnedProjectiles.Values)
        {
            projectile.Update(gt);
        }

        PStateMachine.Update(gt);
    }


    /// <summary>
    /// This is the target for any Move Command. 
    /// Player will then request IPlayerState change direction if necessary, and finally update position.
    /// </summary>
    public void Move(Cardinal direction)
    {
        if (PStateMachine.Direction != direction)
        {
            PStateMachine.ChangeDirection(direction);
        }

        if (PStateMachine.GetCurrentState() != States.Walking)
        {
            return;
        }
        //Grabbing the direction from PlayerState here ensures that IPlayerState is the ultimate authority
        Cardinal newDirection = PStateMachine.Direction;
        Position += movementSpeed * Util.CardinalToUnitVector(newDirection);
    }

    //Declares values for all equipment
    private void InitializeEquipment()
    {
        Equipment.Add(IEquipment.Weapons.bow, new Bow());
        Equipment.Add(IEquipment.Weapons.boomerangSack, new BoomerangSack());
        Equipment.Add(IEquipment.Weapons.swordSheathe, new SwordSheathe());
    }
}