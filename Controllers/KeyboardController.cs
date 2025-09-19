using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint0.Sprites;

namespace Sprint0.Controllers;

public class KeyboardController : IController
{
    private readonly HashSet<Keys> _previousTickKeysDown = [];
    private readonly HashSet<Keys> _keysTapped = [];
    private readonly Game _gameObject;
    private Action<int> SetChosenSprite;
    public KeyboardController(Game game, Action<int> setChosenSpriteAction)
    {
        _gameObject = game;
        SetChosenSprite = setChosenSpriteAction;
    }
    public void Update()
    {
        KeyboardState keyState = Keyboard.GetState();
        Keys[] currentlyPressedKeys = keyState.GetPressedKeys();
        _keysTapped.Clear();
        foreach (Keys currentKey in currentlyPressedKeys)
        {
            if (!_previousTickKeysDown.Contains(currentKey))
            {
                _keysTapped.Add(currentKey);
            }
        }

        foreach (Keys key in _keysTapped) {
            switch (key)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    _gameObject.Exit();
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    SetChosenSprite(0);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    SetChosenSprite(1);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    SetChosenSprite(2);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    SetChosenSprite(3);
                    break;
            }
        }

        _previousTickKeysDown.Clear();
        _previousTickKeysDown.UnionWith(currentlyPressedKeys);
    }
}