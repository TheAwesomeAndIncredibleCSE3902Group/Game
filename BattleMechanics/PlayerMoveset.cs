using AwesomeRPG.Characters;
using System;
using System.Security.Cryptography;
using static AwesomeRPG.IEquipment;
using static AwesomeRPG.Util;

namespace AwesomeRPG.BattleMechanics
{
    public class PlayerMoveset
    {

        private Player _player;
        static public bool TurnIsActive {  get; set; }

        public void InitializePlayer()
        {
            _player = Player.Instance;
        }

        public static int Attack()
        {
            int attackDamage = RandomNumberGenerator.GetInt32(5);
            return attackDamage;
        }

        public void Defend()
        {
            
        }

        public void UseItem(Weapons selectedWeapon)
        {
            // need to have this affect enemies in battle
            _player.UseEquipment(selectedWeapon);
        }

        public static bool RunAway()
        {
            bool hasRunAwayBattle = false;
            if (RandomNumberGenerator.GetInt32(1) == 1)
            {
                hasRunAwayBattle = true;
            }
            return hasRunAwayBattle;
        }

    }
}
