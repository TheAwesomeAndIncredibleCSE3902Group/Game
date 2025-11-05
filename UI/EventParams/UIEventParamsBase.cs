// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Elements;

namespace AwesomeRPG.UI.Events;

public abstract class UIEventParamsBase
{
    public ElementBase Element { get; protected set; }
}