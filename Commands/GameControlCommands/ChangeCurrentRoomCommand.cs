using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    public class ChangeCurrentRoomCommand : ICommand
    {
        private Cardinal changeDirection;

        /// <param name="content">ContentManager for RoomMapFromXML.</param>
        /// <param name="map">RoomMap for RoomMapFromXML.</param>
        /// <param name="direction">The dirction the room changes.</param>
        public ChangeCurrentRoomCommand(Cardinal direction) 
        {
            changeDirection = direction;
        }

        public void Execute() 
        {
            Entrance.changeRoom(Player.Instance, changeDirection);
        }
    }
}
