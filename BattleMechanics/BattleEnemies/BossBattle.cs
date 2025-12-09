using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;
using static AwesomeRPG.BattleMechanics.BattleEnemies.ArmosBattle;
using static AwesomeRPG.BattleMechanics.BattleEnemies.MoblinBattle;

namespace Sprint0.BattleMechanics.BattleEnemies
{
    public class BossBattle : IBattle, IEnemyBattle
    {
        public IStats Stats { get; set; }
        public enum BossActions { WildSwing, GutPunch, EatMeal }

        public bool IsFriend { get; set; } = false;
        public bool IsFainted { get; set; } = false;

        public string Name { get; set; } = "Boss";
        public string TurnText { get; set; } = null;
        public BossBattle(int level)
        {
            // Basic stat scaling based on level
            int maxHealth = 50 + (level * 10);
            int speed = 5 + level;
            int attack = 8 + (level * 2);
            int defense = 4 + ((level * 3) / 2);
            int specialAttack = 5 + level;
            int specialDefense = 3 + level;
            int luck = 1 + (level / 2);
            int xpReward = 50 + (level * 5);

            Stats = new EnemyStats(maxHealth, speed, attack, defense, specialAttack, specialDefense, luck, level, xpReward);
        }
        public BossBattle(EnemyStats stats)
        {
            Stats = stats;
            IsFainted = false;
            IsFriend = false;
            Stats.ChangeHealth(Stats.GetMaxHealth());
        }

        public void TakeTurn()
        {
            int rand = new Random().Next(BattleScene.Instance.AllyList.Count);
            IBattle target = BattleScene.Instance.AllyList[rand];
            int healthChangeVal = 0;

            switch (ChooseAction())
            {
                case BossActions.EatMeal:
                    healthChangeVal = Stats.GetSpecialAttack() / 2;
                    Stats.ChangeHealth(healthChangeVal);
                    TurnText = $"{Name} ate a delicious meal for {healthChangeVal}";
                    break;
                case BossActions.WildSwing:
                    healthChangeVal = Stats.GetAttack() - target.Stats.GetDefense();
                    if (healthChangeVal < 0) healthChangeVal = 0;

                    target.Stats.ChangeHealth(-healthChangeVal);
                    TurnText = $"{Name} took a wild swing at {target.Name} for {healthChangeVal}";
                    break;
                case BossActions.GutPunch:
                    healthChangeVal = Stats.GetAttack() - (target.Stats.GetDefense() / 2);
                    if (healthChangeVal < 0) healthChangeVal = 0;

                    target.Stats.ChangeHealth(-healthChangeVal);
                    TurnText = $"{Name} gut punched {target.Name} for {healthChangeVal}";
                    break;
            }
        }

        private BossActions ChooseAction()
        {
            BossActions bossChoice = BossActions.EatMeal;

            if (Stats.GetHealth() > (Stats.GetMaxHealth() / 3))
            {
                Random random = new();
                int danceChance = random.Next(0, 3);
                if (danceChance % 2 == 0)
                {
                    bossChoice = BossActions.WildSwing;
                }
                else
                {
                    bossChoice = BossActions.GutPunch;
                }
            }
            return bossChoice;
        }
    }
}
