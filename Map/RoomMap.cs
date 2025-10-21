using System.Collections.Generic;
using System.Numerics;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Map;

public class RoomMap
{
    private Tilemap _tilemap;
    public List<ICharacter> Characters;
    private Tilemap _minimap;

    public RoomMap(Tilemap map)
    {
        _tilemap = map;
        Characters = new();
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
        DrawCharacters(gameTime);
    }
    public void DrawTiles(SpriteBatch spriteBatch)
    {
        _tilemap.Draw(spriteBatch);
    }
    
    public void DrawCharacters(GameTime gameTime)
    {
        foreach (ICharacter c in Characters)
        {
            c.Draw(gameTime);
        }
    }

    
}