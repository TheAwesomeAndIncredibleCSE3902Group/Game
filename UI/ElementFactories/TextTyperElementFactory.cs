// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI.ElementFactories;

public class TextTyperElementFactory : IElementFactory
{
    private const long TICKS_IN_ONE_MILLISECOND = 10000;
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

        // System.Console.WriteLine("Creating new text typer element!");

        Color color = textColor == default ? new Color(0, 0, 0, 255) : textColor;

        elem.Attributes["text_color"] = color;
        elem.Attributes["sprite_font"] = spriteFont;
        elem.Attributes["text_string"] = textString;
        elem.Attributes["char_delay_ms"] = charDelayMs;
        elem.Attributes["currently_drawn_char"] = 0;
        elem.Attributes["started_typing_time"] = null;

        elem.AddActionOnUIEvent(UIEvent.Update, (eventParams) =>
        {
            // System.Console.WriteLine("before everything " + elem.Attributes["started_typing_time"]);
            DrawUIEventParams drawParams = (DrawUIEventParams)eventParams;
            GameTime gameTime = drawParams.GameTime;
            if (elem.Attributes["started_typing_time"] == null)
            {
                elem.Attributes["started_typing_time"] = gameTime.TotalGameTime.Ticks;
            }
            long startedTimeTick = (long) elem.Attributes["started_typing_time"];
            long currentTimeTick = gameTime.TotalGameTime.Ticks;

            // Calculate how many characters should be drawn
            int charDelay = (int)elem.Attributes["char_delay_ms"];
            int elapsedMs = (int)((currentTimeTick - startedTimeTick) / TICKS_IN_ONE_MILLISECOND);
            int currentlyDrawnChar = Math.Min(elapsedMs / charDelay, textString.Length);

            elem.Attributes["currently_drawn_char"] = currentlyDrawnChar;

            // System.Console.WriteLine("freaking drawing text typer shit! " + currentlyDrawnChar);
            // System.Console.WriteLine("Elapsed ms " + elapsedMs);
            // System.Console.WriteLine("Start time tick " + startedTimeTick);
            // System.Console.WriteLine("Current time tick " + currentTimeTick);
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