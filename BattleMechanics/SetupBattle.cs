using AwesomeRPG.BattleMechanics.BattleEnemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.BattleMechanics
{
    public class SetupBattle
    {
        private string enemyType = null;
        private int partyLevelAvg = 0;
        public void Initialize(string enemy)
        {
            BattleScene.Instance.AllyList.Clear();
            BattleScene.Instance.EnemyList.Clear();
            enemyType = enemy;

            foreach (var ally in Player.Instance.Party) partyLevelAvg+=ally.GetLevel();
            partyLevelAvg /= Player.Instance.Party.Count;

            List<IBattle> enemies = SetEnemies();
            List<IBattle> allies = SetAllies();

            BattleScene.Instance.InitializeBattleSequence(true, enemies, allies);
        }
        private List<IBattle> SetEnemies()
        {
            List<IBattle> enemyList = new List<IBattle>();

            switch (enemyType)
            {
                case "Moblin":
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg)); 
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    break;
                case "Armos":
                    enemyList.Add(new BattleEnemies.ArmosBattle(partyLevelAvg));
                    enemyList.Add(new BattleEnemies.ArmosBattle(partyLevelAvg));
                    break;
                case "Lynel":
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList.Add(new BattleEnemies.LynelBattle(partyLevelAvg));
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    break;
                default:
                    throw new Exception("Enemy type not recognized in InitializeBattle");
            }

            return enemyList;
        }

        private List<IBattle> SetAllies()
        {
            List<IBattle> allyList = new List<IBattle>();

            foreach (var ally in Player.Instance.Party)
            {
                allyList.Add(new PlayerBattle(ally));
            }

            return allyList;
        }
    }
}
