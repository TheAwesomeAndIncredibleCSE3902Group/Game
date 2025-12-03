// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.ElementFactories;

public class SelectionAnimationElementFactory : IElementFactory
{
    private RootElement RootElem { get; set; }

    public SelectionAnimationElementFactory(RootElement rootElement)
    {
        RootElem = rootElement;
    }

    public Element CreateNew()
    {
        var elem = new Element(RootElem);
        GameTime lastGameTime = null;

        elem.AddActionOnUIEvent(UIEvent.BeforeDraw, (e) =>
        {
            elem.CalculateDerivedValuesFromAncestors();
            if (e is DrawUIEventParams drawParams)
            {
                lastGameTime = drawParams.GameTime;
            }
        });

        elem.AddActionOnUIEvent(UIEvent.Draw, (e) =>
        {
            Console.WriteLine("Will be drawing selection.");
            if (elem.DerivedAncestorIsSelected && elem.DerivedAncestorIsVisible && lastGameTime != null)
            {
                int animationFrame = (int)lastGameTime.TotalGameTime.TotalMicroseconds / 9000 % 100;

                var sizedRectangle1 = new Rectangle(elem.DerivedAbsolutePosition, elem.OffsetAndSize.Size);
                sizedRectangle1.Inflate(2, 2);

                elem.RootElement.SpriteBatch.Draw(
                    elem.RootElement.RectangleTexture,
                    sizedRectangle1,
                    Color.LightBlue * 1
                );

                Color selectColor = new Color(255, 255, 255);
                var sizedRectangle = new Rectangle(elem.DerivedAbsolutePosition, elem.OffsetAndSize.Size);
                sizedRectangle.Inflate(animationFrame / 8 + 2, animationFrame / 8 + 2);
                
                Console.WriteLine("Drawing the selection shit!\n" + sizedRectangle);

                elem.RootElement.SpriteBatch.Draw(
                    elem.RootElement.RectangleTexture,
                    sizedRectangle,
                    selectColor * ((99 - animationFrame) / 2 / 255.0f)
                );
            }
        });

        return elem;
    }
}
