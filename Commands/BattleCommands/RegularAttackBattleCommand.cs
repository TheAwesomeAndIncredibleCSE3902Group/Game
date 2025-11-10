using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;

namespace AwesomeRPG.Commands.BattleCommands
{
    public class RegularAttackBattleCommand : ICommand
    {
        private PlayerBattle playerBattle;
        private IBattle targetBattle;
        public RegularAttackBattleCommand(BattleScene currentBattle, IBattle target) 
        { 
            //Command should only be able to be called when it is a Player turn since Enemies should be automated
            playerBattle = (PlayerBattle)currentBattle.CurrentBattle;
            targetBattle = target;
        }

        public void Execute() 
        {
            playerBattle.Attack(targetBattle);
        }
    }
}
