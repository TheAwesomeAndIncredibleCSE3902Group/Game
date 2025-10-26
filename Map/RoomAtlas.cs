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

        public RoomAtlas(List<List<RoomMap>> startingAtlas)
        {
            atlas = startingAtlas;
        }

        public RoomMap GetRoom(int column, int row)
        {
            if (column > atlas.Count - 1 || column < 0)
            {
                return null;
            }
            else
            {
                if (row > atlas[column].Count - 1 || row < 0)
                {
                    return null;
                }
                return atlas[column][row];
            }
        }

        public int GetColumn(RoomMap room)
        {
            for (int i = 0; i < atlas.Count; i++)
            {
                if (atlas[i].Contains(room))
                {
                    return i;
                }
            }
            //room not found
            return -1;
        }

        public int GetRow(RoomMap room)
        {
            for (int i = 0; i < atlas.Count; i++)
            {
                if (atlas[i].Contains(room))
                {
                    return atlas[i].IndexOf(room);
                }
            }
            //room not found
            return -1;
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

        public void RemoveRoom(RoomMap room)
        {
            for (int i = 0; i < atlas.Count; i++)
            {
                if (atlas[i].Contains(room))
                {
                    int roomIndex = atlas[i].IndexOf(room);
                    atlas[i].Remove(room);
                    // Optionally, remove the column if it's empty
                    if (atlas[i].Count == 0)
                    {
                        atlas.RemoveAt(i);
                    }
                    // Creates an empty RoomMap placeholder to maintain row position.
                    else
                    {
                        atlas[i].Insert(roomIndex, new RoomMap(null));
                    }
                    return;
                }
            }
        }

        public void ReplaceRoom(RoomMap oldRoom, RoomMap newRoom)
        {
            for (int i = 0; i < atlas.Count; i++)
            {
                if (atlas[i].Contains(oldRoom))
                {
                    int roomIndex = atlas[i].IndexOf(oldRoom);
                    atlas[i].Remove(oldRoom);
                    atlas[i].Insert(roomIndex, newRoom);
                    return;
                }
            }
        }
    }
}
