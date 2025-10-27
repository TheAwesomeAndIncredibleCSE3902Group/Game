using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;


namespace AwesomeRPG.UI.Elements;

public class DivElement : ElementBase
{
    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEvent(this, gameTime));
        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEvent(this, gameTime));
    }

    public DivElement(RootElement rootElement)
    {
        SetUpElement(rootElement);
    }
}
