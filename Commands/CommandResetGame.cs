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
    internal class CommandResetGame : ICommand
    {
        private Game1 myGame;
        public CommandResetGame(Game1 game) { 
            myGame = game;
        }

        public void Execute() {
            myGame._characterSet.Clear();
            myGame._characterSet.Add(new CharacterEnemyMoblin(new Vector2(300, 350), Util.Cardinal.up));
            myGame._characterSet.Add(new CharacterEnemyArmos(new Vector2(300, 350), Util.Cardinal.down));
            myGame._characterSet.Add(new CharacterEnemyLynel(new Vector2(300, 350), Util.Cardinal.right));
            myGame._characterSet.Add(new CharacterKris());
            myGame.ChangeGameSpriteToNewSprite("item", MapItemSpriteFactory.CreatePotionSprite());
            myGame.Tilemap.SetTile(1, 2, 9);
            myGame.Player.Position = new Vector2(300, 300);
            myGame.currentEnemy = 0;
        }
    }
}
