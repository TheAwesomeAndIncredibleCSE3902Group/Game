// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.UI.Elements;
using System;
namespace AwesomeRPG.UI.Components;

public static class BattleUITextTyperComponent
{
    public static CommandElement CreatedElement { get; private set; }
    public static int CurrentlyDrawnTextChar { get; private set; }
    private static string _stringToType;
    public static bool CanAdvanceText;
    private static GameTime _startedTypingGameTime;
    private static TextElement _textElem;
    public static bool IsSetup { get; private set; }

    public static CommandElement NewTyper(RootElement rootElement, SpriteFont spriteFont, Game1 game, Rectangle location, Color textColor, string textString = "No text string provided")
    {
        CurrentlyDrawnTextChar = 0;
        _stringToType = textString;
        _startedTypingGameTime = null;

        if (CreatedElement == null)
        {
            _textElem = new TextElement(rootElement, spriteFont, _stringToType, textColor);
            _textElem.OffsetAndSize = new Rectangle(Point.Zero, location.Size);
            CommandElement commandElem = new(rootElement);
            commandElem.AddChild(_textElem);
            CreatedElement = commandElem;
        }
        IsSetup = true;

        return CreatedElement;
    }

    public static void UnSetup()
    {
        IsSetup = false;
        _startedTypingGameTime = null;
    }

    public static void Update(GameTime gameTime)
    {
        if (IsSetup)
        {
            if (_startedTypingGameTime == null)
            {
                _startedTypingGameTime = gameTime;
            }

            // We will draw new characters here!
            CurrentlyDrawnTextChar = Math.Min(((int) gameTime.TotalGameTime.TotalMilliseconds - (int)_startedTypingGameTime.TotalGameTime.TotalMilliseconds) / 16, _stringToType.Length);
            _textElem.TextString = _stringToType.Substring(0, CurrentlyDrawnTextChar);

            Console.WriteLine(CurrentlyDrawnTextChar);
        }
    }
}