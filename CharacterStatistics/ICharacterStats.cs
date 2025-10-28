using System;
using System.Collections;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

interface ICharacterStats
{
    public int GetCurrentHealth(int updateHealth);
    public int GetCurrentOverallLevel(int updateOverallLevel);
    public int GetCurrentSpeedLevel(int updateSpeedlevel);
}
