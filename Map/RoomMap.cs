using System.Collections.Generic;
using System.Numerics;
using AwesomeRPG.Collision;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Map;

public class RoomMap
{
    private readonly Tilemap _tilemap;
    public List<ICharacter> Characters =[];
    public List<Pickup> Pickups = [];
    public List<CollisionObject> _movingCollisionObjects = [];
    public List<CollisionObject> _nonMovingCollisionObjects = [];
    private readonly Tilemap _minimap;

    public RoomMap(Tilemap map)
    {
        _tilemap = map;
    }


    // we might use this later to make some sort of minimap or map item. 
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
        foreach (ICharacter c in Characters)
        {
            c.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        DrawTiles(spriteBatch);
        DrawPickups(gameTime);
        DrawCharacters(gameTime);
    }
    
    public void DrawTiles(SpriteBatch spriteBatch)
    {
        _tilemap.Draw(spriteBatch);
    }

    public void DrawPickups(GameTime gameTime)
    {
        foreach (Pickup p in Pickups)
        {
            p.Draw(gameTime);
        }
    }
    
    public void DrawCharacters(GameTime gameTime)
    {
        foreach (ICharacter c in Characters)
        {
            c.Draw(gameTime);
        }
    }

    
}