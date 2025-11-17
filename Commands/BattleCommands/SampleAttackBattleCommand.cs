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
    public class SampleAttackBattleCommand : ICommand
    {
        private PlayerBattle playerBattle;
        private IBattle targetBattle;
        public SampleAttackBattleCommand() 
        { 
            //Command should only be able to be called when it is a Player turn since Enemies should be automated
            playerBattle = new PlayerBattle(new PlayerStats(200,11,3,11,3,3,3,3,3));
            targetBattle = new MoblinBattle(new EnemyStats(100,1,1,1,1,1,1,1,100));
        }

        public void Execute() 
        {
            //Command should no longer be functional
            Debug.WriteLine("SampleAttackBattleCommand Executed");
            playerBattle.Attack(1);
            Debug.WriteLine($"Target Health after attack: {targetBattle.Stats.GetHealth()}");
        }
    }
}
