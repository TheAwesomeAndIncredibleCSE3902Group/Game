public interface IInventoryItem
{
    enum Type { potion, boomerang, bow, beamSword }

    public Type ThisType { get; }
}