using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Stats;

class EnemyStats : IStats
{
    private int maxHealth;
    private int health;
    private int speed;
    private int attack;
    private int defense;
    private int specialAttack;
    private int specialDefense;
    private int luck;

    private int level;
    private int xpReward;

    public EnemyStats(int maxHealth, int speed, int attack, int defense,
                      int specialAttack, int specialDefense, int luck,
                      int level, int xpReward)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.speed = speed;
        this.attack = attack;
        this.defense = defense;
        this.specialAttack = specialAttack;
        this.specialDefense = specialDefense;
        this.luck = luck;
        this.level = level;
        this.xpReward = xpReward;
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
            if (health < 1)
            {
                health = 0;
            }
            else if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        return health;
    }

    #endregion

    #region Stat Methods

    #region Stat Getters
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

    public int GetCurrentLuckLevel(int updateLuck)
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
    public int GetXPReward()
    {
        return xpReward;
    }
    #endregion
}
