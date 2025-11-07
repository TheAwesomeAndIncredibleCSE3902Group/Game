using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class MoblinBattle : IBattle
{
    public EnemyStats Stats { get; set; }
    public enum MoblinActions { ScratchBellyButton, RambleCharge, Dance }
    public bool IsFainted { get; set; }

    public MoblinBattle(EnemyStats stats)
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
            case MoblinActions.ScratchBellyButton:
                Stats.ChangeHealth(3);
                break;
            case MoblinActions.RambleCharge:
                damageOutput = 4;
                break;
            case MoblinActions.Dance:
                Stats.ChangeHealth(-1);
                break;
        }
        return damageOutput;
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
