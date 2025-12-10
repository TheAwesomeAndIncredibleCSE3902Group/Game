using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeRPG.Characters;

namespace AwesomeRPG.BattleMechanics.BattleEnemies
{
    public interface IEnemyBattle : IBattle
    {
        public CharacterEnemyBase.CType Type { get; }
        public void TakeTurn() { }
    }
}
