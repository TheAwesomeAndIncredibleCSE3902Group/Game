// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.ElementFactories;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Events;

public class DrawUIEventParams : UIEventParamsBase
{
    public GameTime GameTime { get; set; }
    public DrawUIEventParams(Element element, GameTime gameTime)
    {
        Element = element;
        GameTime = gameTime;
    }
}