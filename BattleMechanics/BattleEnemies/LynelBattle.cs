using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class LynelBattle : IBattle
{
    public EnemyStats Stats { get; set; }
    public enum LynelActions { BrushBackHair, HardStomp, StabNSlash }

    public bool IsFainted { get; set; }
    
    public LynelBattle(EnemyStats stats)
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
            case LynelActions.BrushBackHair:
                Stats.ChangeHealth(1);
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

        if (Stats.GetHealth() < 5)
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
