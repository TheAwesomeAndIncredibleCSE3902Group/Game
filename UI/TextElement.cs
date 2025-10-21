using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI;

public class TextElement : ElementBase
{
    public Color TextColor { get; set; } = new Color(0, 0, 0, 255);
    public SpriteFont SpriteFont { get; set; }
    public String TextString { get; set; } = "";
    public override void Draw(GameTime gameTime, ElementBase parent)
    {
        CalculateDerivedValuesFromAncestors(parent);

        RootElement.SpriteBatch.DrawString(SpriteFont, TextString, DerivedAbsolutePosition.ToVector2(), TextColor);

        DrawChildren(gameTime);
    }

    public TextElement(ElementRoot rootElement, SpriteFont spriteFont)
    {
        RootElement = rootElement;
        SpriteFont = spriteFont;
    }

    public TextElement(ElementRoot rootElement, SpriteFont spriteFont, String textString)
    {
        RootElement = rootElement;
        SpriteFont = spriteFont;
        TextString = textString;
    }

    public TextElement(ElementRoot rootElement, SpriteFont spriteFont, String textString, Color textColor)
    {
        RootElement = rootElement;
        SpriteFont = spriteFont;
        TextString = textString;
        TextColor = textColor;
    }
}
