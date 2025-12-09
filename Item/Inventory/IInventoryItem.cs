public interface IInventoryItem
{
    enum Type { potion, boomerang, bow, beamSword }

    public void Apply();
    public Type ThisType { get; }
}