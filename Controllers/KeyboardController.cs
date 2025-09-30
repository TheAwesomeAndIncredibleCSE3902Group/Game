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
        private Dictionary<Keys, ICommand> controllerMappings;
        private KeyboardState _previousState;
        public KeyboardController(Game1 game) 
        {
            controllerMappings = new Dictionary<Keys, ICommand>();
            _previousState = Keyboard.GetState();
            InitializeCommands(game);
        }
        public void RegisterCommand(Keys key, ICommand command)
        {
            controllerMappings.Add(key, command);
        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();

            foreach (Keys key in currentState.GetPressedKeys())
            {
                // Only trigger if it wasn't down before
                if (controllerMappings.ContainsKey(key) && _previousState.IsKeyUp(key) )
                {
                    controllerMappings[key].Execute();
                }
            }

            _previousState = currentState; // save for next frame
        }

        private void InitializeCommands(Game1 game)
        {
            controllerMappings[Keys.Q] = new CommandQuit(game);
            controllerMappings[Keys.U] = new CommandSwitchOverworldItemSprite(game,CommandSwitchOverworldItemSprite.Direction.left);
            controllerMappings[Keys.I] = new CommandSwitchOverworldItemSprite(game, CommandSwitchOverworldItemSprite.Direction.right);
        }
    }
}
