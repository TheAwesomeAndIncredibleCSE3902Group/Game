// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Elements;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.UI.Components;

public class ButtonComponent : ComponentBase
{
    private readonly Color _selectedBgDim = new Color(220, 220, 220);
    private readonly Color _clickBgDim = new Color(180,180,180);
    public ICommand AssociatedCommand {
        get {
            return ((CommandElement)ComponentBaseElement).AssociatedCommand;
        }
        set {
            ((CommandElement)ComponentBaseElement).AssociatedCommand = value;
        }
    }
    public ButtonComponent(RootElement rootElement, SpriteFont spriteFont, Game1 game, Rectangle location, Color bgColor, Color textColor, string textString = "")
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
            rectElem.FillColor = bgColor * _selectedBgDim;
        });
        commandElem.AddActionOnUIEvent(UIEvent.Unselect, (e) =>
        {
            rectElem.FillColor = bgColor;
        });

        rootElement.AddActionOnUIEvent(UIEvent.ButtonDown, (e) =>
        {
            InputUIEventParams inputEventParams = (InputUIEventParams) e;
            if (commandElem.IsSelected && inputEventParams.Controls.Contains(UIControl.Interact))
                rectElem.FillColor = bgColor * _clickBgDim;
        });

        rootElement.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams inputEventParams = (InputUIEventParams) e;
            if (commandElem.IsSelected && inputEventParams.Controls.Contains(UIControl.Interact))
                rectElem.FillColor = bgColor;
        });

        ComponentBaseElement = commandElem;
    }
    protected internal override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        System.Console.WriteLine("DRAWING BUTTON");
        System.Console.WriteLine(OffsetAndSize);
    }
}