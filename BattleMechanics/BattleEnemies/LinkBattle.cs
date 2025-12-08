using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;

namespace Sprint0.BattleMechanics.BattleEnemies
{
    public class LinkBattle : PlayerBattle
    {
        public LinkBattle(PlayerStats stats) : base(stats)
        {
            Name = "Link";
        }

        public void SwordStab(int enemyIndex)
        {
            int attackVal = Stats.GetAttack();
            int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
            int damageVal = (defenseVal / 2) - attackVal;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(damageVal);

            TurnText = $"{Name} attack value: {attackVal}. enemy defense value: {defenseVal}\nPlayer attacked for {Math.Abs(damageVal)} damage!\nEnemy's health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
            if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
            {
                BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
                TurnText += $"Enemy has fainted!";
            }
        }
    }
}
