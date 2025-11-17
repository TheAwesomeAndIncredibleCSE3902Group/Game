using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;
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
            battles.Add(new PlayerBattle(Player.Instance.PlayerStats));
            battles.Add(new PlayerBattle(new PlayerStats(20, 5, 5, 5, 5, 5, 5, 5, 100)));
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
