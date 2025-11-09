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
            //TODO: game no longer uses _characterSet, so this command should cause a level reload instead 

            // myGame._characterSet.Clear();
            // myGame._characterSet.Add(new CharacterEnemyMoblin(new Vector2(300, 350), Util.Cardinal.up));
            // myGame._characterSet.Add(new CharacterEnemyArmos(new Vector2(300, 350), Util.Cardinal.down));
            // myGame._characterSet.Add(new CharacterEnemyLynel(new Vector2(300, 350), Util.Cardinal.right));
            // myGame._characterSet.Add(new CharacterKris());
            // myGame.ChangeGameSpriteToNewSprite("item", MapItemSpriteFactory.CreatePotionSprite());
            // myGame.Player.Position = new Vector2(300, 300);
            // myGame.currentEnemy = 0;
        }
    }
}
