using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int target;
        public RegularAttackBattleCommand(int targetIndex) 
        {
            //Command should only be able to be called when it is a Player turn since Enemies should be automateds
            target = targetIndex;
        }

        public void Execute() 
        {
            ((PlayerBattle)BattleScene.Instance.CurrentBattle).Attack(target);
        }
    }
}
