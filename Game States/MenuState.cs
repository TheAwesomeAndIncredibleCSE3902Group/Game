using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.BattleMechanics.BattleEnemies;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using AwesomeRPG.UI;
using AwesomeRPG.UI.ElementFactories;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AwesomeRPG;

/// <summary>
/// Menu state with three main tabs: Party, Inventory, and Options
/// </summary>
public class MenuState : IGameState
{
    public RootElement RootUIElement { get; private set; }
    public GameState CurrentState { get => GameState.overworld; } // Menu is considered part of overworld state
    
    private OverworldState _overworldState;
    private Game1 _game;
    
    private KeyboardState _previousKeyboardState = new KeyboardState();
    
    // Menu containers for each tab
    private Element _partyContainer;
    private Element _inventoryContainer;
    private Element _optionsContainer;
    
    // Current active menu
    private MenuTab _currentTab = MenuTab.Party;
    
    private enum MenuTab
    {
        Party,
        Inventory,
        Options
    }
    
    public MenuState(OverworldState overworldState, Game1 game)
    {
        _overworldState = overworldState;
        _game = game;
    }
    
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Draw the overworld state in the background (paused)
        _overworldState.Draw(spriteBatch, gameTime);
        
        // Initialize UI on first draw
        if (RootUIElement == null)
        {
            InitializeUI(spriteBatch);
        }
        
        // Draw the menu UI
        RootUIElement.Draw(gameTime);
    }
    
    public void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();
        if (keyboard.IsKeyUp(Keys.Escape) && _previousKeyboardState.IsKeyDown(Keys.Escape))
        {
            ChangeToOverworldState();
        }
        _previousKeyboardState = keyboard;

        GameSoundFactory.PlayOverworldMapTheme(gameTime);
        
        RootUIElement?.Update(gameTime);
    }
    
    
    
    public void ChangeToOverworldState()
    {
        // Return to the overworld state
        RootUIElement = null;
        _game.SetStateClass(_overworldState);
    }
    
    public void ChangeToStartState() { }
    public void ChangeToGameOverState() { }
    public void ChangeToWinState() { }
    public void ChangeToBattleState(CharacterEnemyBase enemy, bool playerStarting) { }

    
    public bool TransitionAllowedTo(GameState state)
    {
        return state switch
        {
            GameState.overworld => true,
            _ => false
        };
    }
    
    private void InitializeUI(SpriteBatch spriteBatch)
    {
        RootUIElement = new RootElement(spriteBatch);
        
        RectElementFactory rectFactory = new RectElementFactory(RootUIElement);
        TextElementFactory textFactory = new TextElementFactory(RootUIElement);
        ButtonElementFactory buttonFactory = new ButtonElementFactory(RootUIElement);
        AnimSpriteElementFactory animSpriteFactory = new(RootUIElement);
        
        // Create semi-transparent overlay background
        Element overlayBg = rectFactory.CreateNew(new Color(0, 0, 0, 0.7f));
        overlayBg.OffsetAndSize = new Rectangle(0, 0, 1024, 768);
        RootUIElement.AddChild(overlayBg);
        
        // Create main menu panel
        Element menuBorder = rectFactory.CreateNew(new Color(40, 40, 80), 4, new Color(200, 200, 255));
        menuBorder.OffsetAndSize = new Rectangle(112, 84, 800, 600);
        RootUIElement.AddChild(menuBorder);
        
        Element menuBg = rectFactory.CreateNew(new Color(20, 20, 50));
        menuBg.OffsetAndSize = new Rectangle(116, 88, 792, 592);
        RootUIElement.AddChild(menuBg);
        
        // Create tab buttons container
        Element tabButtonContainer = new Element(RootUIElement);
        tabButtonContainer.OffsetAndSize = new Rectangle(116, 88, 792, 60);
        
        // Create the three tab buttons
        Element partyTabButton = buttonFactory.CreateNew(
            _game.DefaultSpriteFont, 
            _game, 
            new Rectangle(10, 10, 240, 40), 
            new Color(100, 50, 150), 
            Color.White, 
            "Party"
        );
        partyTabButton.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams eventParams = (InputUIEventParams)e;
            if (eventParams.Controls.Contains(UIControl.Interact))
            {
                SwitchToTab(MenuTab.Party);
            }
        });
        tabButtonContainer.AddChild(partyTabButton);
        
        Element inventoryTabButton = buttonFactory.CreateNew(
            _game.DefaultSpriteFont, 
            _game, 
            new Rectangle(272, 10, 240, 40), 
            new Color(100, 50, 150), 
            Color.White, 
            "Inventory"
        );
        inventoryTabButton.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams eventParams = (InputUIEventParams)e;
            if (eventParams.Controls.Contains(UIControl.Interact))
            {
                SwitchToTab(MenuTab.Inventory);
            }
        });
        tabButtonContainer.AddChild(inventoryTabButton);
        
        var optionsTabButton = buttonFactory.CreateNew(
            _game.DefaultSpriteFont, 
            _game, 
            new Rectangle(532, 10, 240, 40), 
            new Color(100, 50, 150), 
            Color.White, 
            "Options"
        );
        optionsTabButton.AddActionOnUIEvent(UIEvent.ButtonUp, (e) =>
        {
            InputUIEventParams eventParams = (InputUIEventParams)e;
            if (eventParams.Controls.Contains(UIControl.Interact))
            {
                SwitchToTab(MenuTab.Options);
            }
        });
        tabButtonContainer.AddChild(optionsTabButton);
        
        RootUIElement.AddChild(tabButtonContainer);
        
        // Create containers for each menu tab content
        CreatePartyMenu(rectFactory, textFactory);
        CreateInventoryMenu(rectFactory, textFactory, animSpriteFactory);
        CreateOptionsMenu(rectFactory, textFactory, buttonFactory);
        
        // Add all containers to root
        RootUIElement.AddChild(_partyContainer);
        RootUIElement.AddChild(_inventoryContainer);
        RootUIElement.AddChild(_optionsContainer);
        
        // Handle keyboard navigation between tabs
        RootUIElement.AddActionOnUIEvent(UIEvent.ButtonDown, (e) =>
        {
            InputUIEventParams eventParams = (InputUIEventParams)e;
            
            if (eventParams.Controls.Contains(UIControl.MoveDown))
            {
                if (RootUIElement.UIState.SelectionIndex == RootUIElement.UIState.SelectableElements.Count - 1)
                {
                    // At bottom, do nothing
                } else if (RootUIElement.UIState.SelectionIndex >= 0 && RootUIElement.UIState.SelectionIndex < 3)
                {
                    RootUIElement.UIState.SelectionIndex = 3;
                    UISoundFactory.PlayGrazeSoundEffect();
                } else
                {
                    RootUIElement.UIState.SelectionIndex += 1;
                    UISoundFactory.PlayGrazeSoundEffect();
                }
            }
            if (eventParams.Controls.Contains(UIControl.MoveUp))
            {
                if (RootUIElement.UIState.SelectionIndex == 3)
                {
                    RootUIElement.UIState.SelectionIndex = _currentTab switch
                    {
                        MenuTab.Party => 0,
                        MenuTab.Inventory => 1,
                        MenuTab.Options => 2,
                        _ => 0
                    };
                    UISoundFactory.PlayGrazeSoundEffect();
                } else if (RootUIElement.UIState.SelectionIndex >= 0 && RootUIElement.UIState.SelectionIndex < 3)
                {
                    // At top, do nothing
                } else
                {
                    RootUIElement.UIState.SelectionIndex -= 1;
                    UISoundFactory.PlayGrazeSoundEffect();
                }
            }
            if (eventParams.Controls.Contains(UIControl.MoveRight))
            {
                // Switch to next tab
                
                if (RootUIElement.UIState.SelectionIndex >= 0 && RootUIElement.UIState.SelectionIndex < 3)
                {
                    RootUIElement.UIState.SelectionIndex = (RootUIElement.UIState.SelectionIndex + 1) % 3;
                    int nextTab = ((int)_currentTab + 1) % 3;
                    SwitchToTab((MenuTab)nextTab);
                    UISoundFactory.PlayGrazeSoundEffect();
                }
            }
            if (eventParams.Controls.Contains(UIControl.MoveLeft))
            {
                // Switch to previous tab
                
                if (RootUIElement.UIState.SelectionIndex >= 0 && RootUIElement.UIState.SelectionIndex < 3)
                {
                    RootUIElement.UIState.SelectionIndex = (RootUIElement.UIState.SelectionIndex - 1 + 3) % 3;
                    int prevTab = ((int)_currentTab - 1 + 3) % 3;
                    SwitchToTab((MenuTab)prevTab);
                    UISoundFactory.PlayGrazeSoundEffect();
                }
            }
            if (eventParams.Controls.Contains(UIControl.Return))
            {
                // Close menu and return to overworld
                ChangeToOverworldState();
                UISoundFactory.PlaySelectSoundEffect();
            }
        });
        
        // Set initial selection and show Party tab
        RootUIElement.UIState.SelectionIndex = 0;
        SwitchToTab(MenuTab.Party);
    }
    
    private void CreatePartyMenu(RectElementFactory rectFactory, TextElementFactory textFactory)
    {
        _partyContainer = new(RootUIElement);
        _partyContainer.OffsetAndSize = new Rectangle(116, 158, 792, 522);
        
        // Party menu header
        Element partyHeader = textFactory.CreateNew(
            _game.DefaultSpriteFont, 
            "Party Members", 
            Color.White, 
            TextElementFactory.TextAlign.Center, 
            TextElementFactory.TextAlign.Left
        );
        partyHeader.OffsetAndSize = new Rectangle(0, 10, 792, 40);
        _partyContainer.AddChild(partyHeader);
        
        Element partyStatusContainer = new(RootUIElement);
        partyStatusContainer.OffsetAndSize = new Rectangle(745, 540, 260, 210);
        
        for (int i = 0; i < 4; i++)
        {
            Element allyHealth = new(RootUIElement);
            allyHealth.OffsetAndSize = new Rectangle(0, i * 55, 260, 45);

            Element currentBgRect = rectFactory.CreateNew(new Color(0, 0, 0, 0.2f));
            currentBgRect.OffsetAndSize = new Rectangle(0, 0, 260, 45);
            allyHealth.AddChild(currentBgRect);
            if (i < Player.Instance.Party.Count)
            {
                allyHealth.Attributes["associated_battle"] = Player.Instance.Party[i];
                PlayerStats associatedBattle = Player.Instance.Party[i];

                Element currentHealthBarBg = rectFactory.CreateNew(new Color(100, 0, 0));
                currentHealthBarBg.OffsetAndSize = new Rectangle(46, 29, 208, 10);
                allyHealth.AddChild(currentHealthBarBg);

                Element currentHealthBarFg = rectFactory.CreateNew(new Color(0, 200, 0));
                currentHealthBarFg.OffsetAndSize = new Rectangle(46, 29, 111, 10); 
                allyHealth.AddChild(currentHealthBarFg);

                Element currentTextElem = textFactory.CreateNew(_game.DefaultSpriteFont, "ALLY TEXT?", Color.White);
                currentTextElem.OffsetAndSize = new Rectangle(46, 2, 208, 20);
                allyHealth.AddChild(currentTextElem);
                int hp = associatedBattle.GetHealth();
                int maxHp = associatedBattle.GetMaxHealth();

                currentHealthBarFg.OffsetAndSize = new Rectangle(
                    currentHealthBarFg.OffsetAndSize.X,
                    currentHealthBarFg.OffsetAndSize.Y,
                    (int)(208 * ((float)hp / maxHp)),
                    currentHealthBarFg.OffsetAndSize.Height
                );

                currentTextElem.Attributes["text_string"] = $"{associatedBattle.Type} HP: {hp}/{maxHp}";
            }

            partyStatusContainer.AddChild(allyHealth);
        }
        partyStatusContainer.OffsetAndSize = new Rectangle(50, 80, 692, 400);
        _partyContainer.AddChild(partyStatusContainer);
    }

    private void CreateInventoryMenu(RectElementFactory rectFactory, TextElementFactory textFactory, AnimSpriteElementFactory animSpriteFactory)
    {
        _inventoryContainer = new(RootUIElement);
        _inventoryContainer.OffsetAndSize = new Rectangle(116, 158, 792, 522);

        // Inventory menu header
        Element inventoryHeader = textFactory.CreateNew(
            _game.DefaultSpriteFont,
            "Inventory",
            Color.White,
            TextElementFactory.TextAlign.Center,
            TextElementFactory.TextAlign.Left
        );
        inventoryHeader.OffsetAndSize = new Rectangle(0, 10, 792, 40);
        _inventoryContainer.AddChild(inventoryHeader);

        Element inventoryContent = new Element(RootUIElement);
        inventoryContent.OffsetAndSize = new Rectangle(745, 540, 260, 210);

        int consumables = 0;
        int equipments = 0;
        foreach (KeyValuePair<IInventoryItem.Type, int> keyValue in Player.Instance.Inventory)
        {
            if (keyValue.Value > 0)
            {
                Element itemContainer = new Element(RootUIElement);

                if (keyValue.Key.IsConsumable())
                {
                    itemContainer.OffsetAndSize = new Rectangle(0, consumables * 55, 260, 45);
                    consumables++;
                }
                else
                {
                    itemContainer.OffsetAndSize = new Rectangle(400, equipments * 55, 260, 45);
                    equipments++;
                }
                
                Element currentBgRect = rectFactory.CreateNew(new Color(0, 0, 0, 0.2f));
                currentBgRect.OffsetAndSize = new Rectangle(0, 0, 220, 45); //Should do variable width but I aint gonna
                itemContainer.AddChild(currentBgRect);

                var pickupIcon = animSpriteFactory.CreateNew();
                pickupIcon.Attributes["associated_anim_sprite"] = keyValue.Key.GetInventorySprite();
                pickupIcon.OffsetAndSize = new Rectangle(6, 7, 32, 32);
                itemContainer.AddChild(pickupIcon);

                string text = keyValue.Key.IsConsumable() ? $"{keyValue.Key}: {keyValue.Value}" : $"{keyValue.Key}";
                Element currentTextElem = textFactory.CreateNew(_game.DefaultSpriteFont, text, Color.White);
                currentTextElem.OffsetAndSize = new Rectangle(46, 2, 208, 20);
                itemContainer.AddChild(currentTextElem);
                inventoryContent.AddChild(itemContainer);
            }
        }
        inventoryContent.OffsetAndSize = new Rectangle(50, 80, 692, 400);
        _inventoryContainer.AddChild(inventoryContent);
    }
    
    private void CreateOptionsMenu(RectElementFactory rectFactory, TextElementFactory textFactory, ButtonElementFactory buttonFactory)
    {
        _optionsContainer = new Element(RootUIElement);
        _optionsContainer.OffsetAndSize = new Rectangle(116, 158, 792, 522);
        
        // Options menu header
        Element optionsHeader = textFactory.CreateNew(
            _game.DefaultSpriteFont, 
            "Options", 
            Color.White, 
            TextElementFactory.TextAlign.Center, 
            TextElementFactory.TextAlign.Left
        );
        optionsHeader.OffsetAndSize = new Rectangle(0, 10, 792, 40);
        _optionsContainer.AddChild(optionsHeader);

        Element optionsContent = new(RootUIElement);
        optionsContent.OffsetAndSize = new Rectangle(50, 80, 692, 400);

        optionsContent.AddChild(buttonFactory.CreateNew(_game.DefaultSpriteFont, _game, new Rectangle(0, 0, 320, 60), Color.Purple, Color.White, "Sound"));
        optionsContent.AddChild(buttonFactory.CreateNew(_game.DefaultSpriteFont, _game, new Rectangle(0, 70, 320, 60), Color.Purple, Color.White, "Graphics"));
        optionsContent.AddChild(buttonFactory.CreateNew(_game.DefaultSpriteFont, _game, new Rectangle(0, 140, 320, 60), Color.Purple, Color.White, "Controls"));

        _optionsContainer.AddChild(optionsContent);
    }
    
    private void SwitchToTab(MenuTab tab)
    {
        // Hide all containers
        _partyContainer.IsVisible = false;
        _inventoryContainer.IsVisible = false;
        _optionsContainer.IsVisible = false;
        
        // Make all children unselectable
        MakeContainerChildrenUnselectable(_partyContainer);
        MakeContainerChildrenUnselectable(_inventoryContainer);
        MakeContainerChildrenUnselectable(_optionsContainer);
        
        // Show the selected container
        _currentTab = tab;
        Element activeContainer = tab switch
        {
            MenuTab.Party => _partyContainer,
            MenuTab.Inventory => _inventoryContainer,
            MenuTab.Options => _optionsContainer,
            _ => _partyContainer
        };
        
        activeContainer.IsVisible = true;
        // MakeContainerChildrenSelectable(activeContainer);
        
        // Reset selection to tab buttons (index 0-2 are the tab buttons)
        RootUIElement.UIState.SelectionIndex = (int)tab;
        
        UISoundFactory.PlaySelectSoundEffect();
    }
    
    private void MakeContainerChildrenUnselectable(Element container)
    {
        foreach (var child in container.GetChildren())
        {
            child.MakeUnselectable();
        }
    }
    
    private void MakeContainerChildrenSelectable(Element container)
    {
        foreach (var child in container.GetChildren())
        {
            child.MakeSelectable();
        }
    }

}
