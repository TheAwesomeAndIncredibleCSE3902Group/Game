using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Stats;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace AwesomeRPG.BattleMechanics;
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
    public static BattleScene Instance { get; private set; } = new BattleScene();

    public void NextTurn()
    {
        CurrentBattle = turnOrder.NextBattle();
        if (turnOrder.battleEnd)
        {
            int totalXP = 0;
            CurrentlyInBattle = false;

            foreach (IBattle battle in EnemyList) totalXP += ((EnemyStats)battle.Stats).GetXPReward();
            foreach (IBattle battle in AllyList)
            {
                ((PlayerStats)battle.Stats).ChangeLevel(totalXP);

            }

            AllyList.Clear();
            EnemyList.Clear();
            turnOrder = new TurnList();
            return;
        }
        if (!CurrentBattle.IsFriend)
        {
            //progress straight into text element with EnemyActionBattleCommand
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
            turnList = new List<IBattle>(AllyList);
            turnList.AddRange(new List<IBattle>(EnemyList));
        }
        else
        {
            turnList = new List<IBattle>(EnemyList);
            turnList.AddRange(new List<IBattle>(AllyList));
        }

        turnOrder = new TurnList(turnList);
    }



}
