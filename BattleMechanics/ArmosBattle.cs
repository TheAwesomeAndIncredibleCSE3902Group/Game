using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics;
public class ArmosBattle : IEnemyBattle
{
    public static ArmosBattle Instance { get; private set; }
    public int CurrentHealth { get; set; }
    public enum ArmosActions { ShineArmour, ChargeForward}

    public bool IsFainted { get; set; }
    public int speed = 2;

    public ArmosBattle()
    {
        Instance = this;
        CurrentHealth = 14;
        IsFainted = false;
    }

    public int TakeActionTurn()
    {
        int damageOutput = 0;
        switch (ChooseAction())
        {
            case ArmosActions.ShineArmour:
                CurrentHealth += 3;
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
            
        if (CurrentHealth < 5)
        {
            armosChoice = ArmosActions.ChargeForward;
        }
        return armosChoice;
    }

}
