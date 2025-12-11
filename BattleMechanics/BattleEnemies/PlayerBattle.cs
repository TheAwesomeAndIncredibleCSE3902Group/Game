using AwesomeRPG.Sprites;
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
    public AnimatableSprite Icon { get; set; } = null;
    public string TurnText { get; set; } = null;

    public PlayerBattle(PlayerStats stats)
    {
        Stats = stats;
        IsFainted = false;
        IsFriend = true;
    }

    public virtual void LevelUp()
    {
        PlayerStats stats = ((PlayerStats)Stats);
        int levelUps = stats.levelUps;
        Debug.WriteLine("Generic player level up");
        stats.ChangeAll
            (
                (levelUps * 2), (levelUps * 5), (levelUps), (levelUps), (levelUps), (levelUps), (levelUps), (levelUps), ((levelUps * 3) / 4)
            );
        stats.levelUps = 0;
    }

    public void Attack(int enemyIndex)
    {
        int attackVal = Stats.GetAttack();
        int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
        int damageVal =  attackVal - defenseVal;
        if (damageVal < 0) damageVal = 0;

        BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);

        TurnText = $"{Name} attack value: {attackVal}. enemy defense value: {defenseVal}\n{Name} attacked for {Math.Abs(damageVal)} damage!\n{BattleScene.Instance.EnemyList[enemyIndex].Name}'s health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
        if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
        {
            BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
            TurnText += $"Enemy has fainted!";
        }
    }
    public void Heal()
    {
        int specialAtkVal = (Stats.GetSpecialAttack() * 2);

        Stats.ChangeHealth(specialAtkVal);

        TurnText = $"{Name} healed themselves for {specialAtkVal}!";
    }
}
