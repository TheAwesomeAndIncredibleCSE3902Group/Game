// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.ElementFactories;

namespace AwesomeRPG.UI.Events;

public abstract class UIEventParamsBase
{
    public Element Element { get; protected set; }
}