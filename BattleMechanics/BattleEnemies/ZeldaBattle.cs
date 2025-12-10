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
    public class ZeldaBattle : PlayerBattle
    {
        public ZeldaBattle(PlayerStats stats) : base(stats)
        {
            Name = "Zelda";
            AnimatableSprite animatableSprite = TeamSpriteFactory.Instance.CreateZeldaSprite() as AnimatableSprite;
            animatableSprite.SetScale(2);
            Icon = animatableSprite;
        }
        public void LightArrow(int enemyIndex)
        {
            int specialAttackVal = Stats.GetSpecialAttack();
            int specialDefenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetSpecialDefense();
            int damageVal = specialAttackVal - (specialDefenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} shot a light arrow at {BattleScene.Instance.EnemyList[enemyIndex].Name} for {damageVal} damage!\n";
            if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
            {
                BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
                TurnText += $"{BattleScene.Instance.EnemyList[enemyIndex].Name} has fainted!";
            }
        }
    }
}
