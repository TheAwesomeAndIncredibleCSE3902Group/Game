using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Stats;

public interface IStats
{
    public int GetHealth(); 
    public int GetMaxHealth();
    public int GetLevel();
    public int GetAttack();
    public int GetSpeed();
    public int GetDefense();
    public int GetSpecialAttack();
    public int GetSpecialDefense();
    public int GetLuck();
    public int ChangeHealth(int change);
}
