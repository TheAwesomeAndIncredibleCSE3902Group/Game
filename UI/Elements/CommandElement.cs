// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the element that holds commands.

using System;
using AwesomeRPG.Commands;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

public class CommandElement : ElementBase
{
    public ICommand AssociatedCommand { get; set; }

    public CommandElement(RootElement rootElement)
    {
        SetUpElement(rootElement);
        SetUpCommandInput();
    }

    public CommandElement(RootElement rootElement, ICommand command)
    {
        SetUpElement(rootElement);
        SetUpCommandInput();
        AssociatedCommand = command;
    }
    private void SetUpCommandInput()
    {
        RootElement.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams inputEventParams = (InputUIEventParams) e;
            if (inputEventParams.Controls.Contains(UIControl.Interact) && IsSelected)
            {
                if (AssociatedCommand == null)
                {
                    Console.WriteLine("WARNING: The clicked CommandElement has no associated Command!");
                } else
                {
                    AssociatedCommand.Execute();
                }
            }
        });
    }
    protected internal override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
