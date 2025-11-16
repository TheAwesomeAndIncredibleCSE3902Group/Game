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
        switch (ChooseAction())
        {
            case ArmosActions.ShineArmour:
                Stats.ChangeHealth(3);
                break;
            case ArmosActions.ChargeForward:
                target.Stats.ChangeHealth(-4);
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
