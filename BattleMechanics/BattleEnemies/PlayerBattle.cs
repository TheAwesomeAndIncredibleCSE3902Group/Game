using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public abstract class PlayerBattle : IBattle
{
    public IStats Stats { get; set; }

    public bool IsFriend { get; set; } = true;
    public bool IsFainted { get; set; } = false;

    public string Name { get; set; } = "Player";
    public string TurnText { get; set; } = null;

    public PlayerBattle(PlayerStats stats)
    {
        Stats = stats;
        IsFainted = false;
        IsFriend = true;
    }

    public void LevelUp()
    {
        PlayerStats stats = ((PlayerStats)Stats);
        int level = stats.GetLevel();
        stats.ChangeAll
            (
                (level * 2), (level * 5), (level), (level), (level), (level), (level), (level), ((level * 3) / 4)
            );
    }

    public void Attack(int enemyIndex)
    {
        int attackVal = Stats.GetAttack();
        int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
        int damageVal =  defenseVal - attackVal;

        BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(damageVal);

        TurnText = $"{Name} attack value: {attackVal}. enemy defense value: {defenseVal}\nPlayer attacked for {Math.Abs(damageVal)} damage!\nEnemy's health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
        if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
        {
            BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
            TurnText += $"Enemy has fainted!";
        }
    }
}
