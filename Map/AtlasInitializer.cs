using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;

namespace AwesomeRPG.Map
{
    public class AtlasInitializer
    {
        public static List<List<RoomMap>> InitializeAtlas(ContentManager content)
        {
            // Initialize with the starting rooms
            return ParseRoomsFromXML(content);
        }

        private static List<List<RoomMap>> ParseRoomsFromXML(ContentManager content)
        {
            List<List<RoomMap>> atlas = [];
            int numRows = 5;
            int numCols = 4;
            for (int i = 0; i < numRows; i++)
            {
                List<RoomMap> roomRow = [];
                for (int j = 0; j < numCols; j++)
                {
                    RoomMap room = MapParser.RoomMapFromXML(content, $"MapItems\\Level{i}-{j}.xml");
                    roomRow.Add(room);
                }
                atlas.Add(roomRow);
            }
            return atlas;
        }
    }
}
