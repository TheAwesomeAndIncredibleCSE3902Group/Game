using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Events;

public class UICommandEvent : UIEventBase
{
    public UIControl Control { get; set; }
    public UICommandEvent(ElementBase element, UIControl control)
    {
        Element = element;
        Control = control;
    }
}