using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.BattleMechanics
{
    public class SetupBattle
    {
        private string enemyType = null;
        public void Initialize(string enemy)
        {
            BattleScene.Instance.AllyList.Clear();
            BattleScene.Instance.EnemyList.Clear();
            enemyType = enemy;


            SetEnemies();
        }
        public void SetEnemies()
        {
            switch (enemyType)
            {
                case "Moblin":
                    BattleScene.Instance.EnemyList.Add(new BattleEnemies.MoblinBattle(new Stats.EnemyStats(10, 1, 1, 1, 1, 1, 1, 1, 10)));
                    break;
                case "Armos":
                    BattleScene.Instance.EnemyList.Add(new BattleEnemies.ArmosBattle(new Stats.EnemyStats(10, 1, 1, 1, 1, 1, 1, 1, 10)));
                    break;
                case "Lynel":
                    BattleScene.Instance.EnemyList.Add(new BattleEnemies.LynelBattle(new Stats.EnemyStats(10, 1, 1, 1, 1, 1, 1, 1, 10)));
                    break;
                default:
                    throw new Exception("Enemy type not recognized in InitializeBattle");
            }
        }

        public void SetAllies()
        {
            BattleScene.Instance.AllyList.Add(new BattleEnemies.PlayerBattle(Player.Instance.Party[0]));
        }
    }
}
