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
        ((PlayerStats)Stats).ChangeSpecialPoint(((PlayerStats)Stats).GetSpecialPointMax());
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
    public void EnemyFainted(int enemyIndex)
    {
        if (BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth() < 1)
        {
            BattleScene.Instance.EnemyList[enemyIndex].IsFainted = true;
            TurnText += $"Enemy has fainted!";
        }
    }

    public void Attack(int enemyIndex)
    {
        int attackVal = Stats.GetAttack();
        int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
        int damageVal =  attackVal - defenseVal;
        if (damageVal < 0) damageVal = 0;

        BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);

        TurnText = $"{Name} attacked for {Math.Abs(damageVal)} damage!\n{BattleScene.Instance.EnemyList[enemyIndex].Name}'s health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
        EnemyFainted(enemyIndex);
    }

    public void LuckyStrike(int enemyIndex)
    {
        int luck = Stats.GetLuck();
        if (luck > 7) luck = 7;
        int attackVal = Stats.GetAttack();
        int defenseVal = BattleScene.Instance.EnemyList[enemyIndex].Stats.GetDefense();
        int damageVal = attackVal;
        Random random = new();
        if (random.Next(0, 10 - Stats.GetLuck()) % 2 == 0)
        {
            damageVal *= 10;
            TurnText = $"{Name} got a lucky strike on {BattleScene.Instance.EnemyList[enemyIndex].Name} for {damageVal}!\n{BattleScene.Instance.EnemyList[enemyIndex].Name}'s health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}";
        }
        else { TurnText = $"{Name} whiffed a hit for {Math.Abs(damageVal)} damage\n{BattleScene.Instance.EnemyList[enemyIndex].Name}'s health is now {BattleScene.Instance.EnemyList[enemyIndex].Stats.GetHealth()}"; }
        damageVal = attackVal - defenseVal;
        if (damageVal < 0) damageVal = 0;

        BattleScene.Instance.EnemyList[enemyIndex].Stats.ChangeHealth(-damageVal);

        
        EnemyFainted(enemyIndex);
    }
    public void Heal()
    {
        int specialAtkVal = (Stats.GetSpecialAttack() * 2);

        Stats.ChangeHealth(specialAtkVal);

        TurnText = $"{Name} healed themselves for {specialAtkVal}!";
    }
}
