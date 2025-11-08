using AwesomeRPG.Stats;
using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class PlayerBattle : IBattle
{
    public PlayerStats Stats { get; set; }
    public enum ArmosActions { ShineArmour, ChargeForward}

    public bool IsFainted { get; set; }

    public PlayerBattle(PlayerStats stats)
    {
        Stats = stats;
    }

    public int TakeTurn()
    {
        int damageOutput = 0;

        return damageOutput;
    }

}
