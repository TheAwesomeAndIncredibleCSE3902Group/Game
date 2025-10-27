using AwesomeRPG.UI.Elements;

namespace AwesomeRPG.UI.Events;

public abstract class UIEventBase
{
    public ElementBase Element { get; protected set; }
}