using AwesomeRPG.BattleMechanics.BattleEnemies;
using System.Collections.Generic;

namespace AwesomeRPG.BattleMechanics
{
    public class BattleScene
    {
        public bool CurrentlyInBattle { get; set; }
        public IBattle CurrentBattle { get; private set; }

        private TurnList turnOrder;


        public void NextTurn()
        {
            CurrentBattle = turnOrder.NextBattle();
        }

        public void InitializeBattleSequence(bool isPlayerStartingFirst, List<IBattle> enemiesInBattle, List<IBattle> playersInBattle)
        {
            SetTurnOrder(isPlayerStartingFirst, enemiesInBattle, playersInBattle);
            CurrentBattle = turnOrder.GetBattle(0);
            CurrentlyInBattle = true;
        }

        private void SetTurnOrder(bool isPlayerStartingFirst, List<IBattle> enemyList, List<IBattle> playerList)
        {
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

            turnOrder = new TurnList(turnList);
            turnOrder.SetEnemies(enemyList);
            turnOrder.SetAllies(playerList);
        }


    }
}