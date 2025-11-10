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
        public IBattle CurrentBattle { get; private set; }

        private BattleSet turnOrder;
        private BattleSet currentEnemySet;
        private BattleSet currentPlayerSet;


        public void NextTurn()
        {
            CurrentBattle = turnOrder.NextBattle();
        }

        public void InitializeBattleSequence(bool isPlayerStartingFirst, List<IBattle> enemiesInBattle, List<IBattle> playersInBattle)
        {
            currentPlayerSet = new BattleSet(playersInBattle);
            currentEnemySet = new BattleSet(enemiesInBattle);
            SetTurnOrder(isPlayerStartingFirst);
            CurrentBattle = turnOrder.GetBattle(0);
            CurrentlyInBattle = true;
        }

        private void SetTurnOrder(bool isPlayerStartingFirst)
        {
            List<IBattle> enemyList = currentEnemySet.GetList();
            List<IBattle> playerList = currentPlayerSet.GetList();
            List<IBattle> turnList = new List<IBattle>();

            //We either do this or have a temporary speed boost to whoever starts first, what that specifically means depends on implementation of speed stat.
            if (isPlayerStartingFirst)
            {
                turnList = playerList;
                turnList.AddRange(enemyList);
            }
            else
            {
                turnList = enemyList;
                turnList.AddRange(playerList);
            }

            turnOrder = new BattleSet(turnList);
            turnOrder.SetEnemies(enemyList);
            turnOrder.SetAllies(playerList);
        }


    }
}