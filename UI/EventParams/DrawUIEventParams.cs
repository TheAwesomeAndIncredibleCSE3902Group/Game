// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Events;

public class DrawUIEventParams : UIEventParamsBase
{
    public GameTime GameTime { get; set; }
    public DrawUIEventParams(ElementBase element, GameTime gameTime)
    {
        Element = element;
        GameTime = gameTime;
    }
}