using AwesomeRPG.BattleMechanics.BattleEnemies;
using System.Collections.Generic;

namespace AwesomeRPG.BattleMechanics
{
    public class BattleScene
    {
        public bool CurrentlyInBattle { get; set; }
        public IBattle CurrentBattle { get; private set; }
        public List<IBattle> AllyList { get; private set; }
        public List<IBattle> EnemyList { get; private set; }

        private TurnList turnOrder;

        private BattleScene() 
        {
            AllyList = new List<IBattle>();
            EnemyList = new List<IBattle>();
        }
        private static BattleScene instance = new BattleScene();
        public static BattleScene Instance { get { return instance; } }

        public void NextTurn()
        {
            CurrentBattle = turnOrder.NextBattle();
            if (turnOrder.battleEnd)
            {
                CurrentlyInBattle = false;

                AllyList = new List<IBattle>();
                EnemyList = new List<IBattle>();
                turnOrder = new TurnList();
            }
        }

        public void InitializeBattleSequence(bool isPlayerStartingFirst, List<IBattle> enemiesInBattle, List<IBattle> playersInBattle)
        {
            AllyList = playersInBattle;
            EnemyList = enemiesInBattle;
            SetTurnOrder(isPlayerStartingFirst);
            CurrentBattle = turnOrder.GetBattle(0);
            CurrentlyInBattle = true;
        }

        private void SetTurnOrder(bool isPlayerStartingFirst)
        {
            List<IBattle> turnList = new List<IBattle>();

            //We either do this or have a temporary speed boost to whoever starts first, what that specifically means depends on implementation of speed stat.
            if (isPlayerStartingFirst)
            {
                turnList = AllyList;
                turnList.AddRange(EnemyList);
            }
            else
            {
                turnList = EnemyList;
                turnList.AddRange(AllyList);
            }

            turnOrder = new TurnList(turnList);
        }


    }
}