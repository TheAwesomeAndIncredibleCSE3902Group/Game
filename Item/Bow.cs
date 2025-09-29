using System;

namespace Sprint0;

/// <summary>
/// The Bow can shoot an Arrow, which it assigns to Player.Arrow
/// </summary>
public class Bow : IEquipment
{
    public void Use()
    {
        Player player = Player.Instance;

        //Player can only ever have one arrow on screen, so if arrow already exists then abort
        if (player.Arrow != null)
            return;

        player.Arrow = new PlayerArrow(player.Position, player.FacingDirection, 2, 2, 2);
    }
}
