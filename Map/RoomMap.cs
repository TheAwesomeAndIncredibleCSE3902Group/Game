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
    public List<ICharacter> Characters = [];
    public List<Pickup> Pickups = [];
    public List<Projectile> Projectiles = [];
    public List<CollisionObject> _movingCollisionObjects = [];
    public List<CollisionObject> _nonMovingCollisionObjects = [];
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

    public void Update(GameTime gameTime)
    {
        UpdateCharacters(gameTime);
        UpdateProjectiles(gameTime);
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

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        DrawTiles(spriteBatch);
        DrawPickups(gameTime);
        DrawCharacters(gameTime);
        DrawProjectiles(gameTime);
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

    
}