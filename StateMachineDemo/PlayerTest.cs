using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AwesomeRPG;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace StateMachineDemo;

public class PlayerTest
{
    //Singleton pattern seems acceptable for the player
    public static PlayerTest Instance { get; private set; }
    public Cardinal FacingDirection => Cardinal.right;
    public Vector2 Position { get; set; }
    public StateMachine PStateMachine { get; private set; }
    public List<IEquipment> Equipment { get; } = new();
    public Dictionary<IEquipment.Projectiles, Projectile> spawnedProjectiles { get; set; } = new();

    public PlayerTest(ContentManager content, SpriteBatch _spriteBatch)
    {
        Instance = this;
        Equipment.Add(new Bow());
        //Throwing in a random position so the sprite isn't halfway off the screen or something
        Vector2 startingPos = new Vector2(300, 300);
        Position = startingPos;
        PStateMachine = new StateMachine();
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

    /*
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
        Position += movementSpeed * new[] { -Vector2.UnitY, Vector2.UnitX, Vector2.UnitY, -Vector2.UnitX }[(int)newDirection];
    }
    */
}
