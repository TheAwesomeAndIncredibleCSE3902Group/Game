using System;
using System.Collections.Generic;
using AwesomeRPG.Characters;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG;

/// <summary>
/// Made once per sessions and only modified thereafter.
/// Handles the updating and drawing of all overworld things: Rooms (which includes Characters), Player, and Collisions.
/// </summary>
public class OverworldState : IGameState
{
    //Will eventually be used as a global scalar for time (ie affects everything in the Overworld)
    public float TimeScale { get; private set; } = 1;
    public Player Player { get; private set; }

    private Game1 game;
    private AllCollisionHandler allCollisionHandler;
    public GameState CurrentState { get => GameState.overworld; }

    // public RootElement RootUIElement {get; private set; }

    /// <summary>
    /// Requires Content already loaded and Player fully constructed
    /// </summary>
    /// <param name="contentManager"></param>
    /// <param name="player"></param>
    /// <exception cref="NotImplementedException"></exception>
    public OverworldState(ContentManager contentManager, Player player, Game1 game)
    {
        this.game = game;

        allCollisionHandler = new AllCollisionHandler();

        CreateWorld(contentManager);
        this.Player = player;

        //Add player to the collision tracker of this room
        RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Add(Player);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        //Scale gameTime by TimeScale
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);

        RoomAtlas.Instance.CurrentRoom.Draw(spriteBatch, gameTime);
        Player.Draw(gameTime);
        //TODO: draw HUD here
    }

    public void Update(GameTime gameTime)
    {
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);
       
        Player.Update(gameTime);
        RoomAtlas.Instance.CurrentRoom.Update(gameTime);
        HandleCollisions();
        //TODO: refresh HUD here

    }

    public void ChangeToBattleState(CharacterEnemyBase[] enemies)
    {
        //This will have to convert any relevant data to its battle representation
        //And return a new BattleState
        game.SetStateClass(new BattleState(this, game, enemies));
    }

    public void ChangeToOverworldState() { }

    public void ChangeToStartState() { }
    
    public void ChangeToGameOverState() { }

    private void CreateWorld(ContentManager contentManager)
    {
        RoomAtlas.Instance.SetAtlas(new AtlasInitializer().InitializeAtlas(contentManager));
        RoomAtlas.Instance.CurrentRoom = RoomAtlas.Instance.GetRoom(0,0);
    }

    //I don't really like this handling collisions. But there's no functional reason to move this functionality at this time. 

    private void HandleCollisions()
    {
        List<CollisionObject> movingCollisionObjects =  RoomAtlas.Instance.CurrentRoom._movingCollisionObjects;
        List<CollisionObject> nonMovingCollisionObjects = RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects;
        for (int i = 0; i < movingCollisionObjects.Count; i++)
        {
            for (int k = 0; k < nonMovingCollisionObjects.Count; k++)
            {
                try
                {
                    CollisionObject movingObject = RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[i];
                    CollisionObject nonMovingObject = nonMovingCollisionObjects[k];
                    CollisionInfo collision = movingObject.DetectCollision(nonMovingObject);
                    allCollisionHandler.HandleCollision(collision);
                }
                catch
                {
                    return;
                }
            }

            for (int j = i + 1; j < RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Count; j++)
            {
                CollisionInfo collision = RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[i].DetectCollision(RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[j]);
                allCollisionHandler.HandleCollision(collision);
            }
        }
        ClearProjectiles();
        ClearPickups();
    }

    //Vile code, made by the most deprived of man. May this be fixed next sprint
    private void ClearProjectiles()
    {
        if(Player.spawnedProjectiles.Count == 0)
        {
            for(int i = 0; i < RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Count; i++)
            {
                if(RoomAtlas.Instance.CurrentRoom._movingCollisionObjects[i].ObjectType == CollisionObjectType.PlayerProjectile)
                {
                    RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.RemoveAt(i);
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
            for (int i = 0; i < RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects.Count; i++)
            {
                if (RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects[i].ObjectType == CollisionObjectType.Pickup)
                {
                    RoomAtlas.Instance.CurrentRoom._nonMovingCollisionObjects.RemoveAt(i);
                }
            }
            prevPickups--;
        }
    }

    public bool TransitionAllowedTo(GameState state)
    {
        return state switch
        {
            GameState.battle => true,
            GameState.overworld => true,
            _ => false
        };
    }
}