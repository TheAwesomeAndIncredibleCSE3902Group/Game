using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using static AwesomeRPG.BattleMechanics.BattleEnemies.ArmosBattle;
using static AwesomeRPG.BattleMechanics.BattleEnemies.MoblinBattle;

namespace Sprint0.BattleMechanics.BattleEnemies
{
    public class BossBattle : IBattle, IEnemyBattle
    {
        public CharacterEnemyBase.CType Type { get; } = CharacterEnemyBase.CType.boss;
        public IStats Stats { get; set; }
        public enum BossActions { WildSwing, GutPunch, PoisonBreath }

        public bool IsFriend { get; set; } = false;
        public bool IsFainted { get; set; } = false;

        public string Name { get; set; } = "Boss";
        public string TurnText { get; set; } = null;
        public BossBattle(int level)
        {
            // Basic stat scaling based on level
            int maxHealth = 50 + (level * 10);
            int speed = 5 + level;
            int attack = 10 + (level * 2);
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
            BattleScene.Instance.End = true;
        }

        public void TakeTurn()
        {
            int rand = new Random().Next(BattleScene.Instance.AllyList.Count);
            IBattle target = BattleScene.Instance.AllyList[rand];
            if (target.IsFainted) { foreach (IBattle player in BattleScene.Instance.AllyList) { if (!player.IsFainted) { target = player; break; } } }
            int healthChangeVal = 0;

            switch (ChooseAction())
            {
                case BossActions.PoisonBreath:
                    healthChangeVal = Stats.GetSpecialAttack();

                    foreach (IBattle player in BattleScene.Instance.AllyList) { player.Stats.ChangeHealth(-healthChangeVal); }
                    TurnText = $"{Name} used their poison breath on the whole party for {healthChangeVal}";
                    break;
                case BossActions.WildSwing:
                    healthChangeVal = (Stats.GetAttack() + Stats.GetSpecialAttack()) - target.Stats.GetDefense();
                    if (healthChangeVal < 0) healthChangeVal = 0;

                    target.Stats.ChangeHealth(-healthChangeVal);
                    TurnText = $"{Name} took a wild swing at {target.Name} for {healthChangeVal}";
                    break;
                case BossActions.GutPunch:
                    healthChangeVal = (Stats.GetAttack() * 2) - (target.Stats.GetDefense() / 2);
                    if (healthChangeVal < 0) healthChangeVal = 0;

                    target.Stats.ChangeHealth(-healthChangeVal);
                    TurnText = $"{Name} gut punched {target.Name} for {healthChangeVal}";
                    break;
            }
            if (target.Stats.GetHealth() < 1) { target.IsFainted = true; }
        }

        private BossActions ChooseAction()
        {
            BossActions bossChoice = BossActions.PoisonBreath;

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
