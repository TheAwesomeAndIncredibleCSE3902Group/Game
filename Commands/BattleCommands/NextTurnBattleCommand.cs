using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.Commands;

namespace Sprint0.Commands.BattleCommands
{
    public class NextTurnBattleCommand : ICommand
    {
        public NextTurnBattleCommand() { }
        public void Execute() 
        {
            BattleScene.Instance.NextTurn();
        }
    }
}
