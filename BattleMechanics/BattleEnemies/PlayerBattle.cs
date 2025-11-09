using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class PlayerBattle : IBattle
{
    public PlayerStats Stats { get; set; }

    public bool IsFainted { get; set; }

    public PlayerBattle(PlayerStats stats)
    {
        Stats = stats;
    }


    public void Attack(IStats enemyStats)
    {
        int attackVal = Stats.GetAttack();
        int defenseVal = enemyStats.GetDefense();
        int damageVal =  defenseVal - attackVal;

        enemyStats.ChangeHealth(damageVal);
        if (damageVal > 0)
        {

        }
    }
}
