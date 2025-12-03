// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.ElementFactories;

public class RectElementFactory : IElementFactory
{
    private RootElement RootElem {get; set;}

    public RectElementFactory(RootElement rootElement)
    {
        RootElem = rootElement;
    }

    public Element CreateNew()
    {
        var elem = new Element(RootElem); 

        elem.Attributes["fill_color"] = new Color(255, 255, 255);
        elem.Attributes["outline_color"] = new Color(0,0,0);
        elem.Attributes["outline_thickness"] = 0;

        elem.AddActionOnUIEvent(UIEvent.Draw, (e) => {
            Color fillColor = (Color) elem.Attributes["fill_color"];
            Color outlineColor = (Color) elem.Attributes["outline_color"];
            int outlineThickness = (int) elem.Attributes["outline_thickness"];

            elem.RootElement.SpriteBatch.Draw(
                elem.RootElement.RectangleTexture,
                new Rectangle(elem.DerivedAbsolutePosition, elem.OffsetAndSize.Size),
                fillColor * elem.Opacity
            );

            if (outlineThickness > 0)
            {
                var outlineTopRect = new Rectangle(
                    elem.DerivedAbsolutePosition.X - outlineThickness,
                    elem.DerivedAbsolutePosition.Y - outlineThickness,
                    elem.OffsetAndSize.Width + outlineThickness * 2,
                    outlineThickness
                );
                var outlineBottomRect = new Rectangle(
                    elem.DerivedAbsolutePosition.X - outlineThickness,
                    elem.DerivedAbsolutePosition.Y + elem.OffsetAndSize.Height,
                    elem.OffsetAndSize.Width + outlineThickness * 2,
                    outlineThickness
                );
                var outlineLeftRect = new Rectangle(
                    elem.DerivedAbsolutePosition.X - outlineThickness,
                    elem.DerivedAbsolutePosition.Y,
                    outlineThickness,
                    elem.OffsetAndSize.Height
                );
                var outlineRightRect = new Rectangle(
                    elem.DerivedAbsolutePosition.X + elem.OffsetAndSize.Width,
                    elem.DerivedAbsolutePosition.Y,
                    outlineThickness,
                    elem.OffsetAndSize.Height
                );
                elem.RootElement.SpriteBatch.Draw(elem.RootElement.RectangleTexture, outlineTopRect, outlineColor * elem.Opacity);
                elem.RootElement.SpriteBatch.Draw(elem.RootElement.RectangleTexture, outlineBottomRect, outlineColor * elem.Opacity);
                elem.RootElement.SpriteBatch.Draw(elem.RootElement.RectangleTexture, outlineLeftRect, outlineColor * elem.Opacity);
                elem.RootElement.SpriteBatch.Draw(elem.RootElement.RectangleTexture, outlineRightRect, outlineColor * elem.Opacity);
            }
        });

        return elem;
    }

    public Element CreateNew(Color fillColor)
    {
        var elem = CreateNew();
        elem.Attributes["fill_color"] = fillColor;
        return elem;
    }

    public Element CreateNew(Color fillColor, int outlineThickness, Color outlineColor)
    {
        var elem = CreateNew();
        elem.Attributes["fill_color"] = fillColor;
        elem.Attributes["outline_thickness"] = outlineThickness;
        elem.Attributes["outline_color"] = outlineColor;
        return elem;
    }
}
