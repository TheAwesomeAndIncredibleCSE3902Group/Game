// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Events;

public class DrawUIEvent : UIEventBase
{
    public GameTime GameTime { get; set; }
    public DrawUIEvent(ElementBase element, GameTime gameTime)
    {
        Element = element;
        GameTime = gameTime;
    }
}