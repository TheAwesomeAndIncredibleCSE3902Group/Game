using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class LynelBattle : IEnemyBattle
{
    public IStats Stats { get; set; }
    public enum LynelActions { BrushBackHair, HardStomp, StabNSlash }

    public bool IsFriend { get; set; }
    public bool IsFainted { get; set; }
    public String TurnText { get; set; }

    public LynelBattle(EnemyStats stats)
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
        switch (ChooseAction())
        {
            case LynelActions.BrushBackHair:
                Stats.ChangeHealth(1);
                TurnText = $"Lynel healed for 1";
                break;
            case LynelActions.HardStomp:
                target.Stats.ChangeHealth(-3);
                TurnText = $"Moblin attacked someone for 3";
                break;
            case LynelActions.StabNSlash:
                target.Stats.ChangeHealth(-5);
                TurnText = $"Moblin attacked someone for 5";
                break;
        }
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
