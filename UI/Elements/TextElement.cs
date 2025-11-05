// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI.Elements;

public class TextElement : ElementBase
{
    public Color TextColor { get; set; } = new Color(0, 0, 0, 255);
    public SpriteFont SpriteFont { get; set; }
    public string TextString { get; set; } = "";
    public enum TextAlign {Left, Center, Right};
    public TextAlign HorizontalTextAlign = TextAlign.Left;
    public TextAlign VerticalTextAlign = TextAlign.Left;

    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();

        Vector2 textCalculatedPosition = DerivedAbsolutePosition.ToVector2();
        Vector2 measuredText = SpriteFont.MeasureString(TextString);

        if (HorizontalTextAlign == TextAlign.Center)
        {
            textCalculatedPosition.X += (OffsetAndSize.Width - measuredText.X) / 2;
        }
        else if (HorizontalTextAlign == TextAlign.Right)
        {
            textCalculatedPosition.X += OffsetAndSize.Width - measuredText.X;
        }

        if (VerticalTextAlign == TextAlign.Center)
        {
            textCalculatedPosition.Y += (OffsetAndSize.Height - measuredText.Y) / 2;
        }
        else if (VerticalTextAlign == TextAlign.Right)
        {
            textCalculatedPosition.Y += OffsetAndSize.Height - measuredText.Y;
        }

        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(this, gameTime));
        RootElement.SpriteBatch.DrawString(SpriteFont, TextString, textCalculatedPosition, TextColor);

        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(this, gameTime));
    }

    public TextElement(RootElement rootElement, SpriteFont spriteFont)
    {
        SetUpElement(rootElement);
        SpriteFont = spriteFont;
    }

    public TextElement(RootElement rootElement, SpriteFont spriteFont, String textString)
    {
        SetUpElement(rootElement);
        SpriteFont = spriteFont;
        TextString = textString;
    }

    public TextElement(RootElement rootElement, SpriteFont spriteFont, String textString, Color textColor)
    {
        SetUpElement(rootElement);
        SpriteFont = spriteFont;
        TextString = textString;
        TextColor = textColor;
    }
}
