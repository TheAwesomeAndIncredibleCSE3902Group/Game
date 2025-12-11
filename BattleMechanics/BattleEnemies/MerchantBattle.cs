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
            ((PlayerStats)Stats).ChangeAll
                (
                    (levelUps * 2), (levelUps * 5), (levelUps), (levelUps * 2), (levelUps / 2), (levelUps), (levelUps * 2), (levelUps / 2), (levelUps * 2)
                );
            ((PlayerStats)Stats).levelUps = 0;
            Stats.ChangeHealth(Stats.GetMaxHealth());
        }
        public void ThrowGold(int enemyIndex)
        {
            int attackVal = Stats.GetAttack();
            int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
            int damageVal = attackVal - (defenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} threw gold at {BattleScene.Instance.EnemyList[enemyIndex].Name} for {damageVal} damage!\n";
            EnemyFainted(enemyIndex);
        }
    }
}
