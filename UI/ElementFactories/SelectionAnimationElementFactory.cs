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

        var rectElemFact = new RectElementFactory(RootElem);

        var outlineRectElem = rectElemFact.CreateNew(new Color(0,0,0,0), 2, Color.White);
        var glowRectElem = rectElemFact.CreateNew(new Color(0,0,0,0), 2, Color.White);
        
        outlineRectElem.IsVisible = false;
        glowRectElem.IsVisible = false;
        
        elem.AddChild(outlineRectElem);
        elem.AddChild(glowRectElem);

        elem.AddActionOnUIEvent(UIEvent.BeforeDraw, (e) =>
        {
            if (e is DrawUIEventParams drawParams)
            {
                lastGameTime = drawParams.GameTime;
            }
        });

        elem.AddActionOnUIEvent(UIEvent.BeforeUpdate, (e) =>
        {
            outlineRectElem.OffsetAndSize = new Rectangle(Point.Zero, elem.OffsetAndSize.Size);
            glowRectElem.OffsetAndSize = new Rectangle(Point.Zero, elem.OffsetAndSize.Size);
        });

        elem.AddActionOnUIEvent(UIEvent.Update, (e) =>
        {
            if (elem.DerivedAncestorIsSelected && elem.DerivedAncestorIsVisible && lastGameTime != null)
            {
                outlineRectElem.IsVisible = true;
                glowRectElem.IsVisible = true;
            } else
            {
                outlineRectElem.IsVisible = false;
                glowRectElem.IsVisible = false;
            }
        });

        elem.AddActionOnUIEvent(UIEvent.Draw, (e) =>
        {
            int animationFrame = (int)lastGameTime.TotalGameTime.TotalMicroseconds / 9000 % 100;

            var newThickness = animationFrame / 8 + 2;

            //The 150 used to be ~500 but I wanted it brighter
            var newColor = Color.White * ((99 - animationFrame) / 200.0f);

            Console.WriteLine("Color is " + newColor);

            glowRectElem.Attributes["outline_thickness"] = newThickness;
            glowRectElem.Attributes["outline_color"] = newColor;
        });

        return elem;
    }
}
