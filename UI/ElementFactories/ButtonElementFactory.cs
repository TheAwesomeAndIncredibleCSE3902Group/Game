// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.UI.ElementFactories;

public class ButtonElementFactory : IElementFactory
{
    private static readonly Color _selectedBgDim = new Color(220, 220, 220);
    private static readonly Color _clickBgDim = new Color(180, 180, 180);
    private RootElement _rootElement;

    public ButtonElementFactory(RootElement rootElement)
    {
        _rootElement = rootElement;
    }

    public Element CreateNew()
    {
        // Parameterless factory method - use default values
        // This is required by the ElementFactory interface
        return CreateNew(null, null, new Rectangle(0, 0, 100, 50), Color.White, Color.Black);
    }

    public Element CreateNew(SpriteFont spriteFont, Game1 game, Rectangle location, Color bgColor, Color textColor, string textString = "")
    {
        var textFactory = new TextElementFactory(_rootElement);
        var textElem = textFactory.CreateNew(spriteFont, textString, textColor);
        textElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
        textElem.Attributes["horizontal_align"] = TextElementFactory.TextAlign.Center;
        textElem.Attributes["vertical_align"] = TextElementFactory.TextAlign.Center;

        var rectFactory = new RectElementFactory(_rootElement);
        var rectElem = rectFactory.CreateNew(bgColor);
        rectElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
        rectElem.AddChild(textElem);

        var selAnimFactory = new SelectionAnimationElementFactory(_rootElement);
        var selAnimElem = selAnimFactory.CreateNew();
        selAnimElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
        selAnimElem.AddChild(rectElem);

        var elem = new Element(_rootElement);
        elem.AddChild(selAnimElem);
        elem.MakeSelectable();

        elem.AddActionOnUIEvent(UIEvent.Select, (e) =>
        {
            rectElem.Attributes["fill_color"] = bgColor * _selectedBgDim;
        });
        elem.AddActionOnUIEvent(UIEvent.Unselect, (e) =>
        {
            rectElem.Attributes["fill_color"] = bgColor;
        });

        _rootElement.AddActionOnUIEvent(UIEvent.ButtonDown, (e) =>
        {
            InputUIEventParams inputEventParams = (InputUIEventParams)e;
            if (elem.IsSelected && inputEventParams.Controls.Contains(UIControl.Interact))
                rectElem.Attributes["fill_color"] = bgColor * _clickBgDim;
        });

        _rootElement.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams inputEventParams = (InputUIEventParams)e;
            if (elem.IsSelected && inputEventParams.Controls.Contains(UIControl.Interact))
                rectElem.Attributes["fill_color"] = bgColor;
        });

        //Allows convenient access to buttton text
        elem.Attributes["text_element"] = textElem;
        elem.OffsetAndSize = location;

        return elem;
    }
}
