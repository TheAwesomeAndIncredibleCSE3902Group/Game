using System;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class LynelBattle : IEnemyBattle
{
    public CharacterEnemyBase.CType Type { get; } = CharacterEnemyBase.CType.lynel;
    public IStats Stats { get; set; }
    public enum LynelActions { RecklessCharge, HardStomp, StabNSlash }

    public bool IsFriend { get; set; } = false;
    public bool IsFainted { get; set; } = false;

    public string Name { get; set; } = "Lynel";
    public string TurnText { get; set; } = null;

    #region Constructors
    public LynelBattle(int level)
    {
        // Basic stat scaling based on level
        int maxHealth = 25 + (level * 10);
        int speed = 10 + (level * 4);
        int attack = 7 + (level * 3);
        int defense = 5 + ((level * 3) / 2);
        int specialAttack = 3 + level;
        int specialDefense = 3 + level;
        int luck = 1 + (level / 2);
        int xpReward = 50;

        Stats = new EnemyStats(maxHealth, speed, attack, defense, specialAttack, specialDefense, luck, level, xpReward);
    }
    public LynelBattle(EnemyStats stats)
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
            case LynelActions.RecklessCharge:
                healthChangeVal = Stats.GetSpeed();
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} recklessly charged {target.Name} for {healthChangeVal}";
                break;
            case LynelActions.HardStomp:
                healthChangeVal = Stats.GetAttack() - target.Stats.GetDefense();
                if (healthChangeVal < 0) healthChangeVal = 0;

                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} stomped {target.Name} for {healthChangeVal}";
                break;
            case LynelActions.StabNSlash:
                healthChangeVal = Stats.GetAttack() - (target.Stats.GetDefense() / 2);
                if (healthChangeVal < 0) healthChangeVal = 0;

                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} stabbed {target.Name} for {healthChangeVal}";
                break;
        }
        if (target.Stats.GetHealth() < 1) { target.IsFainted = true; }
    }

    private LynelActions ChooseAction()
    {
        LynelActions lynelChoice = LynelActions.RecklessCharge;

        if (Stats.GetHealth() > (Stats.GetMaxHealth() / 3))
        {
            Random random = new();
            int coinFlip = random.Next(0, 2);
            if (coinFlip == 0)
            {
                lynelChoice = LynelActions.HardStomp;
            }
            else
            {
                lynelChoice = LynelActions.StabNSlash;
            }   
        }
        return lynelChoice;
    }
}
