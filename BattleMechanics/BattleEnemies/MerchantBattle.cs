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
    public class MerchantBattle : PlayerBattle
    {
        public MerchantBattle(PlayerStats stats) : base(stats) 
        {
            Name = "Merchant";
            AnimatableSprite animatableSprite = TeamSpriteFactory.Instance.CreateMerchantSprite() as AnimatableSprite;
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
                        (levelUps * 2), (levelUps * 5), (levelUps), (levelUps * 2), (levelUps / 2), (levelUps), (levelUps * 2), (levelUps / 2), (levelUps * 2)
                    );
                ((PlayerStats)Stats).levelUps = 0;
                Stats.ChangeHealth(Stats.GetMaxHealth());
            }
        }
        public void ThrowGold(int enemyIndex)
        {
            if (((PlayerStats)Stats).specialPointCount < 5) { Attack(enemyIndex); TurnText += "\nYou do not have enough special points to do this move."; return; }
            ((PlayerStats)Stats).specialPointCount -= 5;
            target = BattleScene.Instance.EnemyList[enemyIndex];
            int defenseVal = target.Stats.GetDefense();
            int damageVal = attackVal - (defenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            target.Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} threw gold at {target.Name} for {damageVal} damage!\nTheir special points are now {((PlayerStats)Stats).specialPointCount}\n";
            EnemyFainted();
        }
    }
}
