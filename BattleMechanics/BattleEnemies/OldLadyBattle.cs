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
        public override void LevelUp()
        {
            int levelUps = ((PlayerStats)Stats).levelUps;
            if (levelUps > 0)
            {
                ((PlayerStats)Stats).ChangeAll
                    (
                        (levelUps * 10), (levelUps * 10), (levelUps), (levelUps), (levelUps / 4), (levelUps), (levelUps * 5), (levelUps / 4), ((levelUps * 3) / 4)
                    );
                ((PlayerStats)Stats).levelUps = 0;
                Stats.ChangeHealth(Stats.GetMaxHealth());
            }
        }
        public void WiseAdvice(int enemyIndex)
        {
            if (((PlayerStats)Stats).specialPointCount < 5) { Attack(enemyIndex); TurnText += "\nYou do not have enough special points to do this move."; return; }
            ((PlayerStats)Stats).specialPointCount -= 5;
            target = BattleScene.Instance.EnemyList[enemyIndex];
            int specialDefenseVal = target.Stats.GetSpecialDefense();
            int damageVal = specialAttackVal - (specialDefenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            target.Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} gave {target.Name} wise advice for {damageVal} damage!\nTheir special points are now {((PlayerStats)Stats).specialPointCount}\n";
            EnemyFainted();
        }
    }
}
