// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;


namespace AwesomeRPG.UI.Elements;

public class DivElement : ElementBase
{
    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(this, gameTime));
        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(this, gameTime));
    }

    public DivElement(RootElement rootElement)
    {
        SetUpElement(rootElement);
    }
}
