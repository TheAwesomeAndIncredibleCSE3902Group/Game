using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI;

public class RectElement : ElementBase
{
    public Color FillColor { get; set; } = new Color(255, 255, 255, 0);

    public override void Draw(GameTime gameTime, ElementBase parent)
    {
        CalculateDerivedValuesFromAncestors(parent);

        RootElement.SpriteBatch.Draw(
            RootElement.RectangleTexture,
            new Rectangle(DerivedAbsolutePosition, OffsetAndSize.Size),
            FillColor
        );

        DrawChildren(gameTime);
    }

    public RectElement(ElementRoot rootElement)
    {
        RootElement = rootElement;
    }

    public RectElement(ElementRoot rootElement, Color fillColor)
    {
        RootElement = rootElement;
        FillColor = fillColor;
    }
}
