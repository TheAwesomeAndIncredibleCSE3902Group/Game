// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the element that holds commands.

using System;
using AwesomeRPG.Commands;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.UI.ElementFactories;

public class CommandElementFactory : IElementFactory
{
    private RootElement RootElem { get; set; }

    public CommandElementFactory(RootElement rootElement)
    {
        RootElem = rootElement;
    }

    public Element CreateNew()
    {
        var elem = new Element(RootElem);

        elem.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams inputEventParams = (InputUIEventParams)e;
            if (inputEventParams.Controls.Contains(UIControl.Interact) && elem.IsSelected)
            {
                if (elem.Attributes.TryGetValue("associated_command", out var commandObj) && commandObj is ICommand command)
                {
                    command.Execute();
                }
                else
                {
                    Console.WriteLine("WARNING: The clicked CommandElement has no associated Command!");
                }
            }
        });

        return elem;
    }

    public Element CreateNew(ICommand command)
    {
        var elem = CreateNew();
        elem.Attributes["associated_command"] = command;
        return elem;
    }
}
