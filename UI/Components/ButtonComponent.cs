using AwesomeRPG.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Elements;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.UI.Components;

public static class ButtonComponent
{
    public static ElementBase Create(RootElement rootElement, SpriteFont spriteFont, Game1 game, Rectangle location)
    {
        TextElement textElem = new TextElement(rootElement, spriteFont, "test");
        textElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);

        RectElement rectElem = new RectElement(rootElement, Color.LightBlue);
        rectElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
        rectElem.AddChild(textElem);

        textElem.AddActionOnUIEvent(UIEvent.BeforeDraw, (e) =>
        {
            DrawUIEvent thisEvent = (DrawUIEvent)e;
            GameTime eventGameTime = thisEvent.GameTime;
            double seconds = eventGameTime.TotalGameTime.TotalSeconds;
            textElem.TextString = "Time: " + seconds;
        });

        textElem.HorizontalTextAlign = TextElement.TextAlign.Center;
        textElem.VerticalTextAlign = TextElement.TextAlign.Center;

        SelectionAnimationElement selAnimElem = new SelectionAnimationElement(rootElement);
        selAnimElem.OffsetAndSize = location;
        selAnimElem.AddChild(rectElem);

        CommandElement commandElem = new CommandElement(rootElement);
        commandElem.AssociatedCommand = new CommandQuit(game);
        commandElem.AddChild(selAnimElem);
        commandElem.MakeSelectable();

        return commandElem;
    }
}