using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.Characters;

namespace Sprint0.Commands
{
    public class CommandSwitchEnemySprite : ICommand
    {
        private Game1 myGame;
        private int direction = 0;

        public CommandSwitchEnemySprite(Game1 game, bool right)
        {
            myGame = game;
            direction += right ? -1 : 1;
        }

        public void Execute()
        {
            int Size = myGame._characterSet.Count;
            if (direction + myGame.currentEnemy >= Size)
            {
                myGame.currentEnemy = 0;
            }
            else if (direction + myGame.currentEnemy < 0)
            {
                myGame.currentEnemy = Size;
            }
            else
            {
                myGame.currentEnemy += direction;
            }
            
        }

    }
}
