using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class PlayerBattle : IBattle
{
    public IStats Stats { get; set; }

    public bool IsFriend { get; set; }
    public bool IsFainted { get; set; }

    public PlayerBattle(PlayerStats stats)
    {
        Stats = stats;
        IsFainted = false;
        IsFriend = true;
    }


    public void Attack(IBattle enemy)
    {
        int attackVal = Stats.GetAttack();
        int defenseVal = enemy.Stats.GetDefense();
        int damageVal =  defenseVal - attackVal;

        enemy.Stats.ChangeHealth(damageVal);
        if (enemy.Stats.GetHealth() < 1)
        {
            enemy.IsFainted = true;
        }
    }
}
