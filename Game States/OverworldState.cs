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
        RoomMap currentRoom = RoomAtlas.Instance.CurrentRoom;
        gameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime * TimeScale);
       
        Player.Update(gameTime);
        currentRoom.Update(gameTime);
        allCollisionHandler.HandleCollisions(currentRoom._movingCollisionObjects, currentRoom._nonMovingCollisionObjects);
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