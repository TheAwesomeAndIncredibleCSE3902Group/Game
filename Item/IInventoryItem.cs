public interface IInventoryItem
{
    enum Type { potion }

    public void Apply();
    public Type ThisType { get; }
}