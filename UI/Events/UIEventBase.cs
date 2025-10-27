// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Elements;

namespace AwesomeRPG.UI.Events;

public abstract class UIEventBase
{
    public ElementBase Element { get; protected set; }
}