using Microsoft.Xna.Framework;
using AwesomeRPG.Collision;
using static AwesomeRPG.Util;
using System.Diagnostics;
using System;

namespace AwesomeRPG.Map
{
    public class DebugEntrance : CollisionObject
    {
        public DebugEntrance(Vector2 startPos, int width, int height)
        {
            Position = startPos;
            Collider = new CollisionRect(this, width, height);
            ObjectType = CollisionObjectType.DebugEntrance;
        }

        public static void ChangeRoom(CollisionObject player)
        {
            RoomAtlas roomAtlas = RoomAtlas.Instance;
            RoomMap oldRoom = roomAtlas.CurrentRoom;

            int oldRow = roomAtlas.GetRow(oldRoom);
            int oldCol = roomAtlas.GetColumn(oldRoom);
            
            int newRow = oldRow == 0 ? 3 : 0;
            int newCol = 0;
            
            RoomMap newRoom = roomAtlas.GetRoom(newCol, newRow);
            Vector2 futurePlayerPos = new Vector2(400, 350);

            if (newRoom != null)
            {
                player.Position = futurePlayerPos;
                roomAtlas.CurrentRoom = newRoom;
                roomAtlas.AddPlayer(player as PlayerOverworld);
                roomAtlas.RemovePlayer(player as PlayerOverworld,oldCol,oldRow);
            }
        } 
    }
}
