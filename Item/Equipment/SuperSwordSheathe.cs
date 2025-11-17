using System;
using System.Collections.Generic;
using AwesomeRPG.Map;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// The SuperSwordSheathe that holds a sword with a swordBeam
/// </summary>
public class SuperSwordSheathe : Equipment
{
    protected override List<Projectile> CreateProjectiles()
    {
        Player player = Player.Instance;
        return [new PlayerSword(player.Position, player.FacingDirection),new SwordBeam(player.Position,player.FacingDirection)];
    }
}
