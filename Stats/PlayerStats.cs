using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Stats;

public class PlayerStats : IStats
{
    private int maxHealth;
    private int health;
    private int level;
    private int specialPointCount;
    private int speed;
    private int attack;
    private int defense;
    private int weaponUse;
    private int specialAttack;
    private int specialDefense;
    private int luck;
    private int xp;

    public int levelUps;
    public CharType Type { get; set; }

    public PlayerStats(CharType type) : this(100, 10, 10, 10, 2, 10, 10, 2, 1)
    {
        this.Type = type;
        switch (type)
        {
            case CharType.Link:
                maxHealth = 150;
                attack = 15;
                defense = 5;
                break;
            case CharType.OldLady:
                maxHealth = 80;
                specialAttack = 15;
                specialPointCount = 15;
                break;
            case CharType.Zelda:
                maxHealth = 100;
                specialDefense = 4;
                speed = 15;
                luck = 10;
                break;
            case CharType.Merchant:
                maxHealth = 100;
                attack = 12;
                specialAttack = 12;
                break;
            default:
                // Default stats are already set in the constructor call
                break;
        }
        this.health = maxHealth;
    }
    public PlayerStats(int maxHealth, int specialPointCount, int speed, int attack, int defense, int weaponUse, int specialAttack, int specialDefense, int luck)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.level = 1;
        this.specialPointCount = specialPointCount;
        this.speed = speed;
        this.attack = attack;
        this.defense = defense;
        this.weaponUse = weaponUse;
        this.specialAttack = specialAttack;
        this.specialDefense = specialDefense;
        this.luck = luck;
    }
    #region Health Methods
    public int GetMaxHealth() { return maxHealth; }
    public int GetHealth() { return health; }
    public int ChangeHealth(int updateHealth)
    {
        if (updateHealth != 0)
        {
            health += updateHealth;
            if (health >  maxHealth) health = maxHealth;
        }

        if (health < 0)
        {
            health = 0;
        }

        return health;
    }
    #endregion
    #region Stats Methods
    #region Stat Getters
    public int GetSpecialPoint() { return specialPointCount; }
    public int GetSpeed() { return speed; }
    public int GetAttack() { return attack; }
    public int GetDefense() { return defense; }
    public int GetWeaponUse() { return weaponUse; }
    public int GetSpecialAttack() { return specialAttack; }
    public int GetSpecialDefense() { return specialDefense; }
    public int GetLuck() { return luck; }
    #endregion

    #region Stat Changers
    public void ChangeAll(int maxHealthChange, int specialPointCountChange, int speedChange, int attackChange, int defenseChange, int weaponUseChange, int specialAttackChange, int specialDefenseChange, int luckChange)
    {
        this.maxHealth += maxHealthChange;
        this.health += maxHealthChange;
        this.specialPointCount += specialPointCountChange;
        this.speed += speedChange;
        this.attack += attackChange;
        this.defense += defenseChange;
        this.weaponUse += weaponUseChange;
        this.specialAttack += specialAttackChange;
        this.specialDefense += specialDefenseChange;
        this.luck += luckChange;
    }
    public int ChangeSpecialPoint(int updateSpecialPointCount)
    {
        if (updateSpecialPointCount != 0)
        {
            specialPointCount += updateSpecialPointCount;
        }
        return specialPointCount;
    }
    public int ChangeSpeed(int updateSpeed)
    {
        if (updateSpeed != 0)
        {
            speed += updateSpeed;
        }
        return speed;
    }

    public int ChangeAttack(int updateAttack)
    {
        if (updateAttack != 0)
        {
            attack += updateAttack;
        }
        return attack;
    }

    public int ChangeDefense(int updateDefense)
    {
        if (updateDefense != 0)
        {
            defense += updateDefense;
        }
        return defense;
    }

    public int ChangeWeaponUse(int updateWeaponUse)
    {
        if (updateWeaponUse != 0)
        {
            weaponUse += updateWeaponUse;
        }
        return weaponUse;
    }

    public int ChangeSpecialAttack(int updateSpecialAttack)
    {
        if (updateSpecialAttack != 0)
        {
            specialAttack += updateSpecialAttack;
        }
        return specialAttack;
    }

    public int ChangeSpecialDefense(int updateSpecialDefense)
    {
        if (updateSpecialDefense != 0)
        {
            specialDefense += updateSpecialDefense;
        }
        return specialDefense;
    }

    public int ChangeLuck(int updateLuck)
    {
        if (updateLuck != 0)
        {
            luck += updateLuck;
        }
        return luck;
    }
    #endregion
    #endregion
    #region Level Methods
    public int GetLevel() { return level; }
    public int GetXP() { return xp; }
    public int ChangeLevel(int xpGain)
    {
        if (xpGain != 0)
        {
            xp += xpGain;
            levelUps = xp / 100;
            level += levelUps;
            xp = xp % 100;
        }
        return level;
    }
    #endregion
}
