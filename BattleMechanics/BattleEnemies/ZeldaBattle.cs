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
        public override void LevelUp()
        {
            int levelUps = ((PlayerStats)Stats).levelUps;
            ((PlayerStats)Stats).ChangeAll
                (
                    (levelUps * 5), (levelUps * 5), (levelUps * 2), (levelUps * 3), (levelUps / 2), (levelUps), (levelUps * 3), (levelUps / 2), ((levelUps * 3) / 4)
                );
            ((PlayerStats)Stats).levelUps = 0;
            Stats.ChangeHealth(Stats.GetMaxHealth());
        }
        public void LightArrow(int enemyIndex)
        {
            target = BattleScene.Instance.EnemyList[enemyIndex];
            int specialAttackVal = Stats.GetSpecialAttack();
            int specialDefenseVal = target.Stats.GetSpecialDefense();
            int damageVal = specialAttackVal - (specialDefenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            target.Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} shot a light arrow at {target.Name} for {damageVal} damage!\n";
            EnemyFainted();
        }
    }
}
