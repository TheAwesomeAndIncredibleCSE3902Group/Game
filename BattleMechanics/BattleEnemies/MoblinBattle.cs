using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class MoblinBattle : IEnemyBattle
{
    public IStats Stats { get; set; }
    public enum MoblinActions { ScratchBellyButton, RambleCharge, Dance }
    public bool IsFainted { get; set; } = false;
    public bool IsFriend { get; set; } = false;

    public string Name { get; set; } = "Moblin";
    public string TurnText { get; set; } = null;

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
        int healthChangeVal = 0;

        switch (ChooseAction())
        {
            case MoblinActions.ScratchBellyButton:
                healthChangeVal = 3;
                Stats.ChangeHealth(healthChangeVal);
                TurnText = $"{Name} scratched its belly and healed for {healthChangeVal}";
                break;
            case MoblinActions.RambleCharge:
                healthChangeVal = 4;
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"The {Name} charged at {target.Name} for {healthChangeVal}\nTheir health is now: {target.Stats.GetHealth()}";
                break;
            case MoblinActions.Dance:
                healthChangeVal = 1;
                target.Stats.ChangeHealth(-healthChangeVal);
                TurnText = $"The {Name}'s horrible dance caused {target.Name} to suffer {healthChangeVal} damage";
                break;
        }
    }

    private MoblinActions ChooseAction()
    {
        MoblinActions moblinChoice = MoblinActions.ScratchBellyButton;
        if (Stats.GetHealth() < 10)
        {
            Random random = new();
            int danceChance = random.Next(0, 3);
            if (danceChance % 2 == 0)
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
