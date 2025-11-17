using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class MoblinBattle : IEnemyBattle
{
    public IStats Stats { get; set; }
    public enum MoblinActions { ScratchBellyButton, RambleCharge, Dance }
    public bool IsFainted { get; set; }
    public bool IsFriend { get; set; }
    public String TurnText { get; set; }

    public MoblinBattle(EnemyStats stats)
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
            case MoblinActions.ScratchBellyButton:
                Stats.ChangeHealth(3);
                TurnText = $"Moblin healed for 3";
                break;
            case MoblinActions.RambleCharge:
                target.Stats.ChangeHealth(-4);
                TurnText = $"Moblin attacked someone for 4";
                break;
            case MoblinActions.Dance:
                target.Stats.ChangeHealth(-1);
                TurnText = $"Moblin damaged someone for 1";
                break;
        }
    }

    private MoblinActions ChooseAction()
    {
        MoblinActions moblinChoice = MoblinActions.ScratchBellyButton;
        if (Stats.GetHealth() < 5)
        {
            Random random = new();
            int danceChance = random.Next(0, 20);
            if (danceChance % 2 == 7)
            {
                moblinChoice = MoblinActions.RambleCharge;
            }
            else
            {
                moblinChoice = MoblinActions.Dance;
            }
        }
        return moblinChoice;
    }
}
