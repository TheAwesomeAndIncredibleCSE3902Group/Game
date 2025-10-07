using AwesomeRPG.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.Commands
{
    public class CommandSwitchMapItemSprite : ICommand
    {
        Game1 myGame;
        public enum Direction {left,right}
        private const int _minIndex = 0;
        private static int _currentItemIndex = _minIndex;
        private const int _maxIndex = 2;
        private int _direction;

        /// <summary>
        /// Creates a new command which switches the item over a list. Direction determines which way.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="direction"></param>
        public CommandSwitchMapItemSprite(Game1 game, bool right) 
        {
            myGame = game;
            _direction = right ? -1 : 1; //Figure out which way to cycle the sprite 
        }

        public void Execute()
        {
            SwapSprite();
        }

        //Swaps the sprite through the list
        private void SwapSprite()
        {
            _currentItemIndex += _direction;
            CheckWrapping();
            switch (_currentItemIndex)
            {
                case 0:
                    myGame.ChangeGameSpriteToNewSprite("item", MapItemSpriteFactory.CreatePotionSprite());
                    break;
                case 1:
                    myGame.ChangeGameSpriteToNewSprite("item", MapItemSpriteFactory.CreateCandleSprite());
                    break;
                case 2:
                    myGame.ChangeGameSpriteToNewSprite("item", MapItemSpriteFactory.CreateRupeeSprite());
                    break;    
            }
           
        }

        //Checks if currentIndex is out of bounds if it is then it loops it over
        private void CheckWrapping()
        {
            if (_currentItemIndex > _maxIndex)
            {
                _currentItemIndex = _minIndex;
            }
            else if(_currentItemIndex < _minIndex)
            {
                _currentItemIndex = _maxIndex;
            }
        }

    }
}
