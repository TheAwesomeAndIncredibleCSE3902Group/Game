// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

public class RectElement : ElementBase
{
    public Color FillColor { get; set; }
    public Color OutlineColor { get; set; } = Color.Black;
    public int OutlineThickness { get; set; } = 0;

    protected internal override void Draw(GameTime gameTime)
    {
        RootElement.SpriteBatch.Draw(
            RootElement.RectangleTexture,
            new Rectangle(DerivedAbsolutePosition, OffsetAndSize.Size),
            FillColor * Opacity
        );
        if (OutlineThickness > 0)
        {
            var outlineTopRect = new Rectangle(
                DerivedAbsolutePosition.X - OutlineThickness,
                DerivedAbsolutePosition.Y - OutlineThickness,
                OffsetAndSize.Width + OutlineThickness * 2,
                OutlineThickness
            );
            var outlineBottomRect = new Rectangle(
                DerivedAbsolutePosition.X - OutlineThickness,
                DerivedAbsolutePosition.Y + OffsetAndSize.Height,
                OffsetAndSize.Width + OutlineThickness * 2,
                OutlineThickness
            );
            var outlineLeftRect = new Rectangle(
                DerivedAbsolutePosition.X - OutlineThickness,
                DerivedAbsolutePosition.Y,
                OutlineThickness,
                OffsetAndSize.Height
            );
            var outlineRightRect = new Rectangle(
                DerivedAbsolutePosition.X + OffsetAndSize.Width,
                DerivedAbsolutePosition.Y,
                OutlineThickness,
                OffsetAndSize.Height
            );
            RootElement.SpriteBatch.Draw(RootElement.RectangleTexture, outlineTopRect, OutlineColor * Opacity);
            RootElement.SpriteBatch.Draw(RootElement.RectangleTexture, outlineBottomRect, OutlineColor * Opacity);
            RootElement.SpriteBatch.Draw(RootElement.RectangleTexture, outlineLeftRect, OutlineColor * Opacity);
            RootElement.SpriteBatch.Draw(RootElement.RectangleTexture, outlineRightRect, OutlineColor * Opacity);
        }
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

    public RectElement(RootElement rootElement, Color fillColor, int outlineThickness, Color outlineColor)
    {
        SetUpElement(rootElement);
        FillColor = fillColor;
        OutlineThickness = outlineThickness;
        OutlineColor = outlineColor;
    }
}
