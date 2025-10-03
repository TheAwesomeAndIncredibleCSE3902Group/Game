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

        //TODO: change as we migrate to state-by-class
        private ICommand playerToStandingCommand;

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

            //TODO: change as we migrate to state-by-class
            HandleStanding(currentState);
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

        /// <summary>
        /// Very quick and dirty way to enter standing state when no movement is input
        /// TODO: change as we migrate to state-by-class
        /// </summary>
        /// <param name="currentState"></param>
        private void HandleStanding(KeyboardState currentState)
        {
            //If any movement key is pressed just return
            if (currentState.IsKeyDown(Keys.W)
                || currentState.IsKeyDown(Keys.A)
                || currentState.IsKeyDown(Keys.S)
                || currentState.IsKeyDown(Keys.D)
                || currentState.IsKeyDown(Keys.Up)
                || currentState.IsKeyDown(Keys.Right)
                || currentState.IsKeyDown(Keys.Down)
                || currentState.IsKeyDown(Keys.Left))
            {
                return;
            }
            //Otherwise transition to standing
            else
                playerToStandingCommand.Execute();
        }

        //Links all keyboard commands into their keys
        private void InitializeCommands(Game1 game)
        {
            //Key Presses
            //Quitting
            keyPressMappings[Keys.Q] = new CommandQuit(game);
            //Switching Overworld Item Sprite
            keyPressMappings[Keys.U] = new CommandSwitchOverworldItemSprite(game, CommandSwitchOverworldItemSprite.Direction.left);
            keyPressMappings[Keys.I] = new CommandSwitchOverworldItemSprite(game, CommandSwitchOverworldItemSprite.Direction.right);
            //Use Equipment
            keyPressMappings[Keys.D1] = new CommandUseItem(game);
            //Switching Enemy
            keyPressMappings[Keys.O] = new CommandSwitchEnemySprite(game, false);
            keyPressMappings[Keys.P] = new CommandSwitchEnemySprite(game, true);

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

            //TODO: change as we migrate to state-by-class
            playerToStandingCommand = new CommandPlayerToStanding(game);
        }
    }
}