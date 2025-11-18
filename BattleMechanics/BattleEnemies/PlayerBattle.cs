using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class PlayerBattle : IBattle
{
    public IStats Stats { get; set; }

    public bool IsFriend { get; set; }
    public bool IsFainted { get; set; }

    public String TurnText { get; set; } = null;

    public PlayerBattle(PlayerStats stats)
    {
        Stats = stats;
        IsFainted = false;
        IsFriend = true;
    }


    public void Attack(int enemyIndex)
    {
        int attackVal = Stats.GetAttack();
        int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
        int damageVal =  defenseVal - attackVal;

        BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(damageVal);

        TurnText = $"player attack value: {attackVal}. enemy defense value: {defenseVal}\nPlayer attacked for {Math.Abs(damageVal)} damage!\nEnemy's health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
        if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
        {
            BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
            TurnText += $"Enemy has fainted!";
        }
    }
}
