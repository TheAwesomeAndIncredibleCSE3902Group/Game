using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0;

public class Player
{
    //Singleton pattern seems acceptable for the player
    public static Player Instance { get; private set; }
    public Cardinal FacingDirection => PStateMachine.Direction;
    public Vector2 Position { get; set; }

    public PlayerStateMachine PStateMachine { get; private set; }

    public List<IEquipment> Equipment { get; } = new();
    public Dictionary<IEquipment.Projectiles, Projectile> spawnedProjectiles { get; set; } = new();
    
    //In pixels per tick. Might change to pixels per second later
    float movementSpeed = 2;


    //TODO: GET RID OF PLAYER KNOWING ABOUT SPRITE THINGS AT ALL
    public Player(ContentManager content, SpriteBatch _spriteBatch)
    {
        Instance = this;
        Equipment.Add(new Bow());
        Equipment.Add(new BoomerangSack());
        //Throwing in a random position so the sprite isn't halfway off the screen or something
        Vector2 startingPos = new Vector2(300, 300);
        Position = startingPos;

        PStateMachine = new PlayerStateMachine();
        PStateMachine.LoadPlayer(content,_spriteBatch);
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

    public void BeIdle()
    {
        PStateMachine.ChangeStateStanding();
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
            PStateMachine.ChangeStateStanding();
        }
        //Grabbing the direction from PlayerState here ensures that IPlayerState is the ultimate authority
        Cardinal newDirection = PStateMachine.Direction;
        PStateMachine.ChangeStateWalking();
        Position += movementSpeed * Util.CardinalToUnitVector(newDirection);
    }

    public void SwordAttack()
    {
        PStateMachine.ChangeStateSwordAttack();
    }

    public void UseEquipment()
    {
        PStateMachine.UseEquipment();
    }
}