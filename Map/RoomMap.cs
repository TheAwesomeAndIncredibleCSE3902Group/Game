using System.Numerics;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Map;

public class RoomMap
{
    private Tilemap _tilemap;
    private Tilemap _minimap;

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
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _tilemap.Draw(spriteBatch);
    }

    
}