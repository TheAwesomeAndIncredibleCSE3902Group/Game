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

        public void ThrowGold(int enemyIndex)
        {
            int attackVal = Stats.GetAttack();
            int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
            int damageVal = attackVal - (defenseVal / 2);
            if (damageVal < 0) damageVal = 0;

            BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);
            TurnText = $"{Name} threw gold at {BattleScene.Instance.EnemyList[enemyIndex].Name} for {damageVal} damage!\n";
            if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
            {
                BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
                TurnText += $"{BattleScene.Instance.EnemyList[enemyIndex].Name} has fainted!";
            }
        }
    }
}
