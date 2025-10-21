// This is the element that holds commands. It is selectable by default.

using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI;

public class ActionElement : ElementBase
{

    public override void Draw(GameTime gameTime, ElementBase parent)
    {
        CalculateDerivedValuesFromAncestors(parent);

        DrawChildren(gameTime);
    }

    public ActionElement(ElementRoot rootElement)
    {
        RootElement = rootElement;
    }
}
