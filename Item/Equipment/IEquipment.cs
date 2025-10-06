using System;

namespace Sprint0;

public interface IEquipment
{
    public enum Weapons { bow,boomerangSack,swordSheathe}
    public enum Projectiles {arrow,boomerang,sword}
    public void Use();

}
