// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI.ElementFactories;

public class TextElementFactory : IElementFactory
{
    public enum TextAlign { Left, Center, Right };
    private RootElement RootElem { get; set; }

    public TextElementFactory(RootElement rootElement)
    {
        RootElem = rootElement;
    }

    public Element CreateNew()
    {
        // Parameterless factory method - override with specific parameters
        return CreateNew(null, "");
    }

    public Element CreateNew(SpriteFont spriteFont, string textString = "", Color? textColor = null, TextAlign horizontalAlign = TextAlign.Left, TextAlign verticalAlign = TextAlign.Left)
    {
        var elem = new Element(RootElem);

        Color color = textColor ?? new Color(0, 0, 0, 255);

        elem.Attributes["text_color"] = color;
        elem.Attributes["sprite_font"] = spriteFont;
        elem.Attributes["text_string"] = textString;
        elem.Attributes["horizontal_align"] = horizontalAlign;
        elem.Attributes["vertical_align"] = verticalAlign;

        elem.AddActionOnUIEvent(UIEvent.Draw, (e) =>
        {
            if (elem.DerivedAncestorIsVisible)
            {
                Vector2 textCalculatedPosition = elem.DerivedAbsolutePosition.ToVector2();
                Color color = (Color)elem.Attributes["text_color"];
                SpriteFont font = (SpriteFont)elem.Attributes["sprite_font"];
                string text = (string)elem.Attributes["text_string"];
                TextAlign hAlign = (TextAlign)elem.Attributes["horizontal_align"];
                TextAlign vAlign = (TextAlign)elem.Attributes["vertical_align"];

                Vector2 measuredText = font.MeasureString(text);

                if (hAlign == TextAlign.Center)
                {
                    textCalculatedPosition.X += (elem.OffsetAndSize.Width - measuredText.X) / 2;
                }
                else if (hAlign == TextAlign.Right)
                {
                    textCalculatedPosition.X += elem.OffsetAndSize.Width - measuredText.X;
                }

                if (vAlign == TextAlign.Center)
                {
                    textCalculatedPosition.Y += (elem.OffsetAndSize.Height - measuredText.Y) / 2;
                }
                else if (vAlign == TextAlign.Right)
                {
                    textCalculatedPosition.Y += elem.OffsetAndSize.Height - measuredText.Y;
                }

                elem.RootElement.SpriteBatch.DrawString(font, text, textCalculatedPosition, color * elem.Opacity);
            }
        });

        return elem;
    }
}
