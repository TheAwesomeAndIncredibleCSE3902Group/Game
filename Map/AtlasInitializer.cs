using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.Map
{
    public class AtlasInitializer
    {
        private List<List<RoomMap>> atlas;

        public AtlasInitializer()
        {
        }
        public List<List<RoomMap>> InitializeAtlas(ContentManager content)
        {
            atlas = new List<List<RoomMap>>();
            // Initialize with the starting rooms
            RoomMap room_0_0 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level0-0.xml", new Vector2(3, 3));
            RoomMap room_0_1 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level0-1.xml", new Vector2(3, 3));
            RoomMap room_0_2 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level0-2.xml", new Vector2(3, 3));
            RoomMap room_1_0 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level1-0.xml", new Vector2(3, 3));
            RoomMap room_1_1 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level1-1.xml", new Vector2(3, 3));
            RoomMap room_1_2 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level1-2.xml", new Vector2(3, 3));
            RoomMap room_2_0 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level2-0.xml", new Vector2(3, 3));
            RoomMap room_2_1 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level2-1.xml", new Vector2(3, 3));
            RoomMap room_2_2 = MapParser.Instance.RoomMapFromXML(content, "MapItems\\Level2-2.xml", new Vector2(3, 3));
            atlas.Add(new List<RoomMap> { room_0_0, room_0_1, room_0_2 });
            atlas.Add(new List<RoomMap> { room_1_0, room_1_1, room_1_2 });
            atlas.Add(new List<RoomMap> { room_2_0, room_2_1, room_2_2 });

            return atlas;
        }
    }
}
