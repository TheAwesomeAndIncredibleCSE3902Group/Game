// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI.ElementFactories;

public class TextTyperElementFactory : IElementFactory
{
    private RootElement RootElem { get; set; }

    public TextTyperElementFactory(RootElement rootElement)
    {
        RootElem = rootElement;
    }

    public Element CreateNew()
    {
        // Parameterless factory method - override with specific parameters
        return CreateNew(null, "");
    }

    public Element CreateNew(SpriteFont spriteFont, string textString = "", Color textColor = default, int charDelayMs = 16)
    {
        var elem = new Element(RootElem);

        Color color = textColor == default ? new Color(0, 0, 0, 255) : textColor;

        elem.Attributes["text_color"] = color;
        elem.Attributes["sprite_font"] = spriteFont;
        elem.Attributes["text_string"] = textString;
        elem.Attributes["char_delay_ms"] = charDelayMs;
        elem.Attributes["currently_drawn_char"] = 0;
        elem.Attributes["started_typing_time"] = null;

        elem.AddActionOnUIEvent(UIEvent.Update, (eventParams) =>
        {
            DrawUIEventParams drawParams = (DrawUIEventParams)eventParams;
            GameTime gameTime = drawParams.GameTime;
            object startedTimeObj = elem.Attributes["started_typing_time"];
            GameTime startedTime = startedTimeObj as GameTime ?? gameTime;

            if (startedTimeObj == null)
            {
                elem.Attributes["started_typing_time"] = gameTime;
            }

            // Calculate how many characters should be drawn
            int charDelay = (int)elem.Attributes["char_delay_ms"];
            int elapsedMs = (int)gameTime.TotalGameTime.TotalMilliseconds - (int)startedTime.TotalGameTime.TotalMilliseconds;
            int currentlyDrawnChar = Math.Min(elapsedMs / charDelay, textString.Length);

            elem.Attributes["currently_drawn_char"] = currentlyDrawnChar;
        });

        elem.AddActionOnUIEvent(UIEvent.Draw, (e) =>
        {
            if (elem.DerivedAncestorIsVisible)
            {
                Vector2 textCalculatedPosition = elem.DerivedAbsolutePosition.ToVector2();
                Color color = (Color)elem.Attributes["text_color"];
                SpriteFont font = (SpriteFont)elem.Attributes["sprite_font"];
                string text = (string)elem.Attributes["text_string"];
                int currentlyDrawnChar = (int)elem.Attributes["currently_drawn_char"];

                if (font != null && text.Length > 0)
                {
                    string displayText = text.Substring(0, Math.Min(currentlyDrawnChar, text.Length));
                    RootElem.SpriteBatch.DrawString(font, displayText, textCalculatedPosition, color);
                }
            }
        });

        return elem;
    }
}