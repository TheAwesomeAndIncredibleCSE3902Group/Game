using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class LynelBattle : IEnemyBattle
{
    public IStats Stats { get; set; }
    public enum LynelActions { BrushBackHair, HardStomp, StabNSlash }

    public bool IsFriend { get; set; } = false;
    public bool IsFainted { get; set; } = false;

    public string Name { get; set; } = "Lynel";
    public string TurnText { get; set; } = null;

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
        int healthChangeVal = 0;

        switch (ChooseAction())
        {
            case LynelActions.BrushBackHair:
                healthChangeVal = 1;
                Stats.ChangeHealth(healthChangeVal);
                TurnText = $"{Name} healed for {healthChangeVal}";
                break;
            case LynelActions.HardStomp:
                healthChangeVal = 3;
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} attacked {target.Name} for {healthChangeVal}";
                break;
            case LynelActions.StabNSlash:
                healthChangeVal = 5;
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"{Name} attacked {target.Name} for {healthChangeVal}";
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
