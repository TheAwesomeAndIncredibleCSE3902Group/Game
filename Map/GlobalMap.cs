using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Map;

public class GLobalMap
{
    private static GLobalMap instance = new GLobalMap();

    public RoomMap[][] Rooms;

    public static GLobalMap Instance
    {
        get
        {
            return instance;
        }
    }

    

    private GLobalMap ()
    {
        
    }  
}