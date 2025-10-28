using AwesomeRPG;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.Characters;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using System;
using System.Collections.Generic;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics
{
    public class BattleScene
    {
        public bool CurrentlyInBattle { get; set; }

        private enum BattleMoves { Attack, Defend, ItemUse, Flee }

        private Player link;
        private ICharacter[] enemiesInBattle;
        private EnemySet currentEnemySet;

        private ArmosBattle armos = new();
        private MoblinBattle moblin = new();
        private LynelBattle lynel = new();
        public static Dictionary<string, IEnemyBattle[]> TotalEnemySets { get; set; }

        private void PlayOutTurnOrder()
        {
            // When the player is active, they can take their time to choose their next action,
            // otherwise the enemies in the current battle are active and thier turns play out.
            if (!PlayerMoveset.TurnIsActive)
            {
                currentEnemySet.RotateThroughActiveEnemies();
                PlayerMoveset.TurnIsActive = true;
                link.PStateMachine.currentDamageIntake = 1;
            }


        }
        public void Update()
        {
            if (enemiesInBattle.Length == 0)
            {
                // TODO: put reward system in here and exit the battle
                CurrentlyInBattle = false;
            } else
            {
                PlayOutTurnOrder();
            }
        }

        public void InitializeBattleSequence(Player link, bool isPlayerStartingFirst, string currentBattleList)
        {
            this.link = link;

            TotalEnemySets.Add("dungeonArmos", [armos, lynel, moblin, moblin]);
            TotalEnemySets.Add("fieldMoblinPair", [moblin, moblin]);
            TotalEnemySets.Add("fieldMoblinFleet", [moblin, moblin, moblin, moblin, moblin, moblin]);
            TotalEnemySets.Add("fieldLynelTwins", [lynel, lynel]);
            TotalEnemySets.Add("fieldArmos", [armos]);

            SetCurrentEnemiesInBattle(currentBattleList);
            SetTurnOrder(isPlayerStartingFirst);
            CurrentlyInBattle = true;
        }
        private void SetCurrentEnemiesInBattle(string currentBattleList)
        {
            TotalEnemySets.TryGetValue(currentBattleList, out IEnemyBattle[] currentEnemyList);
            currentEnemySet = new EnemySet(currentEnemyList);
        }

        private static void SetTurnOrder(bool isPlayerStartingFirst)
        {
            if (isPlayerStartingFirst)
            {
                PlayerMoveset.TurnIsActive = true;
            }
            else
            {
                PlayerMoveset.TurnIsActive = false;
            }

        }


    }
}