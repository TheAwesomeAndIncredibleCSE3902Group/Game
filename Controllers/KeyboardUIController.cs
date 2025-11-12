using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using AwesomeRPG.Commands;
using AwesomeRPG.UI;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.Controllers;

public class KeyboardUIController : IController
{
    private Game1 _myGame1;
    private readonly Dictionary<Keys, UIControl> _keyMappings = new() 
    {
        [Keys.Left] = UIControl.MoveLeft,
        [Keys.Right] = UIControl.MoveRight,
        [Keys.Up] = UIControl.MoveUp,
        [Keys.Down] = UIControl.MoveDown,
        [Keys.Enter] = UIControl.Interact,
        [Keys.Back] = UIControl.Return,
    };

    private KeyboardState _previousState;

    //Update all keyboard input for UI
    public void Update(GameState gameState)
    {
        KeyboardState currentState = Keyboard.GetState();
        List<UIControl> uiControlsDown = [];
        List<UIControl> uiControlsUp = []; 
        List<UIControl> uiControlsPress = [];


        foreach (KeyValuePair<Keys, UIControl> keyMapping in _keyMappings)
        {
            if (currentState.IsKeyDown(keyMapping.Key))
            {
                uiControlsPress.Add(keyMapping.Value);
                // System.Console.WriteLine("keypress" + keyMapping.Value.ToString());
            }
            if (currentState.IsKeyDown(keyMapping.Key) && _previousState.IsKeyUp(keyMapping.Key))
            {
                uiControlsDown.Add(keyMapping.Value);
                // System.Console.WriteLine("keydown" + keyMapping.Value.ToString());
            }
            if (currentState.IsKeyUp(keyMapping.Key) && _previousState.IsKeyDown(keyMapping.Key))
            {
                uiControlsUp.Add(keyMapping.Value);
                // System.Console.WriteLine("keyup" + keyMapping.Value.ToString());
            }
        }

        if (uiControlsDown.Count > 0)
        {
            InputUIEventParams downUIEventParams = new(_myGame1.RootUIElement, uiControlsDown);
            _myGame1.RootUIElement.DispatchUIEvent(UIEvent.ButtonDown, downUIEventParams);
        }
        if (uiControlsUp.Count > 0)
        {
            InputUIEventParams upUIEventParams = new(_myGame1.RootUIElement, uiControlsUp);
            _myGame1.RootUIElement.DispatchUIEvent(UIEvent.ButtonUp, upUIEventParams);
        }
        if (uiControlsPress.Count > 0)
        {
            InputUIEventParams pressUIEventParams = new(_myGame1.RootUIElement, uiControlsPress);
            _myGame1.RootUIElement.DispatchUIEvent(UIEvent.ButtonPress, pressUIEventParams);
        }

        _previousState = currentState;
    }

    public KeyboardUIController(Game1 game)
    {
        _previousState = Keyboard.GetState();
        _myGame1 = game;
    }
}