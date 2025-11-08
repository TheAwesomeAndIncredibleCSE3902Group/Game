using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Stats;

public interface IStats
{
    public int GetHealth();
    public int GetLevel();
    public int GetSpeed();
}
