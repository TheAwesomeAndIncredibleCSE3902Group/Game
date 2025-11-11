using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.Characters;
using static AwesomeRPG.IEquipment;

namespace AwesomeRPG.Map
{
    public class RoomAtlas
    {
        private List<List<RoomMap>> atlas;
        private RoomAtlas()
        {
            atlas = new List<List<RoomMap>>();
        }

        private static RoomAtlas instance = new RoomAtlas();

        public static RoomAtlas Instance { get { return instance; } }

        public RoomMap CurrentRoom { get; set; }

        public void SetAtlas(List<List<RoomMap>> startingAtlas)
        {
            atlas = startingAtlas;
        }
        public RoomMap GetRoom(int column, int row)
        {
            Debug.WriteLine("Columns: " + atlas.Count + ". Row: " + atlas[0].Count);
            if (column > atlas.Count - 1 || column < 0)
            {
                Debug.WriteLine("column out of bounds");
                return null;
            }
            else
            {
                if (row > atlas[column].Count - 1 || row < 0)
                {
                    Debug.WriteLine("row out of bounds");
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
                    Debug.WriteLine(i);
                    return i;
                }
            }
            //room not found
            Debug.WriteLine("RoomAtlas.GetColumn Error");
            return -1;
        }

        public int GetRow(RoomMap room)
        {
            for (int i = 0; i < atlas.Count; i++)
            {
                if (atlas[i].Contains(room))
                {
                    Debug.WriteLine(i);
                    Debug.WriteLine(atlas[i].IndexOf(room));
                    return atlas[i].IndexOf(room);
                }
            }
            //room not found
            Debug.WriteLine("RoomAtlas.GetRow Error");
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

        public void AddEnemy(CharacterEnemyBase enemy)
        {
            AddEnemy(enemy, GetColumn(CurrentRoom), GetRow(CurrentRoom));
        }
        public void AddEnemy(CharacterEnemyBase enemy, int column, int row)
        {
            GetRoom(column, row).Characters.Add(enemy);
            GetRoom(column, row)._movingCollisionObjects.Add(enemy);
        }

        public void RemoveEnemy(CharacterEnemyBase enemy)
        {
            RemoveEnemy(enemy, GetColumn(CurrentRoom), GetRow(CurrentRoom));
        }
        public void RemoveEnemy(CharacterEnemyBase enemy, int column, int row)
        {
            GetRoom(column, row).Characters.Remove(enemy);
            GetRoom(column, row)._movingCollisionObjects.Remove(enemy);
        }

        public void AddProjectile(Projectile projectile)
        {
            AddProjectile(projectile, GetColumn(CurrentRoom), GetRow(CurrentRoom));;
        }

        public void AddProjectile(Projectile projectile, int column, int row)
        {
            GetRoom(column,row).Projectiles.Add(projectile);
            GetRoom(column, row)._movingCollisionObjects.Add(projectile);
        }

        public void RemoveProjectile(Projectile projectile)
        {
            RemoveProjectile(projectile, GetColumn(CurrentRoom), GetRow(CurrentRoom));
        }

        public void RemoveProjectile(Projectile projectile, int column, int row)
        {
            GetRoom(column, row).Projectiles.Remove(projectile);
            GetRoom(column, row)._movingCollisionObjects.Remove(projectile);
        }

        public void AddPickup(Pickup pickup)
        {
            AddPickup(pickup, GetColumn(CurrentRoom), GetRow(CurrentRoom));
        }

        public void AddPickup(Pickup pickup, int column, int row)
        {
            GetRoom(column, row).Pickups.Add(pickup);
            GetRoom(column, row)._nonMovingCollisionObjects.Add(pickup);
        }
        
        public void RemovePickup(Pickup pickup)
        {
            RemovePickup(pickup, GetColumn(CurrentRoom), GetRow(CurrentRoom));
        }

        public void RemovePickup(Pickup pickup, int column, int row)
        {
            GetRoom(column, row).Pickups.Remove(pickup);
            GetRoom(column, row)._nonMovingCollisionObjects.Remove(pickup);
        }

    }

}
