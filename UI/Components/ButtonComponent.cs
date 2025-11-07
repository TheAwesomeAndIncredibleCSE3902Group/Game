// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Elements;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.UI.Components;

public static class ButtonComponent
{
    private static readonly Color _selectedBgDim = new Color(220, 220, 220);
    public static ElementBase Create(RootElement rootElement, SpriteFont spriteFont, Game1 game, Rectangle location, Color bgColor, Color textColor, string textString = "")
    {
        TextElement textElem = new TextElement(rootElement, spriteFont, textString, textColor);
        textElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);

        RectElement rectElem = new RectElement(rootElement, bgColor);
        rectElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
        rectElem.AddChild(textElem);

        textElem.HorizontalTextAlign = TextElement.TextAlign.Center;
        textElem.VerticalTextAlign = TextElement.TextAlign.Center;

        SelectionAnimationElement selAnimElem = new SelectionAnimationElement(rootElement);
        selAnimElem.OffsetAndSize = location;
        selAnimElem.AddChild(rectElem);

        CommandElement commandElem = new CommandElement(rootElement);
        commandElem.AddChild(selAnimElem);
        commandElem.MakeSelectable();

        commandElem.AddActionOnUIEvent(UIEvent.Select, (e) =>
        {
            System.Console.WriteLine("Selected!!!!");
            rectElem.FillColor = bgColor * _selectedBgDim;
        });
        commandElem.AddActionOnUIEvent(UIEvent.Unselect, (e) =>
        {
            System.Console.WriteLine("Unselected!!!!");
            rectElem.FillColor = bgColor;
        });

        return commandElem;
    }
}