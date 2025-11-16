using AwesomeRPG.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class TurnList
{
    private int currentActiveBattle = 0;

    private List<IBattle> currentSet;
    private List<IBattle> allySet;
    private List<IBattle> enemySet;

    public bool battleLost;

    public TurnList(List<IBattle> battleSet) 
    {
        currentSet = battleSet;
    }

    #region Getters
    public List<IBattle> GetList()
    {
        return currentSet;
    }

    public IBattle GetBattle(int index)
    {
        return currentSet[index];
    }

    public IBattle GetCurrentBattle()
    {
        return currentSet[currentActiveBattle];
    }

    #endregion
    #region Setters
    public void SetAllies(List<IBattle> allies)
    {
        allySet = allies;
    }
    public void SetEnemies(List<IBattle> enemies)
    {
        enemySet = enemies;
    }
    #endregion

    public IBattle NextBattle()
    {
        currentActiveBattle = (currentActiveBattle + 1) % currentSet.Count;

        //Handles dead battles and sets battleLost if applicable
        while (currentSet[currentActiveBattle].IsFainted)
        {
            RemoveDead(currentActiveBattle);
        }
        if (allySet.Count < 1)
        {
            battleLost = true;
        }
        else if (enemySet.Count < 1)
        {
            battleLost = false;
        }

        return currentSet[currentActiveBattle];
    }
    public void RemoveDead(int deadIndex)
    {
        IBattle deadBattle = currentSet[deadIndex];
        if (allySet.Contains(deadBattle))
        {
            allySet.RemoveAt(deadIndex);
        }
        else if (enemySet.Contains(deadBattle))
        {
            enemySet.RemoveAt(deadIndex);
        }
        else
        {
            Debug.WriteLine("BattleSet.RemoveDead ERROR");
        }

        currentSet.RemoveAt(deadIndex);
    }

}
