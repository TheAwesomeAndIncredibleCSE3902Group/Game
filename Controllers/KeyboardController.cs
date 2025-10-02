using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;

namespace Sprint0.Controllers
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> keyPressMappings;
        private Dictionary<Keys, ICommand> keyDownMappings;
        private KeyboardState _previousState;
        public KeyboardController(Game1 game) 
        {
            keyPressMappings = new Dictionary<Keys, ICommand>();
            keyDownMappings = new Dictionary<Keys, ICommand>();
            _previousState = Keyboard.GetState();
            InitializeCommands(game);
        }

        //Update all keyboard input
        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            HandleKeyDowns(currentState);
            HandleKeyPresses(currentState);
        }

        //Checks keys in keyPressMappings to see if it just got pressed if so executes command
        private void HandleKeyPresses(KeyboardState currentState)
        {
            foreach (Keys key in currentState.GetPressedKeys())
            {
                // Only trigger if it wasn't down before
                if (keyPressMappings.ContainsKey(key) && _previousState.IsKeyUp(key))
                {
                    keyPressMappings[key].Execute();
                }
            }
            _previousState = currentState; // save for next frame
        }

        //Checks keys in keyDownMappings to see if it's pressed if so executes command
        private void HandleKeyDowns(KeyboardState currentState)
        {
            foreach (Keys key in currentState.GetPressedKeys())
            {
                // Only trigger if it wasn't down before
                if (keyDownMappings.ContainsKey(key))
                {
                    keyDownMappings[key].Execute();
                }
            }
        }

        //Links all keyboard commands into their keys
        private void InitializeCommands(Game1 game)
        {
            //Key Presses
            //Quitting
            keyPressMappings[Keys.Q] = new CommandQuit(game);
            //Switching Overworld Item Sprite
            keyPressMappings[Keys.U] = new CommandSwitchOverworldItemSprite(game,CommandSwitchOverworldItemSprite.Direction.left);
            keyPressMappings[Keys.I] = new CommandSwitchOverworldItemSprite(game, CommandSwitchOverworldItemSprite.Direction.right);
            
            //Key Downs
            //Movement
            ICommand moveLeft = new CommandMovePlayer(game, Util.Cardinal.left);
            ICommand moveRight = new CommandMovePlayer(game, Util.Cardinal.right);
            ICommand moveUp = new CommandMovePlayer(game, Util.Cardinal.up);
            ICommand moveDown = new CommandMovePlayer(game, Util.Cardinal.down);
            keyDownMappings[Keys.Left] = moveLeft;
            keyDownMappings[Keys.A] = moveLeft;
            keyDownMappings[Keys.Right] = moveRight;
            keyDownMappings[Keys.D] = moveRight;
            keyDownMappings[Keys.Up] = moveUp;
            keyDownMappings[Keys.W] = moveUp;
            keyDownMappings[Keys.Down] = moveDown;
            keyDownMappings[Keys.S] = moveDown;

        }
    }
}
