using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class ArmosBattle : IEnemyBattle
{
    public IStats Stats { get; set; }
    public enum ArmosActions { ShineArmour, ChargeForward}

    public bool IsFriend { get; set; }
    public bool IsFainted { get; set; }

    public String Name { get; set; } = "Armos";
    public String TurnText { get; set; } = null;

    public ArmosBattle(EnemyStats stats)
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
            case ArmosActions.ShineArmour:
                healthChangeVal = 3;
                Stats.ChangeHealth(healthChangeVal);
                TurnText = $"{Name} healed for {healthChangeVal}";
                break;
            case ArmosActions.ChargeForward:
                healthChangeVal = 4;
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} attacked {target.Name} for {healthChangeVal}";
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
