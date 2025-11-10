using System;
using AwesomeRPG.Stats;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public interface IBattle
{
    public bool IsFainted { get; set; }
    public bool IsFriend { get; set; }
    public IStats Stats { get; set; }

}
