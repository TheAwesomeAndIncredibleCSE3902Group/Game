using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands;

public class PlayerEntranceCollideCommand : ICollisionCommand
{
    RoomMap oldRoom;
    RoomMap newRoom;
    RoomAtlas roomAtlas;
    Game1 myGame;
    Cardinal direction;
    public void Execute(CollisionInfo collision)
    {
        Entrance entrance = (Entrance)collision.GetCollisionObjectOfType(CollisionObjectType.Entrance);

        roomAtlas = entrance.roomAtlas;
        myGame = entrance.myGame;
        oldRoom = myGame.RoomMap;
        direction = collision.Direction.ToCard();
        int row = roomAtlas.GetRow(oldRoom);
        int column = roomAtlas.GetColumn(oldRoom);

        if (direction == Cardinal.left)
        {
            newRoom = roomAtlas.GetRoom(column - 1, row);
        }
        else if (direction == Cardinal.right)
        {
            newRoom = roomAtlas.GetRoom(column + 1, row);
        }
        else if (direction == Cardinal.up)
        {
            newRoom = roomAtlas.GetRoom(column, row - 1);
        }
        else if (direction == Cardinal.down)
        {
            newRoom = roomAtlas.GetRoom(column, row + 1);
        }

        if (newRoom != null)
        {
            myGame.RoomMap = newRoom;
        }
    }
}
