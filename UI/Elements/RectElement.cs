using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

public class RectElement : ElementBase
{
    public Color FillColor { get; set; }

    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEvent(this, gameTime));

        RootElement.SpriteBatch.Draw(
            RootElement.RectangleTexture,
            new Rectangle(DerivedAbsolutePosition, OffsetAndSize.Size),
            FillColor
        );

        // System.Console.WriteLine("drawing rect element at" + DerivedAbsolutePosition.ToString());
        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEvent(this, gameTime));
    }

    public RectElement(RootElement rootElement)
    {
        SetUpElement(rootElement);
        FillColor = new Color(255, 255, 255, 255);
    }

    public RectElement(RootElement rootElement, Color fillColor)
    {
        SetUpElement(rootElement);
        FillColor = fillColor;
    }
}
