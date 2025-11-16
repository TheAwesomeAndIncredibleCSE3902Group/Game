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

    private int allyCount;
    private int enemyCount;

    public bool battleLost;
    public bool battleEnd;

    public TurnList() 
    {
        currentSet = new List<IBattle>();
    }
    public TurnList(List<IBattle> battleSet) 
    {
        currentSet = battleSet;
        allyCount = BattleScene.Instance.AllyList.Count;
        enemyCount = BattleScene.Instance.EnemyList.Count;
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

    public IBattle NextBattle()
    {
        currentActiveBattle = (currentActiveBattle + 1) % currentSet.Count;

        //Handles dead battles and sets battleLost if applicable
        while (currentSet[currentActiveBattle].IsFainted)
        {
            RemoveDead(currentActiveBattle);
        }
        battleEnd = BattleStatus();

        return currentSet[currentActiveBattle];
    }
    private bool BattleStatus()
    {
        if (allyCount < 1)
        {
            battleLost = true;
            return true;
        }
        else if (enemyCount < 1)
        {
            battleLost = false;
            return true;
        }
        return false;
    }
    public void RemoveDead(int deadIndex)
    {
        IBattle deadBattle = currentSet[deadIndex];
        if (BattleScene.Instance.AllyList.Contains(deadBattle))
        {
            allyCount--;
        }
        else if (BattleScene.Instance.EnemyList.Contains(deadBattle))
        {
            enemyCount--;
        }
        else
        {
            Debug.WriteLine("BattleSet.RemoveDead ERROR");
        }

        currentSet.RemoveAt(deadIndex);
    }

}
