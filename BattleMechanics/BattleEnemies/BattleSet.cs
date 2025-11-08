using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class BattleSet
{
    private int currentActiveBattle = 0;

    private List<IBattle> currentSet;

    public BattleSet(List<IBattle> battleSet) 
    {
        currentSet = battleSet;
    }
    public List<IBattle> GetList()
    {
        return currentSet;
    }

    public void RotateThroughActive()
    {
        foreach (IBattle currentBattle in currentSet)
        {
            currentBattle.TakeTurn();
        }
        currentActiveBattle = 0;
    }

    public void RemoveDead(int deadIndex)
    {
        currentSet.RemoveAt(deadIndex);
    }

}
