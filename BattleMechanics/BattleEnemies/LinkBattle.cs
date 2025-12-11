using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Sprites;
using AwesomeRPG.Stats;

namespace Sprint0.BattleMechanics.BattleEnemies
{
    public class LinkBattle : PlayerBattle
    {
        public LinkBattle(PlayerStats stats) : base(stats)
        {
            Name = "Link";
            AnimatableSprite animatableSprite = TeamSpriteFactory.Instance.CreateLinkSprite() as AnimatableSprite;
            animatableSprite.SetScale(2);
            Icon = animatableSprite;
        }

        public override void LevelUp()
        {
            PlayerStats stats = ((PlayerStats)Stats);
            int levelUps = stats.levelUps;
            stats.ChangeAll
                (
                    (levelUps * 2), (levelUps * 5), (levelUps), (levelUps * 4), (levelUps * 2), (levelUps), (levelUps), (levelUps), ((levelUps * 3) / 4)
                );
            stats.levelUps = 0;
        }

        public void SwordStab(int enemyIndex)
        {
            int attackVal = Stats.GetAttack();
            int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
            int damageVal = attackVal - (defenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);

            TurnText = $"{Name} stabbed {BattleScene.Instance.EnemyList[enemyIndex].Name} for {damageVal} damage!\n";
            if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
            {
                BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
                TurnText += $"{BattleScene.Instance.EnemyList[enemyIndex].Name} has fainted!";
            }
        }
    }
}
