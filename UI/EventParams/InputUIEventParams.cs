// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System.Collections.Generic;
using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Events;

public class InputUIEventParams : UIEventParamsBase
{
    public List<UIControl> Controls { get; protected set; }
    public InputUIEventParams(ElementBase element, List<UIControl> controls)
    {
        Element = element;
        Controls = controls;
    }
}