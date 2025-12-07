using Microsoft.Xna.Framework;
using AwesomeRPG.Collision;
using static AwesomeRPG.Util;
using System.Diagnostics;

namespace AwesomeRPG.Map
{
    public class Entrance : CollisionObject
    {
        public Entrance(Vector2 startPos, int width, int height)
        {
            Position = startPos;
            Collider = new CollisionRect(this, width+1, height+1);
            ObjectType = CollisionObjectType.Entrance;
        }

        public static void ChangeRoom(CollisionObject player, Cardinal direction)
        {
            RoomAtlas roomAtlas = RoomAtlas.Instance;
            RoomMap oldRoom = roomAtlas.CurrentRoom;
            RoomMap newRoom = null;

            int row = roomAtlas.GetRow(oldRoom);
            int column = roomAtlas.GetColumn(oldRoom);
            Vector2 futurePlayerPos = player.Position;

            if (direction == Cardinal.left)
            {
                newRoom = roomAtlas.GetRoom(column - 1, row);
                futurePlayerPos = new Vector2(900, 250);
            }
            else if (direction == Cardinal.right)
            {
                newRoom = roomAtlas.GetRoom(column + 1, row);
                futurePlayerPos = new Vector2(100, 250);
            }
            else if (direction == Cardinal.up)
            {
                newRoom = roomAtlas.GetRoom(column, row - 1);
                futurePlayerPos = new Vector2(500, 400);
            }
            else if (direction == Cardinal.down)
            {
                newRoom = roomAtlas.GetRoom(column, row + 1);
                futurePlayerPos = new Vector2(500, 100);
            }

            if (newRoom != null)
            {
                player.Position = futurePlayerPos;
                roomAtlas.CurrentRoom = newRoom;
                roomAtlas.AddPlayer(player as PlayerOverworld);
                roomAtlas.RemovePlayer(player as PlayerOverworld,column,row);
            }
        } 
    }
}
