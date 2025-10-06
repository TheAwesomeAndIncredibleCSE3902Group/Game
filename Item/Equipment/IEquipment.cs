using System;

namespace Sprint0;

public interface IEquipment
{
    public enum Weapons { bow,boomerangSack,swordSheathe,superSwordSheathe}
    public enum Projectiles {arrow,boomerang,sword,swordBeam}
    public void Use();

}
