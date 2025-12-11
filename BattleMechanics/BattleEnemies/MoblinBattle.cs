using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class MoblinBattle : IEnemyBattle
{
    public CharacterEnemyBase.CType Type { get; } = CharacterEnemyBase.CType.moblin;
    public IStats Stats { get; set; }
    public enum MoblinActions { ScratchBellyButton, RambleCharge, Dance }
    public bool IsFainted { get; set; } = false;
    public bool IsFriend { get; set; } = false;

    public string Name { get; set; } = "Moblin";
    public string TurnText { get; set; } = null;

    #region Constructors
    public MoblinBattle(int level)
    {
        // Basic stat scaling based on level
        int maxHealth = 10 + (level * 2);
        int speed = 5 + level;
        int attack = 5 + level;
        int defense = 3 + level;
        int specialAttack = 2 + level;
        int specialDefense = 2 + level;
        int luck = 1 + (level / 2);
        int xpReward = 5 + (level * 3);

        Stats = new EnemyStats(maxHealth,speed,attack,defense,specialAttack,specialDefense,luck,level,xpReward);
    }
    public MoblinBattle(EnemyStats stats)
    {
        Stats = stats;
        IsFainted = false;
        IsFriend = false;
        Stats.ChangeHealth(Stats.GetMaxHealth());
    }
    #endregion

    public void TakeTurn()
    {
        int rand = new Random().Next(BattleScene.Instance.AllyList.Count);
        IBattle target = BattleScene.Instance.AllyList[rand];
        int healthChangeVal = 0;

        switch (ChooseAction())
        {
            case MoblinActions.ScratchBellyButton:
                healthChangeVal = Stats.GetSpecialAttack() / 4;
                if (healthChangeVal < 0) healthChangeVal = 0;

                Stats.ChangeHealth(healthChangeVal);
                TurnText = $"{Name} scratched its belly and healed for {healthChangeVal}";
                break;
            case MoblinActions.RambleCharge:
                healthChangeVal = ((Stats.GetAttack() * 3) / 2) - target.Stats.GetDefense();
                if (healthChangeVal < 0) healthChangeVal = 0;

                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"The {Name} charged at {target.Name} for {healthChangeVal}\nTheir health is now: {target.Stats.GetHealth()}";
                break;
            case MoblinActions.Dance:
                healthChangeVal = Stats.GetAttack() - target.Stats.GetDefense();
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"The {Name}'s horrible dance caused {target.Name} to suffer {healthChangeVal} damage";
                break;
        }
        if (target.Stats.GetHealth() < 1) { target.IsFainted = true; }
    }

    private MoblinActions ChooseAction()
    {
        MoblinActions moblinChoice = MoblinActions.ScratchBellyButton;
        if (Stats.GetHealth() > (Stats.GetMaxHealth() / 3))
        {
            Random random = new();
            int danceChance = random.Next(0, 3);
            if (danceChance % 2 == 0)
            {
                moblinChoice = MoblinActions.RambleCharge;
            }
            else
            {
                moblinChoice = MoblinActions.Dance;
            }
        }
        return moblinChoice;
    }
}
