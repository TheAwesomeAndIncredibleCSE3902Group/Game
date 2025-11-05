using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Stats;

class PlayerStats : IStats
{
    private int currentHealth;
    private int currentOverallLevel;
    private int currentSpecialPointCount;
    private int currentSpeedLevel;
    private int currentAttackLevel;
    private int currentDefenseLevel;
    private int currentWeaponUseLevel;
    private int currentSpecialAttackLevel;
    private int currentSpecialDefenseLevel;
    private int currentLuckLevel;


    #region Health Methods
    public int GetHealth()
    {
        return currentHealth;
    }
    public int ChangeHealth(int updateHealth)
    {
        if (updateHealth != 0)
        {
            currentHealth += updateHealth;
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        return currentHealth;
    }
    #endregion

    #region Stats Methods
    #region Stat Getters
    public int GetSpecialPointCount()
    {
        return currentSpecialPointCount;
    }
    public int GetSpeedLevel()
    {
        return currentSpeedLevel;
    }
    public int GetAttackLevel()
    {
        return currentAttackLevel;
    }
    public int GetDefenseLevel()
    {
        return currentDefenseLevel;
    }
    public int GetWeaponUseLevel()
    {
        return currentWeaponUseLevel;
    }
    public int GetSpecialAttackLevel()
    {
        return currentSpecialAttackLevel;
    }
    public int GetSpecialDefenseLevel()
    {
        return currentSpecialDefenseLevel;
    }
    public int GetLuckLevel()
    {
        return currentLuckLevel;
    }
    #endregion

    #region Stat Changers
    public int ChangeSpecialPoint(int updateSpecialPointCount)
    {
        if (updateSpecialPointCount != 0)
        {
            currentSpecialPointCount += updateSpecialPointCount;
        }
        return currentSpecialPointCount;
    }
    public int ChangeSpeed(int updateSpeed)
    {
        if (updateSpeed != 0)
        {
            currentSpeedLevel += updateSpeed;
        }
        return currentSpeedLevel;
    }

    public int ChangeAttack(int updateAttack)
    {
        if (updateAttack != 0)
        {
            currentAttackLevel += updateAttack;
        }
        return currentAttackLevel;
    }

    public int ChangeDefense(int updateDefense)
    {
        if (updateDefense != 0)
        {
            currentDefenseLevel += updateDefense;
        }
        return currentDefenseLevel;
    }

    public int ChangeWeaponUse(int updateWeaponUse)
    {
        if (updateWeaponUse != 0)
        {
            currentWeaponUseLevel += updateWeaponUse;
        }
        return currentWeaponUseLevel;
    }

    public int ChangeSpecialAttack(int updateSpecialAttack)
    {
        if (updateSpecialAttack != 0)
        {
            currentSpecialAttackLevel += updateSpecialAttack;
        }
        return currentSpecialAttackLevel;
    }

    public int ChangeSpecialDefense(int updateSpecialDefense)
    {
        if (updateSpecialDefense != 0)
        {
            currentSpecialDefenseLevel += updateSpecialDefense;
        }
        return currentSpecialDefenseLevel;
    }

    public int ChangeLuck(int updateLuck)
    {
        if (updateLuck != 0)
        {
            currentLuckLevel += updateLuck;
        }
        return currentLuckLevel;
    }
    #endregion
    #endregion
    #region Level Methods
    public int GetLevel(int updateOverallLevel)
    {
        if (updateOverallLevel != 0)
        {
            currentOverallLevel += updateOverallLevel;
        }
        return currentOverallLevel;
    }
    #endregion
}
