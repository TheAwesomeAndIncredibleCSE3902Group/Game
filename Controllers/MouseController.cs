using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.Controllers;

public class MouseController : IController
{
    private readonly Game _gameObject;
    private Action<int> SetChosenSprite;
    private bool _prevTickLeftMouse = false;
    private bool _prevTickRightMouse = false;
    public MouseController(Game game, Action<int> setChosenSpriteAction)
    {
        _gameObject = game;
        SetChosenSprite = setChosenSpriteAction;
    }
    public void Update()
    {
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed && !_prevTickLeftMouse)
        {
            _prevTickLeftMouse = true;
            bool isOnLeft = mouseState.X < _gameObject.Window.ClientBounds.Width / 2;
            bool isOnTop = mouseState.Y < _gameObject.Window.ClientBounds.Height / 2;
            if (isOnTop)
            {
                if (isOnLeft)
                {
                    // top left
                    SetChosenSprite(0);
                }
                else
                {
                    // top right
                    SetChosenSprite(1);
                }
            }
            else
            {
                if (isOnLeft)
                {
                    // bottom left
                    SetChosenSprite(2);
                }
                else
                {
                    // bottom right
                    SetChosenSprite(3);
                }
            }
        }
        if (mouseState.LeftButton == ButtonState.Released && _prevTickLeftMouse)
        {
            _prevTickLeftMouse = false;
        }
        if (mouseState.RightButton == ButtonState.Pressed && !_prevTickRightMouse)
        {
            _prevTickRightMouse = true;
            _gameObject.Exit();
        }
        if (mouseState.RightButton == ButtonState.Released && _prevTickRightMouse)
        {
            _prevTickRightMouse = false;
        }
    }
}
