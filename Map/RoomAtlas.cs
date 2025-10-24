using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.Map
{
    public class RoomAtlas
    {
        private List<List<RoomMap>> atlas;
        public RoomAtlas(RoomMap startingRoom) {
            atlas[0].Add(startingRoom);
        }

        public void AddRoom(RoomMap room, int column, bool horizontal, bool increase)
        {
            if (atlas[column].Contains(room))
            {
                //early exit if room already exists in atlas
                return;
            }

            if (horizontal)
            {
                if (increase)
                {
                    // add a new column to the right
                    atlas.Add(new List<RoomMap> { room });
                }
                else
                {
                    // add a new column to the left
                    atlas.Insert(0, new List<RoomMap> { room });
                }
            }
            else
            {
                if (increase)
                {
                    //add a new row to the bottom of the specified column
                    atlas[column].Add(room);
                }
                else
                {
                    //add a new row to the top of the specified column
                    atlas[column].Insert(0, room);
                }
            }
        }

        public RoomMap GetRoom(int column, int row)
        {
            return atlas[column][row];
        }
    }
}
