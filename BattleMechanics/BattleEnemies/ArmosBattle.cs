using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class ArmosBattle : IEnemyBattle
{
    public IStats Stats { get; set; }
    public enum ArmosActions { ShineArmour, ChargeForward}

    public bool IsFriend { get; set; } = false;
    public bool IsFainted { get; set; } = false;

    public string Name { get; set; } = "Armos";
    public string TurnText { get; set; } = null;

    #region Constructors
    public ArmosBattle(int level)
    {
        // Basic stat scaling based on level
        int maxHealth = 20 + (level * 4);
        int speed = 5 + level;
        int attack = 4 + level;
        int defense = 4 + ((level * 3) / 2);
        int specialAttack = 2 + level;
        int specialDefense = 1 + level;
        int luck = 1 + (level / 2);
        int xpReward = 5 + (level * 3);

        Stats = new EnemyStats(maxHealth, speed, attack, defense, specialAttack, specialDefense, luck, level, xpReward);
    }
    public ArmosBattle(EnemyStats stats)
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
            case ArmosActions.ShineArmour:
                healthChangeVal = Stats.GetSpecialAttack() / 2;
                Stats.ChangeHealth(healthChangeVal);
                TurnText = $"{Name} healed for {healthChangeVal}";
                break;
            case ArmosActions.ChargeForward:
                healthChangeVal = Stats.GetAttack() - target.Stats.GetAttack();
                if (healthChangeVal < 0) healthChangeVal = 0;

                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} charged {target.Name} for {healthChangeVal}";
                break;
        }
    }

    private ArmosActions ChooseAction()
    {
        ArmosActions armosChoice = ArmosActions.ShineArmour;
            
        if (Stats.GetHealth() < 5)
        {
            armosChoice = ArmosActions.ChargeForward;
        }
        return armosChoice;
    }

}
