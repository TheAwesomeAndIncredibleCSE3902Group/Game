using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0;

public class Player : IPlayer
{
    //Singleton pattern seems acceptable for the player
    public static Player Instance { get; private set; }
    //TODO: change once PlayerState implemented
    //public Cardinal FacingDirection => PlayerState.Direction;
    public Cardinal FacingDirection => Cardinal.right;
    public Vector2 Position { get; set; }
    public ISprite Sprite { get; set; }
    public IPlayerState PlayerState { get; set; }

    public List<IEquipment> Equipment { get; } = new();
    public PlayerArrow Arrow { get; set; }

    //In pixels per tick. Might change to pixels per second later
    float movementSpeed = 2;

    public Player()
    {
        Instance = this;
        //Throwing in a random position so the sprite isn't halfway off the screen or something
        Position = new Vector2(300, 300);

        //TODO: create State
        //      ensure State initializes Sprite

        //Very temporary, just to test display until PlayerState is implemented
        Sprite = ItemSpriteFactory.CreateKrisSprite();
        //Testing arrow (heart) spawning
        Equipment.Add(new Bow());
        Equipment[0].Use();
    }

    public void Draw(GameTime gt)
    {
        Sprite.Draw(gt, Position);

        Arrow?.Draw(gt);
    }

    public void Update(GameTime gt)
    {
        Arrow?.Update(gt);
    }

    /// <summary>
    /// This is the target for any Move Command. 
    /// Player will then request IPlayerState change direction if necessary, and finally update position.
    /// </summary>
    public void Move(Cardinal direction)
    {
        if (PlayerState.Direction != direction)
            PlayerState.ChangeDirection(direction);

        //Grabbing the direction from PlayerState here ensures that IPlayerState is the ultimate authority
        Cardinal newDirection = PlayerState.Direction;
        Position += movementSpeed * new[] { -Vector2.UnitY, Vector2.UnitX, Vector2.UnitY, -Vector2.UnitX }[(int)newDirection];
    }

    public void UseEquipment()
    {
        PlayerState.UseEquipment();
    }
}
