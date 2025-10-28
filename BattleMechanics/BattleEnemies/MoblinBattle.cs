using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class MoblinBattle : IEnemyBattle
{
    public static MoblinBattle Instance { get; private set; }
    public int CurrentHealth { get; set; }
    public enum MoblinActions { ScratchBellyButton, RambleCharge, Dance }
    public bool IsFainted { get; set; }
    public int speed = 3;

    public MoblinBattle()
    {
        Instance = this;
        CurrentHealth = 8;
        IsFainted = false;
    }

    public int TakeActionTurn()
    {
        int damageOutput = 0;
        switch (ChooseAction())
        {
            case MoblinActions.ScratchBellyButton:
                CurrentHealth += 3;
                break;
            case MoblinActions.RambleCharge:
                damageOutput = 4;
                break;
            case MoblinActions.Dance:
                CurrentHealth--;
                break;
        }
        return damageOutput;
    }

    private MoblinActions ChooseAction()
    {
        MoblinActions moblinChoice = MoblinActions.ScratchBellyButton;
        if (CurrentHealth < 5)
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
