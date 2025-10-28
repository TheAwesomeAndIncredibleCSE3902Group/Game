using System;
using System.Collections.Generic;
using AwesomeRPG.Commands;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AwesomeRPG.Controllers;

public class MouseController : IController
{
    private readonly Game1 _gameObject;
    private Action<int> SetChosenSprite;
    private bool _prevTickLeftMouse = false;
    private bool _prevTickRightMouse = false;

    // Room Commands
    private ICommand leftRoom;
    private ICommand rightRoom;
    private ICommand topRoom;
    private ICommand bottomRoom;
    private List<ICommand> commands = new();
    public MouseController(Game1 game, RoomAtlas atlas)
    {
        _gameObject = game;
        InitializeCommands(game, atlas);
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
                    // Top Left changes to the room on the right (first value increases)
                    commands[1].Execute();
                    
                }
                else
                {
                    // Top Right changes to the room on the bottom (second value increases)
                    commands[3].Execute();
                }
            }
            else
            {
                if (isOnLeft)
                {
                    // Bottom Left changes to the room on the left (first value decreases)
                    commands[0].Execute();
                }
                else
                {
                    // Bottom Right changes to the room on the top (second value decreases)
                    commands[2].Execute();
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

    public void InitializeCommands(Game1 game, RoomAtlas atlas)
    {
        leftRoom = new ChangeCurrentRoomCommand(game, atlas, Util.Cardinal.left);
        rightRoom = new ChangeCurrentRoomCommand(game, atlas, Util.Cardinal.right);
        topRoom = new ChangeCurrentRoomCommand(game, atlas, Util.Cardinal.up);
        bottomRoom = new ChangeCurrentRoomCommand(game, atlas, Util.Cardinal.down);
        //commands = new List<ICommand>();
        commands.Add(leftRoom);
        commands.Add(rightRoom);
        commands.Add(topRoom);
        commands.Add(bottomRoom);
    }
}
