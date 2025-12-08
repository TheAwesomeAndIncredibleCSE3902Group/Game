using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;
using Sprint0.BattleMechanics.BattleEnemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.BattleMechanics
{
    public class InitializeSampleBattle
    {
        public InitializeSampleBattle() {}
        public List<IBattle> SetUpAllies()
        {
            List<IBattle> battles = new List<IBattle>();
            battles.Add(new LinkBattle(new PlayerStats(50, 5, 5, 5, 5, 5, 5, 5, 100)));
            battles.Add(new LinkBattle(Player.Instance.Party[0]));
            return battles;
        }
        public List<IBattle> SetUpEnemies()
        {
            List<IBattle> battles = new List<IBattle>();
            battles.Add(new MoblinBattle(new EnemyStats(10, 1, 1, 1, 1, 1, 1, 1, 100)));
            battles.Add(new MoblinBattle(new EnemyStats(10, 1, 1, 1, 1, 1, 1, 1, 100)));
            return battles;
        }
    }
}
