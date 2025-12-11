using AwesomeRPG.BattleMechanics.BattleEnemies;
using Sprint0.BattleMechanics.BattleEnemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.BattleMechanics
{
    public static class SetupBattle
    {
        private static string enemyType = null;
        private static int partyLevelAvg = 0;
        public static void Initialize(string enemy, bool playerStarting)
        {
            BattleScene.Instance.AllyList.Clear();
            BattleScene.Instance.EnemyList.Clear();
            enemyType = enemy;

            foreach (var ally in Player.Instance.Party) partyLevelAvg+=ally.GetLevel();
            partyLevelAvg /= Player.Instance.Party.Count;

            List<IBattle> enemies = SetEnemies();
            List<IBattle> allies = SetAllies();

            Debug.WriteLine($"Middle enemy name: {enemies[1].Name}");
                
            BattleScene.Instance.InitializeBattleSequence(playerStarting, enemies, allies);
        }
        private static List<IBattle> SetEnemies()
        {
            List<IBattle> enemyList = new List<IBattle>();

            switch (enemyType)
            {
                case "Moblin":
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg)); 
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    for (int i = 0; i < enemyList.Count; i++) { enemyList[i].Name += $" {i+1}"; }
                    break;
                case "Armos":
                    enemyList.Add(new BattleEnemies.ArmosBattle(partyLevelAvg));
                    enemyList.Add(new BattleEnemies.ArmosBattle(partyLevelAvg));
                    for (int i = 0; i < enemyList.Count; i++) { enemyList[i].Name += $" {i+1}"; }
                    break;
                case "Lynel":
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList[0].Name += " 1";
                    enemyList.Add(new BattleEnemies.LynelBattle(partyLevelAvg));
                    enemyList[1].Name += " 1";
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList[2].Name += " 2";
                    break;
                case "Boss":
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList[0].Name += " 1";
                    enemyList.Add(new BossBattle(partyLevelAvg));
                    enemyList[1].Name += " 1";
                    enemyList.Add(new BattleEnemies.MoblinBattle(partyLevelAvg));
                    enemyList[2].Name += " 2";
                    break;
                default:
                    throw new Exception("Enemy type not recognized in InitializeBattle");
            }

            return enemyList;
        }

        private static List<IBattle> SetAllies()
        {
            List<IBattle> allyList = new List<IBattle>();

            foreach (var ally in Player.Instance.Party)
            {
                switch (ally.Type)
                {
                    case CharType.Link:
                        allyList.Add(new LinkBattle(ally));
                        break;
                    case CharType.OldLady:
                        allyList.Add(new OldLadyBattle(ally));
                        break;
                    case CharType.Zelda:
                        allyList.Add(new ZeldaBattle(ally));
                        break;
                    case CharType.Merchant:
                        allyList.Add(new MerchantBattle(ally));
                        break;
                    default:
                        throw new Exception("Ally type not recognized in InitializeBattle");
                }
            }

            return allyList;
        }
    }
}
