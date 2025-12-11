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

    private int attackVal; private int specialAttackVal;
    public IBattle target;

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
    public void EnemyFainted()
    {
        if (target.Stats.GetHealth() < 1)
        {
            target.IsFainted = true;
            TurnText += $"Enemy has fainted!";
        }
    }

    public void Attack(int enemyIndex)
    {
        target = BattleScene.Instance.EnemyList[enemyIndex];
        int attackVal = Stats.GetAttack();
        int defenseVal = target.Stats.GetDefense();
        int damageVal =  attackVal - defenseVal;
        if (damageVal < 0) damageVal = 0;

        target.Stats.ChangeHealth(-damageVal);

        TurnText = $"{Name} attacked for {Math.Abs(damageVal)} damage!\n{target.Name}'s health is now {target.Stats.GetHealth()}";
        EnemyFainted();
    }

    public void LuckyStrike(int enemyIndex)
    {
        target = BattleScene.Instance.EnemyList[enemyIndex];
        int luck = Stats.GetLuck();
        if (luck > 7) luck = 7;
        int attackVal = Stats.GetSpecialAttack();
        int defenseVal = target.Stats.GetDefense();
        Random random = new();
        if (random.Next(0, 10 - Stats.GetLuck()) % 2 == 0)
        {
            attackVal *= 3;
            TurnText = $"{Name} got a lucky strike on {target.Name}";
        }
        else { TurnText = $"{Name} whiffed a hit on {target.Name}"; }
        int damageVal = attackVal - defenseVal;
        if (damageVal < 0) damageVal = 0;

        TurnText += $" for {attackVal}!\n{target.Name}'s health is now {target.Stats.GetHealth()}";

        target.Stats.ChangeHealth(-damageVal);

        
        EnemyFainted();
    }
    public void Heal()
    {
        int specialAtkVal = (Stats.GetSpecialAttack() * 2);

        Stats.ChangeHealth(specialAtkVal);

        TurnText = $"{Name} healed themselves for {specialAtkVal}!";
    }
}
