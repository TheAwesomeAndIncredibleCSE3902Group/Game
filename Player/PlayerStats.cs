using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

class PlayerStats : ICharacterStats
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

    public int GetCurrentHealth(int updateHealth)
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

    public int GetCurrentOverallLevel(int updateOverallLevel)
    {
        if (updateOverallLevel != 0)
        {
            currentOverallLevel += updateOverallLevel;
        }
        return currentOverallLevel;
    }

    public int GetSpecialPointCount(int updateSpecialPointCount)
    {
        if (updateSpecialPointCount != 0)
        {
            currentSpecialPointCount += updateSpecialPointCount;
        }
        return currentSpecialPointCount;
    }
    public int GetCurrentSpeedLevel(int updateSpeed)
    {
        if (updateSpeed != 0)
        {
            currentSpeedLevel += updateSpeed;
        }
        return currentSpeedLevel;
    }

    public int GetCurrentAttackLevel(int updateAttack)
    {
        if (updateAttack != 0)
        {
            currentAttackLevel += updateAttack;
        }
        return currentAttackLevel;
    }

    public int GetCurrentDefenseLevel(int updateDefense)
    {
        if (updateDefense != 0)
        {
            currentDefenseLevel += updateDefense;
        }
        return currentDefenseLevel;
    }

    public int GetCurrentWeaponUseLevel(int updateWeaponUse)
    {
        if (updateWeaponUse != 0)
        {
            currentWeaponUseLevel += updateWeaponUse;
        }
        return currentWeaponUseLevel;
    }

    public int GetCurrentSpecialAttackLevel(int updateSpecialAttack)
    {
        if (updateSpecialAttack != 0)
        {
            currentSpecialAttackLevel += updateSpecialAttack;
        }
        return currentSpecialAttackLevel;
    }

    public int GetCurrentSpecialDefenseLevel(int updateSpecialDefense)
    {
        if (updateSpecialDefense != 0)
        {
            currentSpecialDefenseLevel += updateSpecialDefense;
        }
        return currentSpecialDefenseLevel;
    }

    public int GetCurrentLuckLevel(int updateLuck)
    {
        if (updateLuck != 0)
        {
            currentLuckLevel += updateLuck;
        }
        return currentLuckLevel;
    }
}
