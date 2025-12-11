using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            int levelUps = ((PlayerStats)Stats).levelUps;
            if (levelUps > 0)
            {
                ((PlayerStats)Stats).ChangeAll
                (
                    (levelUps * 20), (levelUps * 5), (levelUps), (levelUps * 4), (levelUps), (levelUps), (levelUps), (levelUps / 2), ((levelUps * 3) / 4)
                );
                ((PlayerStats)Stats).levelUps = 0;
                Stats.ChangeHealth(Stats.GetMaxHealth());
            }
        }

        public void SpinAttack(int enemyIndex)
        {
            if (((PlayerStats)Stats).specialPointCount < 5) { Attack(enemyIndex); TurnText += "\nYou do not have enough special points to do this move."; return; }
            ((PlayerStats)Stats).specialPointCount -= 5;
            int damageVal = attackVal;

            foreach (IBattle enemy in BattleScene.Instance.EnemyList)
            {
                target = enemy;
                int defenseVal = enemy.Stats.GetDefense();
                damageVal = attackVal - defenseVal;
                if (damageVal < 0) damageVal = 0;
                enemy.Stats.ChangeHealth(-damageVal);
            }


            TurnText = $"{Name} spinned around with their sword, hitting all enemies for {damageVal} damage!\nTheir special points are now {((PlayerStats)Stats).specialPointCount}\n";
            EnemyFainted();
        }
    }
}
