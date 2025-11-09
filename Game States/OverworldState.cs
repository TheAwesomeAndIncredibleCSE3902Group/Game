using System;
using System.Collections.Generic;
using AwesomeRPG.Collision;
using AwesomeRPG.Controllers;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG;

/// <summary>
/// Will probably only be made once per play session and then just modified.
/// </summary>
public class OverworldState : IGameState
{
    public float TimeScale { get; private set; }
    private List<IController> controllersList = new();
    public Player Player { get; private set; }

    //TODO: These two lists should definitely be moved into allCollisionHandler
    public static List<CollisionObject> MovingCollisionObjects { get; private set; } = new();
    public List<CollisionObject> NonMovingCollisionObjects { get; private set; } = new();
    private AllCollisionHandler allCollisionHandler;
    public List<int> Tiles { get; private set; }

    // public RootElement RootUIElement {get; private set; }


    public OverworldState()
    {
        throw new NotImplementedException();
    }

    private void Initialize()
    {
        NonMovingCollisionObjects = RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects;
        MovingCollisionObjects = RoomAtlas.Instance.CurrentRoom._movingCollisionObjects;

        //Player declaration
        //TODO: PROBABLY WANNA HAVE A METHOD IN EACH LEVEL WHICH HANDLES ADDING THINGS TO COLLISION LIST

        //Do I actually want this here?
        //Maybe set up the player in Game or something idk
        //Player = new Player(Content, _spriteBatch);
        MovingCollisionObjects.Add(Player);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);

        RoomAtlas.Instance.CurrentRoom.Draw(spriteBatch, gameTime);
        Player.Draw(gameTime);

        // Temporarily commented out for Sprint3 submission
        // RootUIElement.Draw(gameTime);

        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);

        foreach (IController controller in controllersList)
        {
            controller.Update(Game1.GameState.overworld);
        }

        Player.Update(gameTime);

        RoomAtlas.Instance.CurrentRoom.Update(gameTime);
        HandleCollisions();

        throw new System.NotImplementedException();
    }

    public BattleState ToBattleState()
    {
        //This will have to convert any relevant data to its battle representation
        //And return a new BattleState
        return new BattleState(this);
        throw new System.NotImplementedException();
    }

    public OverworldState ToOverworldState()
    {
        return this;
    }

    private void HandleCollisions()
    {
        // This is solely detecting player collisions with everything because the
        // movingCollisionObjects list has only the player and nothing else added.
        // Might be good to separate the player out into it's own collision object
        // to simplify the interactions between the player and everything not just
        // for interactability with the world but also for battle mechanics with
        // turn order and any AoE damage on both sides.
        for (int i = 0; i < MovingCollisionObjects.Count; i++)
        {
            foreach (CollisionObject nonMovingObject in NonMovingCollisionObjects)
            {
                CollisionInfo collision = MovingCollisionObjects[i].DetectCollision(nonMovingObject);
                allCollisionHandler.HandleCollision(collision);
            }

            for (int j = i + 1; j < MovingCollisionObjects.Count; j++)
            {
                CollisionInfo collision = MovingCollisionObjects[i].DetectCollision(MovingCollisionObjects[j]);
                allCollisionHandler.HandleCollision(collision);
            }
        }
        ClearProjectiles();
        ClearPickups();
    }

    //Vile code, made by the most deprived of man. May this be fixed next sprint
    private void ClearProjectiles()
    {
        if (Player.spawnedProjectiles.Count == 0)
        {
            for (int i = 0; i < MovingCollisionObjects.Count; i++)
            {
                if (MovingCollisionObjects[i].ObjectType == CollisionObjectType.PlayerProjectile)
                {
                    MovingCollisionObjects.RemoveAt(i);
                }
            }
        }
    }

    //Vile code, made by the most deprived of man. May this be fixed next sprint
    private int prevPickups = 2;
    private void ClearPickups()
    {
        if (prevPickups != 0 && RoomAtlas.Instance.CurrentRoom.Pickups.Count != prevPickups)
        {
            for (int i = 0; i < NonMovingCollisionObjects.Count; i++)
            {
                if (NonMovingCollisionObjects[i].ObjectType == CollisionObjectType.Pickup)
                {
                    NonMovingCollisionObjects.RemoveAt(i);
                }
            }
            prevPickups--;
        }
    }
}