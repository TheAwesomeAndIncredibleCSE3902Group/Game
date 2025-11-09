using AwesomeRPG;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.Characters;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;
using AwesomeRPG.Stats;
using System.Linq;

namespace AwesomeRPG.BattleMechanics
{
    public class BattleScene
    {
        public bool CurrentlyInBattle { get; set; }

        private enum BattleMoves { Attack, Defend, ItemUse, Flee }

        private List<IStats> playersInBattle;
        private List<IStats> enemiesInBattle;
        private List<IBattle> turnOrder;
        private BattleSet currentEnemySet;
        private BattleSet currentPlayerSet;
        private int currentTurnIndex = 0;

        public static Dictionary<string, IBattle[]> TotalEnemySets { get; set; }

        private void NextTurn()
        {
            if (currentTurnIndex >= turnOrder.Count)
            {
                currentTurnIndex = 0;
            }

            turnOrder[currentTurnIndex].TakeTurn();
            currentTurnIndex++;
        }
        public void Update()
        {
            if (enemiesInBattle.Count == 0)
            {
                // TODO: put reward system in here and exit the battle
                CurrentlyInBattle = false;
            } else
            {
                NextTurn();
            }
        }

        public void InitializeBattleSequence(bool isPlayerStartingFirst, List<IBattle> enemiesInBattle, List<IBattle> playersInBattle)
        {
            currentPlayerSet = new BattleSet(playersInBattle);
            currentEnemySet = new BattleSet(enemiesInBattle);
            SetTurnOrder(isPlayerStartingFirst);
            CurrentlyInBattle = true;
        }

        private void SetTurnOrder(bool isPlayerStartingFirst)
        {
            List<IBattle> enemyList = currentEnemySet.GetList();
            List<IBattle> playerList = currentPlayerSet.GetList();

            //We either do this or have a temporary speed boost to whoever starts first, what that specifically means depends on implementation of speed stat.
            if (isPlayerStartingFirst)
            {
                turnOrder = playerList;
                turnOrder.AddRange(enemyList);
            }
            else
            {
                turnOrder = enemyList;
                turnOrder.AddRange(playerList);
            }
        }


    }
}