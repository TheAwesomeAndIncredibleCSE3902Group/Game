using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Sprites;
using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.BattleMechanics.BattleEnemies
{
    public class OldLadyBattle : PlayerBattle
    {
        public OldLadyBattle(PlayerStats stats) : base(stats)
        {
            Name = "Old Lady";
            AnimatableSprite animatableSprite = TeamSpriteFactory.Instance.CreateOldSprite() as AnimatableSprite;
            animatableSprite.SetScale(2);
            Icon = animatableSprite;
        }

        public void WiseAdvice(int enemyIndex)
        {
            int specialAttackVal = Stats.GetSpecialAttack();
            int specialDefenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetSpecialDefense();
            int damageVal = specialAttackVal - (specialDefenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} gave {BattleScene.Instance.EnemyList[enemyIndex].Name} wise advice for {damageVal} damage!\n";
            if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
            {
                BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
                TurnText += $"{BattleScene.Instance.EnemyList[enemyIndex].Name} has fainted!";
            }
        }
    }
}
