using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics;
public class LynelBattle : IEnemyBattle
{
    public static LynelBattle Instance { get; private set; }
    public int CurrentHealth { get; set; }
    public enum LynelActions { BrushBackHair, HardStomp, StabNSlash }

    public bool IsFainted { get; set; }
    public int speed = 5;

    public LynelBattle()
    {
        Instance = this;
        CurrentHealth = 20;
        IsFainted = false;
    }

    public int TakeActionTurn()
    {
        int damageOutput = 0;
        switch (ChooseAction())
        {
            case LynelActions.BrushBackHair:
                CurrentHealth++;
                break;
            case LynelActions.HardStomp:
                damageOutput = 3;
                break;
            case LynelActions.StabNSlash:
                damageOutput = 5;
                break;
        }

        return damageOutput;
    }

    private LynelActions ChooseAction()
    {
        LynelActions lynelChoice = LynelActions.BrushBackHair;

        if (CurrentHealth < 5)
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
