using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class ArmosBattle : IBattle
{
    public EnemyStats Stats { get; set; }
    public enum ArmosActions { ShineArmour, ChargeForward}

    public bool IsFainted { get; set; }

    public ArmosBattle(EnemyStats stats)
    {
        Stats = stats;
        IsFainted = false;
        Stats.ChangeHealth(Stats.GetMaxHealth());
    }

    public int TakeTurn()
    {
        int damageOutput = 0;
        switch (ChooseAction())
        {
            case ArmosActions.ShineArmour:
                Stats.ChangeHealth(3);
                break;
            case ArmosActions.ChargeForward:
                damageOutput = 4;
                break;
        }
        return damageOutput;
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
