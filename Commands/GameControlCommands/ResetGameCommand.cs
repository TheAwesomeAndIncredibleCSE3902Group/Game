using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Characters;
using AwesomeRPG.Sprites;

namespace AwesomeRPG.Commands
{
    internal class ResetGameCommand : ICommand
    {
        private Game1 myGame;
        public ResetGameCommand(Game1 game) { 
            myGame = game;
        }

        public void Execute() {
            myGame.Reset();
        }
    }
}
