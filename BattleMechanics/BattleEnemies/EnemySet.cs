using System;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics.BattleEnemies;
public class EnemySet(IEnemyBattle[] newEnemySet)
{

    private int currentActiveEnemy = 0;

    private IEnemyBattle[] currentEnemySet = newEnemySet;

    public IEnemyBattle GetCurrentEnemy()
    {
        return currentEnemySet[currentActiveEnemy];
    }

    public void RotateThroughActiveEnemies()
    {
        while (currentActiveEnemy < currentEnemySet.Length)
        {
            currentEnemySet[currentActiveEnemy].TakeActionTurn();
            currentActiveEnemy++;
        }
        currentActiveEnemy = 0;
    }

    public void RemoveDeadEnemy(int deadEnemyIndex)
    {
        IEnemyBattle[] updatedEnemySet = new IEnemyBattle[currentEnemySet.Length - 1];

        for (int i = 0; i < currentEnemySet.Length - 1; i++)
        {
            if(i != deadEnemyIndex) 
                updatedEnemySet[i] = currentEnemySet[i];
        }

        currentEnemySet = updatedEnemySet;
    }

}
