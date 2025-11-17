// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Elements;
using System;
namespace AwesomeRPG.UI.Components;

public class BattleUITextTyperComponent : ComponentBase
{
    public int CurrentlyDrawnTextChar { get; private set; } = 0;
    private string _stringToType;
    public bool CanAdvanceText;
    private GameTime _startedTypingGameTime = null;
    private TextElement _textElem;

    public BattleUITextTyperComponent(RootElement rootElement, SpriteFont spriteFont, Game1 game, Rectangle location, Color textColor, string textString = "No text string provided")
    {
        _stringToType = textString;

        _textElem = new TextElement(rootElement, spriteFont, _stringToType, textColor);
        _textElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
        CommandElement commandElem = new(rootElement);
        commandElem.AddChild(_textElem);

        ComponentBaseElement = commandElem;
    }

    protected internal override void Update(GameTime gameTime)
    {
        if (_startedTypingGameTime == null)
        {
            _startedTypingGameTime = gameTime;
        }

        // We will draw new characters here!
        CurrentlyDrawnTextChar = Math.Min(((int) gameTime.TotalGameTime.TotalMilliseconds - (int)_startedTypingGameTime.TotalGameTime.TotalMilliseconds) / 16, _stringToType.Length);
        _textElem.TextString = _stringToType.Substring(0, CurrentlyDrawnTextChar);

        base.Update(gameTime);

        Console.WriteLine(CurrentlyDrawnTextChar);
        System.Console.WriteLine(OffsetAndSize);
    }
}