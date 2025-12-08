using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.BattleMechanics.BattleEnemies
{
    public class OldManBattle : PlayerBattle
    {
        public OldManBattle(PlayerStats stats) : base(stats)
        {
            Name = "Old Man";
        }

        public void WiseAdvice(int enemyIndex)
        {
            int specialAttackVal = Stats.GetSpecialAttack();
            int specialDefenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetSpecialDefense();
            int damageVal = specialAttackVal - (specialDefenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} special attack value: {specialAttackVal}. enemy special defense value: {specialDefenseVal}\nPlayer used Wise Advice for {Math.Abs(damageVal)} damage!\nEnemy's health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
            if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
            {
                BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
                TurnText += $"Enemy has fainted!";
            }
        }
    }
