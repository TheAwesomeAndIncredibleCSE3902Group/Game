using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Commands
{
    public class CommandSwitchEnemySprite : ICommand
    {
        private Game1 myGame;

        public CommandSwitchEnemySprite(Game1 game) 
        {
            myGame = game;
        }

        public void Execute()
        {

        }

    }
}
