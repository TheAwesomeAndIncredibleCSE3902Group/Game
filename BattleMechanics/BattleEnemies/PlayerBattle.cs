using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class PlayerBattle : IBattle
{
    public static PlayerBattle Instance { get; private set; }
    public enum ArmosActions { ShineArmour, ChargeForward}
    private IStats playerStats;

    public bool IsFainted { get; set; }

    public PlayerBattle(IStats stats)
    {
        playerStats = stats;
    }

    public int TakeTurn()
    {
        int damageOutput = 0;

        return damageOutput;
    }

}
