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
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
    public int ChangeHealth(int updateHealth)
    {
        if (updateHealth != 0)
        {
            health += updateHealth;
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
    public int GetSpecialPoint()
    {
        return specialPointCount;
    }
    public int GetSpeed()
    {
        return speed;
    }
    public int GetAttack()
    {
        return attack;
    }
    public int GetDefense()
    {
        return defense;
    }
    public int GetWeaponUse()
    {
        return weaponUse;
    }
    public int GetSpecialAttack()
    {
        return specialAttack;
    }
    public int GetSpecialDefense()
    {
        return specialDefense;
    }
    public int GetLuck()
    {
        return luck;
    }
    #endregion

    #region Stat Changers
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
    public int GetLevel()
    {
        return level;
    }
    public int ChangeLevel(int updateOverallLevel)
    {
        if (updateOverallLevel != 0)
        {
            level += updateOverallLevel;
        }
        return level;
    }
    #endregion
}
