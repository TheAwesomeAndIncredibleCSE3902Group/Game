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

namespace AwesomeRPG.Commands
{
    public class ChangeCurrentRoomCommand : ICommand
    {
        private RoomMap roomMap;
        private RoomMap newRoom;
        private RoomAtlas roomAtlas;
        private Game1 myGame;
        private int changeDirection;

        /// <param name="content">ContentManager for RoomMapFromXML.</param>
        /// <param name="map">RoomMap for RoomMapFromXML.</param>
        /// <param name="direction">The dirction the room changes. 1 & 2 go left & right. 3 & 4 go up & down.</param>
        public ChangeCurrentRoomCommand(Game1 game, RoomAtlas atlas, int direction) 
        {
            roomAtlas = atlas;
            changeDirection = direction;
            myGame = game;
        }

        public void Execute() 
        {
            roomMap = myGame.RoomMap;
            int row = roomAtlas.GetRow(roomMap);
            int column = roomAtlas.GetColumn(roomMap);

            if (changeDirection == 1)
            {
                newRoom = roomAtlas.GetRoom(column - 1, row);
            }
            else if (changeDirection == 2)
            {
                newRoom = roomAtlas.GetRoom(column + 1, row);
            }
            else if (changeDirection == 3 && row > 0)
            {
                newRoom = roomAtlas.GetRoom(column, row - 1);
            }
            else if (changeDirection == 4)
            {
                newRoom = roomAtlas.GetRoom(column, row + 1);
            }

            if (newRoom != null)
            {
                myGame.RoomMap = newRoom;
            }
        }
    }
}
