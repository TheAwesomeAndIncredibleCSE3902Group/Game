using System;
using System.Collections.Generic;
using static Sprint0.Util;

namespace Sprint0;

/// <summary>
/// TODO:
/// I recommend this be implemented by the enumerated state machine (see GoombaStateMachineExample(FullEnumeration).
///     Consider using Decorator pattern for restricted/odd states, ie damage invulernability
///     I know prof talked about using the state-by-type pattern for all states, but I find that very hard to extend with additional data fields or values
/// 
/// Anything that influences state transitions should go in here
/// </summary>
public interface IPlayerState
{
    public Cardinal Direction { get; }
    public int Health { get; }
    public int MaxHealth { get; }
    //Equipment might not need to be part of state, but it does drive the sprite, so I put it here for now
    public IEquipment ActiveEquipment { get; set; }
    public void ChangeDirection(Cardinal direction);
    public void TakeDamage(int amount = 1);
    public void Heal(int amount);
    public void UseEquipment();
}
