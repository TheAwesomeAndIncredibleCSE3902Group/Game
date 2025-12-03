# UI Framework Usage Guide

This UI framework was worked on by Eli L. If you need help with the UI framework, feel free to ask!

NOTE: This documentation was created with the help of GenAI, but has been reviewed and edited for accuracy. (See bottom of document for citation).

Also make sure you read this with a markdown previewer! (Such as VSCode's built-in one)

## Overview

This is a hierarchical UI framework built on top of MonoGame. It uses a tree structure of `Element` objects with a single `RootElement` at the top. Elements can be positioned, made selectable, respond to events, and be animated.

## Core Concepts

### The Element Hierarchy

The UI system is organized as a tree:
- **RootElement**: The top-level container (created once per UI context)
  - **Child Elements**: Can contain other elements
    - **Grandchild Elements**: And so on...

Each element knows its parent and can have multiple children.

### Key Properties

Every `Element` has the following properties:

#### Modifiable Properties:

| Property | Type | Purpose |
|----------|------|---------|
| `OffsetAndSize` | `Rectangle` | Position (X, Y) and size (Width, Height) |
| `IsVisible` | `bool` | Whether element and children are drawn |
| `IsSelectable` | `bool` | Whether element can be selected |
| `Opacity` | `float` | Alpha transparency (0.0 to 1.0) |

#### Read-only Properties:

| Property | Type | Purpose |
|----------|------|---------|
| `Attributes` | `Dictionary<string, object>` |  Custom data storage <br>*(can still set entries; only the dictionary itself is read-only!)* |
| `DerivedAbsolutePosition` | `Point` | Calculated absolute position on screen (offset + parent's absolute position) |
| `DerivedAncestorIsSelected` | `bool` | Whether any ancestor element is selected |
| `DerivedAncestorIsVisible` | `bool` | Whether all ancestor elements are visible |
| `IsSelected` | `bool` | Whether element is currently selected |
| `Parent` | `Element` | Reference to parent element (null for root) |
| `RootElement` | `RootElement` | Reference to the root element |

## Getting Started

### Step 1: Create a RootElement

```csharp
// In your game state or initialization code
RootElement rootUIElement = new RootElement(spriteBatch);
```

The `RootElement` requires:
- A `SpriteBatch` for rendering
- Automatically creates a white 1x1 texture for rectangle drawing

### Step 2: Create Child Elements

Create UI elements as children of the root:

```csharp
// Create a background rectangle
var background = new RectElement(rootUIElement, Color.Black);
background.OffsetAndSize = new Rectangle(10, 10, 400, 300);
rootUIElement.AddChild(background);

// Create text
var spriteFont = content.Load<SpriteFont>("Fonts\\MyFont");
var textElement = new TextElement(rootUIElement, spriteFont, "Hello World!")
{
    OffsetAndSize = new Rectangle(20, 20, 200, 50),
    TextColor = Color.White
};
rootUIElement.AddChild(textElement);
```

### Step 3: Update and Draw

In your game loop:

```csharp
// In Update()
rootUIElement.Update(gameTime);

// In Draw()
rootUIElement.Draw(gameTime);
```

## Element Types

### RectElement

Draws a colored rectangle with optional outline.

```csharp
var rect = new RectElement(rootUIElement, Color.Red);
rect.OffsetAndSize = new Rectangle(0, 0, 100, 100);
rootUIElement.AddChild(rect);

// You can change the fill color through Attributes
rect.Attributes["fill_color"] = Color.Blue;

// Add an outline by setting outline thickness and color
rect.Attributes["outline_thickness"] = 2;      // Pixels thick
rect.Attributes["outline_color"] = Color.Black;

// Create a rectangle with outline using the factory
var rectFactory = new RectElementFactory(rootUIElement);
var outlinedRect = rectFactory.CreateNew(
    fillColor: Color.Green,
    outlineThickness: 3,
    outlineColor: Color.White
);
outlinedRect.OffsetAndSize = new Rectangle(150, 50, 100, 80);
rootUIElement.AddChild(outlinedRect);
```

**Outline Properties:**
- `"fill_color"` - `Color`: The main rectangle color
- `"outline_color"` - `Color`: The border color (default: Black)
- `"outline_thickness"` - `int`: Border width in pixels (default: 0 = no outline)

### TextElement

Displays text with alignment options.

```csharp
var text = new TextElement(rootUIElement, spriteFont, "Score: 1000", Color.White)
{
    OffsetAndSize = new Rectangle(10, 10, 200, 50),
    HorizontalTextAlign = TextElement.TextAlign.Center,
    VerticalTextAlign = TextElement.TextAlign.Center
};
rootUIElement.AddChild(text);
```

Alignment options: `Left`, `Center`, `Right`

### AnimSpriteElement

Displays an `AnimatedSprite`.

```csharp
var sprite = new AnimSpriteElement(rootUIElement, animatableSprite);
sprite.OffsetAndSize = new Rectangle(0, 0, 64, 64);
rootUIElement.AddChild(sprite);
```

### SelectionAnimationElement

Displays an animated selection indicator (glowing border) around its child elements. Only visible when the element or its ancestor is selected.

**Key Properties:**
- Automatically animates when selected
- Uses `DerivedAncestorIsSelected` to determine visibility
- Draws a pulsing border effect using `gameTime`

**Basic Usage:**

```csharp
var selectionAnim = new SelectionAnimationElement(rootUIElement);
selectionAnim.OffsetAndSize = new Rectangle(100, 100, 200, 50);

// Add other elements as children to have them glow when selected
var rect = new RectElement(rootUIElement, Color.Blue);
rect.OffsetAndSize = new Rectangle(Point.Zero, selectionAnim.OffsetAndSize.Size);
selectionAnim.AddChild(rect);

rootUIElement.AddChild(selectionAnim);
selectionAnim.MakeSelectable();
```

**Using SelectionAnimationElement in Buttons:**

The `ButtonElementFactory` demonstrates the proper way to use `SelectionAnimationElement`. It creates a hierarchy:

```
CommandElement (root of button, handles input)
├── SelectionAnimationElement (shows pulsing glow when selected)
│   └── RectElement (background color)
│       └── TextElement (button label)
```

This structure ensures:
- The animation only shows when the button is selected
- Click effects modify the rectangle color
- Text remains centered and visible

**Complete Button Example:**

```csharp
// See ButtonElementFactory in the UI/ElementFactories/ButtonElementFactory.cs file
// A full working example of SelectionAnimationElement usage
var buttonFactory = new ButtonElementFactory(rootUIElement);
var button = buttonFactory.CreateNew(
    spriteFont, 
    game,
    new Rectangle(100, 100, 200, 50),
    Color.Purple,
    Color.White,
    "Click Me"
);

rootUIElement.AddChild(button);
rootUIElement.UIState.SelectionIndex = 0;  // Select the button to see animation
```

## Making Elements Selectable

To make an element respond to selection (like menu items):

```csharp
var menuItem = new TextElement(rootUIElement, spriteFont, "Option 1");
menuItem.MakeSelectable();
rootUIElement.AddChild(menuItem);

// Now manage selection through UIState
rootUIElement.UIState.SelectionIndex = 0; // Select first item
Element selected = rootUIElement.UIState.SelectedElement; // Get current selection
```

To stop an element from being selectable:

```csharp
menuItem.MakeUnselectable();
```

## Event System

Elements can respond to UI events through a subscription model.

### Available Events

```csharp
public enum UIEvent 
{ 
    BeforeDraw,    // Called before element draws
    Draw,          // Element's draw event
    AfterDraw,     // Called after element draws
    BeforeUpdate,  // Called before element updates
    Update,        // Element's update event
    AfterUpdate,   // Called after element updates
    Select,        // Element becomes selected
    Unselect,      // Element loses selection
    ButtonDown,    // Button pressed
    ButtonUp,      // Button released
    ButtonPress    // Button interaction
}
```

### Subscribing to Events

```csharp
// Subscribe to a selection event
textElement.AddActionOnUIEvent(UIEvent.Select, (eventParams) =>
{
    Console.WriteLine("Element selected!");
});

// Subscribe to draw event
textElement.AddActionOnUIEvent(UIEvent.Draw, (eventParams) =>
{
    // Draw custom content
    var drawParams = (DrawUIEventParams)eventParams;
    // Use drawParams.GameTime and rootUIElement.SpriteBatch
});

// Unsubscribe from event
textElement.RemoveActionOnUIEvent(UIEvent.Select, myAction);
```

### Event Parameters

Events provide context through parameter objects that you must cast before using:

#### DrawUIEventParams
Used by `BeforeDraw`, `Draw`, and `AfterDraw` events. Provides timing information.

```csharp
element.AddActionOnUIEvent(UIEvent.Draw, (eventParams) =>
{
    var drawParams = (DrawUIEventParams)eventParams;
    GameTime gameTime = drawParams.GameTime;
    Element elem = drawParams.Element;  // The element that triggered the event
    
    // Access drawing resources through the root element
    SpriteBatch spriteBatch = elem.RootElement.SpriteBatch;
    Texture2D rectTexture = elem.RootElement.RectangleTexture;
});
```

#### PlainUIEventParams
Used by `Select`, `Unselect`, and update events. Contains just the element reference.

```csharp
element.AddActionOnUIEvent(UIEvent.Select, (eventParams) =>
{
    var plainParams = (PlainUIEventParams)eventParams;
    Element selectedElement = plainParams.Element;
    Console.WriteLine($"Element selected: {selectedElement}");
});
```

#### InputUIEventParams
Used by `ButtonDown`, `ButtonUp`, and `ButtonPress` events. Contains input data.

```csharp
rootElement.AddActionOnUIEvent(UIEvent.ButtonDown, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    List<UIControl> pressedControls = inputParams.Controls;
    Element elem = inputParams.Element;
    
    // Check if Interact button was pressed
    if (pressedControls.Contains(UIControl.Interact))
    {
        Console.WriteLine("Interact button pressed!");
    }
});
```

**Note**: `UIControl` has values like: `MoveUp`, `MoveLeft`, `MoveRight`, `MoveDown`, `Interact`, `Return`

## Working with Input

### Input Event Dispatch

Input events (`ButtonDown`, `ButtonUp`, `ButtonPress`) are automatically dispatched to **both**:
1. The **RootElement** - for global input handling
2. The **currently selected element** - for element-specific input handling

This two-tier system allows you to handle input at different levels:

**Global Input (RootElement)**:
```csharp
rootUIElement.AddActionOnUIEvent(UIEvent.ButtonPress, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    // This runs for ALL button presses, regardless of selection
    if (inputParams.Controls.Contains(UIControl.Return))
    {
        HandlePauseMenu(); // Global action
    }
});
```

**Selected Element Input**:
```csharp
var button = new TextElement(rootUIElement, font, "Attack");
button.MakeSelectable();

button.AddActionOnUIEvent(UIEvent.ButtonPress, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    // This only runs when this button is selected
    if (inputParams.Controls.Contains(UIControl.Interact))
    {
        ExecuteAttack(); // Element-specific action
    }
});
```

### Common Input Patterns

**Button with Interact Handler**:
```csharp
var actionButton = new TextElement(rootUIElement, spriteFont, "Use Item");
actionButton.MakeSelectable();

actionButton.AddActionOnUIEvent(UIEvent.ButtonPress, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    if (inputParams.Controls.Contains(UIControl.Interact))
    {
        // Perform the action
        inventory.UseSelectedItem();
    }
});
```

**Global Navigation vs Selected Navigation**:
```csharp
// Global navigation (all arrow keys work)
rootUIElement.AddActionOnUIEvent(UIEvent.ButtonDown, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    
    if (inputParams.Controls.Contains(UIControl.MoveDown))
    {
        rootUIElement.UIState.SelectionIndex++;
    }
    if (inputParams.Controls.Contains(UIControl.MoveUp))
    {
        rootUIElement.UIState.SelectionIndex--;
    }
});

// Selected element can have its own handlers
selectedElement.AddActionOnUIEvent(UIEvent.ButtonDown, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    if (inputParams.Controls.Contains(UIControl.MoveRight))
    {
        AdjustValue(1); // Element-specific action
    }
});
```

**Checking Multiple Controls**:
```csharp
element.AddActionOnUIEvent(UIEvent.ButtonDown, (eventParams) =>
{
    var inputParams = (InputUIEventParams)eventParams;
    List<UIControl> controls = inputParams.Controls;
    
    // Multiple conditions
    if (controls.Contains(UIControl.Interact) && isConfirmation)
    {
        ConfirmSelection();
    }
    
    // Check for multiple buttons pressed
    if (controls.Contains(UIControl.MoveLeft) && controls.Contains(UIControl.MoveUp))
    {
        MoveDiagonalUpLeft();
    }
});
```

### Input Event Types

- **`ButtonDown`**: Triggered when a button is first pressed (single frame when transition from not-pressed to pressed)
- **`ButtonUp`**: Triggered when a button is released (single frame when transition from pressed to not-pressed)
- **`ButtonPress`**: Triggered while a button is held down (every frame the button remains pressed)

## Selection Management

The `UIState` class manages which element is currently selected.

```csharp
UIState uiState = rootUIElement.UIState;

// Set selection by index
uiState.SelectionIndex = 0;

// Get currently selected element
Element current = uiState.SelectedElement;

// Navigate selection
uiState.SelectionIndex++; // Move to next selectable element
uiState.SelectionIndex--; // Move to previous

// Selection wraps around automatically
```

### Registering Elements as Selectable

When you call `MakeSelectable()` on an element, it's automatically registered with the `UIState`. Manual registration is rarely needed:

```csharp
// Automatic (recommended)
element.MakeSelectable();

// Manual (if needed)
uiState.RegisterSelectableElement(element);

// Unregister
uiState.UnregisterSelectableElement(element);
```

## Complete Example: Simple Menu

Here's a complete example of a simple menu UI:

```csharp
public class MyMenuState : IGameState
{
    private RootElement rootUIElement;
    private Game1 game;

    public MyMenuState(Game1 game)
    {
        this.game = game;
    }

    private void InitUI(SpriteBatch spriteBatch)
    {
        rootUIElement = new RootElement(spriteBatch);
        var spriteFont = game.Content.Load<SpriteFont>("Fonts\\MyFont");

        // Background
        var background = new RectElement(rootUIElement, Color.DarkBlue);
        background.OffsetAndSize = game.GetScreenRect();
        rootUIElement.AddChild(background);

        // Menu title
        var title = new TextElement(rootUIElement, spriteFont, "Main Menu", Color.White)
        {
            OffsetAndSize = new Rectangle(100, 50, 800, 100),
            HorizontalTextAlign = TextElement.TextAlign.Center,
            VerticalTextAlign = TextElement.TextAlign.Center
        };
        rootUIElement.AddChild(title);

        // Menu items
        string[] menuOptions = { "Play", "Settings", "Quit" };
        for (int i = 0; i < menuOptions.Length; i++)
        {
            var menuItem = new TextElement(rootUIElement, spriteFont, menuOptions[i], Color.White)
            {
                OffsetAndSize = new Rectangle(200, 200 + (i * 80), 600, 60),
                HorizontalTextAlign = TextElement.TextAlign.Center,
                VerticalTextAlign = TextElement.TextAlign.Center
            };

            // Make selectable and add hover effect
            menuItem.MakeSelectable();
            menuItem.AddActionOnUIEvent(UIEvent.Select, (eventParams) =>
            {
                var elem = ((PlainUIEventParams)eventParams).Element;
                if (elem is TextElement textElem)
                    textElem.TextColor = Color.Yellow;
            });

            menuItem.AddActionOnUIEvent(UIEvent.Unselect, (eventParams) =>
            {
                var elem = ((PlainUIEventParams)eventParams).Element;
                if (elem is TextElement textElem)
                    textElem.TextColor = Color.White;
            });

            rootUIElement.AddChild(menuItem);
        }

        // Set initial selection
        rootUIElement.UIState.SelectionIndex = 0;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (rootUIElement == null)
        {
            InitUI(spriteBatch);
        }
        rootUIElement.Draw(gameTime);
    }

    public void Update(GameTime gameTime)
    {
        // Handle input for navigation
        if (InputManager.IsPressed(Keys.Down))
        {
            rootUIElement.UIState.SelectionIndex++;
        }
        if (InputManager.IsPressed(Keys.Up))
        {
            rootUIElement.UIState.SelectionIndex--;
        }

        rootUIElement.Update(gameTime);
    }

    // ... other IGameState methods
}
```

## Real Example: Battle UI

See `BattleState.cs` for the actual implementation in the project. It demonstrates:

```csharp
public class BattleState : IGameState
{
    private RootElement RootUIElement { get; set; }

    public BattleState(OverworldState overState, Game1 game, CharacterEnemyBase[] enemies)
    {
        this.game = game;
        this.RootUIElement = game.RootUIElement;
        // ... initialization
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        RootUIElement.Draw(gameTime);
    }

    public void Update(GameTime gameTime)
    {
        RootUIElement.Update(gameTime);
    }
}
```

## Parent-Child Relationships

### Adding and Removing Children

```csharp
// Add a child
parent.AddChild(child);

// Remove a child
parent.RemoveChild(child);

// Get all children (returns a copy of the list)
List<Element> children = parent.GetChildren();
```

**Important**: An element can only have one parent. Attempting to add an element that already has a parent will generate an error message.

### Position Calculation

Child positions are relative to their parent:

```csharp
var parent = new DivElement(rootUIElement);
parent.OffsetAndSize = new Rectangle(100, 100, 300, 300);

var child = new RectElement(rootUIElement, Color.Red);
child.OffsetAndSize = new Rectangle(10, 10, 50, 50); // Relative to parent

// Child's DerivedAbsolutePosition will be (110, 110)
```

## Visibility Cascade

An element's visibility is determined by both its own `IsVisible` property AND all ancestors:

```csharp
parent.IsVisible = false;
child.IsVisible = true;

// child.DerivedAncestorIsVisible will be false
// Even though child.IsVisible is true, it won't render
```

This applies to selection as well: `DerivedAncestorIsSelected`.

## Custom Data Storage

Use the `Attributes` dictionary to store custom data on elements:

```csharp
element.Attributes["score"] = 1000;
element.Attributes["playerName"] = "Hero";

int score = (int)element.Attributes["score"];
```

## Best Practices

1. **Create UI once**: Initialize UI once per state (not every frame)
2. **Plan your hierarchy**: Think about the tree structure before creating elements
3. **Subscribe to events for interactivity**: Use events rather than polling in Update()
4. **Keep track of references**: Store references to elements you need to modify later
5. **Manage selection carefully**: Only make interactive elements selectable

## Common Patterns

### Menu Navigation

```csharp
if (InputManager.IsPressed(Keys.Up))
    rootUIElement.UIState.SelectionIndex--;
if (InputManager.IsPressed(Keys.Down))
    rootUIElement.UIState.SelectionIndex++;
if (InputManager.IsPressed(Keys.Enter))
    HandleSelection(rootUIElement.UIState.SelectedElement);
```

### Conditionally Showing/Hiding Elements

```csharp
// Hide when not active
element.IsVisible = false;

// Show when active
element.IsVisible = true;

// Toggle
element.IsVisible = !element.IsVisible;
```

### Conditional Selection

```csharp
if (alliesDefeated)
{
    rootUIElement.UIState.SelectedElement = gameOverElement;
}
```

### Updating Element Content

```csharp
if (textElement is TextElement text)
{
    text.TextString = $"Health: {player.CurrentHealth}/{player.MaxHealth}";
    text.OffsetAndSize = new Rectangle(10, 10, 200, 30);
}
```

## Debugging Tips

The `Element` class captures debug information about where it was created:
- Check console output for error messages about registration/parenting issues
- Verify `DerivedAbsolutePosition` to debug positioning issues
- Use the `DEBUG_codeThatCreatedMe` field to trace element creation

---

## Citation

**Documentation Creation**: This documentation was generated with the assistance of Claude Haiku 4.5 using GitHub Copilot.

**Prompt Used**: "Create documentation of using the ui framework. See implementation in the surrounding files. There is an example of correct usage inside of BattleUIState."
