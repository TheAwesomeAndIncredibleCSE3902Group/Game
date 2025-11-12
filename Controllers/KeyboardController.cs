using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using AwesomeRPG.Commands;

namespace AwesomeRPG.Controllers
{
    public class KeyboardController : IController
    {
        //Simply change these to switch between Overworld and BattleState
        private Dictionary<Keys, ICommand> overworldKeyPressMappings;
        private Dictionary<Keys, ICommand> overworldKeyDownMappings;

        private Dictionary<Keys, ICommand> battleKeyPressMappings;
        private Dictionary<Keys, ICommand> battleKeyDownMappings;        

        private KeyboardState _previousState;
        public KeyboardController(Game1 game)
        {
            overworldKeyPressMappings = new Dictionary<Keys, ICommand>();
            overworldKeyDownMappings = new Dictionary<Keys, ICommand>();

            battleKeyPressMappings = new Dictionary<Keys, ICommand>();
            battleKeyDownMappings = new Dictionary<Keys, ICommand>();
            
            _previousState = Keyboard.GetState();
            InitializeCommands(game);
        }

        //Update all keyboard input
        public void Update(GameState gameState)
        {
            KeyboardState currentState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.overworld:
                    HandleKeyDowns(currentState, overworldKeyDownMappings);
                    HandleKeyPresses(currentState, overworldKeyPressMappings);
                    break;
                case GameState.battle:
                    HandleKeyDowns(currentState, battleKeyDownMappings);
                    HandleKeyPresses(currentState, battleKeyPressMappings);
                    break;
            }

        }

        /// <summary>
        /// Run this when ie switching game states
        /// Returns the controller to a base state, where no keys were pressed in the previous frame
        /// </summary>
        public void Flush()
        {
            _previousState = new KeyboardState();
        }

        //Checks keys in keyPressMappings to see if it just got pressed if so executes command
        private void HandleKeyPresses(KeyboardState currentState, Dictionary<Keys, ICommand> keyPressMapping)
        {
            foreach (Keys key in currentState.GetPressedKeys())
            {
                // Only trigger if it wasn't down before
                if (keyPressMapping.ContainsKey(key) && _previousState.IsKeyUp(key))
                {
                    keyPressMapping[key].Execute();
                }
            }
            _previousState = currentState; // save for next frame
        }

        //Checks keys in keyDownMappings to see if it's pressed if so executes command continously
        private void HandleKeyDowns(KeyboardState currentState, Dictionary<Keys, ICommand> keyDownMapping)
        {
            foreach (Keys key in currentState.GetPressedKeys())
            {
                // Only trigger if it wasn't down before
                if (keyDownMapping.ContainsKey(key))
                {
                    keyDownMapping[key].Execute();
                }
            }
        }

        //Links all keyboard commands into their keys
        private void InitializeCommands(Game1 game)
        {
            InitializeGameCommands(game);
            InitializeWeaponCommands(game);
            InitializeMovementCommands();
        }
        //Initialize commands which effect the application as a whole
        private void InitializeGameCommands(Game1 game)
        {
            overworldKeyPressMappings[Keys.Q] = new QuitCommand(game);
            overworldKeyPressMappings[Keys.R] = new ResetGameCommand(game);
            overworldKeyPressMappings[Keys.E] = new DamagePlayerCommand();
        }
        //Initialize commands which relate to weapons and item use
        private void InitializeWeaponCommands(Game1 game)
        {
            overworldKeyPressMappings[Keys.D1] = new UseItemCommand(IEquipment.Weapons.bow);
            overworldKeyPressMappings[Keys.D2] = new UseItemCommand(IEquipment.Weapons.boomerangSack);
            overworldKeyPressMappings[Keys.D3] = new UseItemCommand(IEquipment.Weapons.superSwordSheathe);
            ICommand swordUse = new UseItemCommand(IEquipment.Weapons.swordSheathe);
            overworldKeyPressMappings[Keys.Z] = swordUse;
            overworldKeyPressMappings[Keys.N] = swordUse;
        }
        //Initialize commands which relate to movement
        private void InitializeMovementCommands()
        {
            ICommand moveLeft = new MovePlayerCommand(Util.Cardinal.left);
            ICommand moveRight = new MovePlayerCommand(Util.Cardinal.right);
            ICommand moveUp = new MovePlayerCommand(Util.Cardinal.up);
            ICommand moveDown = new MovePlayerCommand(Util.Cardinal.down);
            overworldKeyDownMappings[Keys.Left] = moveLeft;
            overworldKeyDownMappings[Keys.A] = moveLeft;
            overworldKeyDownMappings[Keys.Right] = moveRight;
            overworldKeyDownMappings[Keys.D] = moveRight;
            overworldKeyDownMappings[Keys.Up] = moveUp;
            overworldKeyDownMappings[Keys.W] = moveUp;
            overworldKeyDownMappings[Keys.Down] = moveDown;
            overworldKeyDownMappings[Keys.S] = moveDown;
        }
    }
}