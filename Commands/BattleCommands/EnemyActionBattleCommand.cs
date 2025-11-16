using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;

namespace AwesomeRPG.Commands.BattleCommands
{
    public class EnemyActionBattleCommand : ICommand
    {
        public IEnemyBattle battle;
        public EnemyActionBattleCommand() {
            battle = (IEnemyBattle)BattleScene.Instance.CurrentBattle;
        }
        public void Execute() {
            battle.TakeTurn();
        }
    }
}
