// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the root element that will contain every other class. This should probably only be used once.
// Unlike the other elements, this will also contain references to objects needed to draw the UI.

using System;
using AwesomeRPG.UI.Components;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI.Elements;

public class RootElement : ElementBase
{
    public SpriteBatch SpriteBatch { get; } // The SpriteBatch being used to draw the UI
    public Texture2D RectangleTexture { get; } // The RectangleTexture used to draw rectangles-- a 1x1 white pixel
    public UIState UIState { get; private set; } // The UI State that is attached to this root element

    public new void Draw(GameTime gameTime)
    {
        RecursiveDraw(this, gameTime);
    }

    private void RecursiveDraw(ElementBase element, GameTime gameTime)
    {
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(element, gameTime));
        element.CalculateDerivedValuesFromAncestors();
        if (element.IsVisible)
        {
            element.Draw(gameTime);
            if (element is ComponentBase) {
                foreach (ElementBase child in ((ComponentBase)element).ComponentBaseElement._children)
                {
                    RecursiveDraw(child, gameTime);
                }
            } else
            {
                foreach (ElementBase child in element._children)
                {
                    RecursiveDraw(child, gameTime);
                }
            }
        }
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(element, gameTime));
    }

    public new void Update(GameTime gameTime)
    {
        RecursiveUpdate(this, gameTime);
    }

    private void RecursiveUpdate(ElementBase element, GameTime gameTime)
    {
        DispatchUIEvent(UIEvent.BeforeUpdate, new DrawUIEventParams(element, gameTime));
        element.CalculateDerivedValuesFromAncestors();
        element.Update(gameTime);
        if (element is ComponentBase) {
            foreach (ElementBase child in ((ComponentBase)element).ComponentBaseElement._children)
            {
                RecursiveUpdate(child, gameTime);
            }
        } else
        {
            foreach (ElementBase child in element._children)
            {
                RecursiveUpdate(child, gameTime);
            }
        }
        DispatchUIEvent(UIEvent.AfterUpdate, new DrawUIEventParams(element, gameTime));
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