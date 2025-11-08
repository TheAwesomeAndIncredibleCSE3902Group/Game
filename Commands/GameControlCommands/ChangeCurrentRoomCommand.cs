using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class ChangeCurrentRoomCommand : ICommand
    {
        private RoomMap roomMap;
        private RoomMap newRoom;
        private RoomAtlas roomAtlas;
        private Cardinal changeDirection;

        /// <param name="content">ContentManager for RoomMapFromXML.</param>
        /// <param name="map">RoomMap for RoomMapFromXML.</param>
        /// <param name="direction">The dirction the room changes.</param>
        public ChangeCurrentRoomCommand(Cardinal direction) 
        {
            roomAtlas = RoomAtlas.Instance;
            changeDirection = direction;
        }

        public void Execute() 
        {
            roomMap = roomAtlas.CurrentRoom;
            int row = roomAtlas.GetRow(roomMap);
            int column = roomAtlas.GetColumn(roomMap);

            if (changeDirection == Cardinal.left)
            {
                newRoom = roomAtlas.GetRoom(column - 1, row);
            }
            else if (changeDirection == Cardinal.right)
            {
                newRoom = roomAtlas.GetRoom(column + 1, row);
            }
            else if (changeDirection == Cardinal.up)
            {
                newRoom = roomAtlas.GetRoom(column, row - 1);
            }
            else if (changeDirection == Cardinal.down)
            {
                newRoom = roomAtlas.GetRoom(column, row + 1);
            }

            if (newRoom != null)
            {
                roomAtlas.CurrentRoom = newRoom;
            }
        }
    }
}
