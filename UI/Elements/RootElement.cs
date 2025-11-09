// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the root element that will contain every other class. This should probably only be used once.
// Unlike the other elements, this will also contain references to objects needed to draw the UI.

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI.Elements;

public class RootElement : ElementBase
{
    public SpriteBatch SpriteBatch { get; } // The SpriteBatch being used to draw the UI
    public Texture2D RectangleTexture { get; } // The RectangleTexture used to draw rectangles-- a 1x1 white pixel
    public UIState UIState { get; private set; } // The UI State that is attached to this root element

    public override void Draw(GameTime gameTime)
    {
        // The root element does not draw anything for itself, (no background color or animatable sprite)
        // We will not run CalculateDerivedValuesFromAncestors. DerivedAbsolutePosition is always 0, 0 and does not need to be updated

        // We will draw each of this element's children.
        // System.Console.WriteLine("drawing root element");
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(this, gameTime));
        UIState.UpdateElementsAreSelected();
        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(this, gameTime));
    }
    
    public RootElement(SpriteBatch spriteBatch)
    {
        SetUpElement(this); // Set root element to self

        SpriteBatch = spriteBatch;

        UIState = new UIState(this);

        // Create the rectangle texture
        RectangleTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        RectangleTexture.SetData([Color.White]);
    }
}