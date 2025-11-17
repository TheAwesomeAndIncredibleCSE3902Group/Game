using System.Collections.Generic;
using System.Numerics;
using AwesomeRPG.Collision;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;

namespace AwesomeRPG.Map;

public class RoomMap
{
    private readonly Tilemap _tilemap;
    public PlayerOverworld Player { private get; set; } 
    public List<ICharacter> Characters { get; private set; } = [];
    public List<Pickup> Pickups { get; private set; } = [];
    public List<Projectile> Projectiles { get; private set; } = [];
    public List<CollisionObject> MovingCollisionObjects { get; private set; } = [];
    public List<CollisionObject> NonMovingCollisionObjects { get; private set; } = [];
    private readonly Tilemap _minimap;

    public RoomMap(Tilemap map)
    {
        _tilemap = map;
    }


    // Currently non-functional but could be used for a map option/item 
    public Tilemap GenMap()
    {
        for (int i = 0; i < _tilemap.Columns; i++)
        {
            for (int j = 0; j < _tilemap.Rows; j++)
            {
                _minimap.SetTile(i, j, _tilemap.GetTile(i, j).Id);
            }

        }
        return _minimap;

    }

    #region Update
    public void Update(GameTime gameTime)
    {
        UpdatePlayer(gameTime);
        UpdateCharacters(gameTime);
        UpdateProjectiles(gameTime);
        UpdateCollision();
    }

    private void UpdatePlayer(GameTime gameTime)
    {
        Player.Update(gameTime);
    }

    private void UpdateCharacters(GameTime gameTime)
    {
        foreach (ICharacter c in Characters)
        {
            c.Update(gameTime);
        }
    }

    private void UpdateProjectiles(GameTime gameTime)
    {
        try
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update(gameTime);
            }
        }
        catch 
        {
            return;
        }
    }

    private void UpdateCollision()
    {
        AllCollisionHandler.Instance.HandleCollisions(MovingCollisionObjects, NonMovingCollisionObjects);
    }
    #endregion

    #region Draw
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        DrawTiles(spriteBatch);
        DrawPickups(gameTime);
        DrawCharacters(gameTime);
        DrawProjectiles(gameTime);
        DrawPlayer(gameTime);
    }
    
    private void DrawPlayer(GameTime gameTime)
    {
        Player.Draw(gameTime);
    }
    private void DrawTiles(SpriteBatch spriteBatch)
    {
        _tilemap.Draw(spriteBatch);
    }

    private void DrawPickups(GameTime gameTime)
    {
        foreach (Pickup p in Pickups)
        {
            p.Draw(gameTime);
        }
    }
    
    private void DrawCharacters(GameTime gameTime)
    {
        foreach (ICharacter c in Characters)
        {
            c.Draw(gameTime);
        }
    }

    private void DrawProjectiles(GameTime gameTime)
    {
        foreach (Projectile p in Projectiles)
        {
            p.Draw(gameTime);
        }
    }
    #endregion

    
}