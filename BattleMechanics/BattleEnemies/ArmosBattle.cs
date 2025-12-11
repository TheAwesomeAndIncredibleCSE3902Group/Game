using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using System;
using System.ComponentModel.Design;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class ArmosBattle : IEnemyBattle
{
    public CharacterEnemyBase.CType Type { get; } = CharacterEnemyBase.CType.armos;
    public IStats Stats { get; set; }
    public enum ArmosActions { ShieldBash, ChargeForward}

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
        int attack = 6 + (level * 2);
        int defense = 5 + ((level * 3) / 2);
        int specialAttack = 2 + level;
        int specialDefense = 3 + level;
        int luck = 1 + (level / 2);
        int xpReward = 25;

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
        if (target.IsFainted) { foreach (IBattle player in BattleScene.Instance.AllyList) { if (!player.IsFainted) { target = player; break; } } }
        int healthChangeVal = 0;

        switch (ChooseAction())
        {
            case ArmosActions.ShieldBash:
                healthChangeVal = (Stats.GetDefense() + (Stats.GetAttack() / 2)) - target.Stats.GetDefense();
                if (healthChangeVal < 0) healthChangeVal = 0;

                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} bashed {target.Name} with their shield for {healthChangeVal}";
                break;
            case ArmosActions.ChargeForward:
                healthChangeVal = Stats.GetAttack() - target.Stats.GetDefense();
                if (healthChangeVal < 0) healthChangeVal = 0;

                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} charged {target.Name} for {healthChangeVal}";
                break;
        }
        if (target.Stats.GetHealth() < 1) { target.IsFainted = true; }
    }

    private ArmosActions ChooseAction()
    {
        ArmosActions armosChoice = new ArmosActions();
        Random random = new();
        int coinFlip = random.Next(0, 4);
        if (coinFlip == 0) 
        {
            armosChoice = ArmosActions.ShieldBash;
        }
        else
        {
            armosChoice = ArmosActions.ChargeForward;
        }
        return armosChoice;
    }

}
