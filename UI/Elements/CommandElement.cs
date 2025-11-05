// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the element that holds commands.

using AwesomeRPG.Commands;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

public class CommandElement : ElementBase
{
    public ICommand AssociatedCommand { get; set; }

    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();

        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(this, gameTime));
        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(this, gameTime));
    }

    public CommandElement(RootElement rootElement)
    {
        SetUpElement(rootElement);
    }
    
    public CommandElement(RootElement rootElement, ICommand command)
    {
        SetUpElement(rootElement);
        AssociatedCommand = command;
    }
}
