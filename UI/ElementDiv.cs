// This is the root element that will contain every other class. This should only be used once.

using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace Sprint0.UI;

public class ElementDiv : ElementBase
{

    public override void Draw(IElement parent)
    {
        
        // We will draw each of this element's children.
        foreach (IElement child in Children)
        {
            child.Draw(this);
        }
    }

    public ElementDiv(ElementRoot rootElement)
    {
        RootElement = rootElement;
    }
}