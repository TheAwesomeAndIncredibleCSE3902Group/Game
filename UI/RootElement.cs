// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the root element that will contain every other class. This should probably only be used once.
// Unlike the other elements, this will also contain references to objects needed to draw the UI.

using System;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI;

public class RootElement : Element
{
    public SpriteBatch SpriteBatch { get; } // The SpriteBatch being used to draw the UI
    public Texture2D RectangleTexture { get; } // The RectangleTexture used to draw rectangles-- a 1x1 white pixel
    public UIState UIState { get; private set; } // The UI State that is attached to this root element

    public new void Draw(GameTime gameTime)
    {
        RecursiveDraw(this, gameTime);
    }

    private void RecursiveDraw(Element element, GameTime gameTime)
    {
        element.DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(element, gameTime));
        element.CalculateDerivedValuesFromAncestors();
        if (element.IsVisible)
        {
            element.Draw(gameTime);
            foreach (Element child in element._children)
            {
                RecursiveDraw(child, gameTime);
            }
        }
        element.DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(element, gameTime));
    }

    public new void Update(GameTime gameTime)
    {
        UIState.UpdateElementsAreSelected();
        RecursiveUpdate(this, gameTime);
    }

    private void RecursiveUpdate(Element element, GameTime gameTime)
    {
        element.DispatchUIEvent(UIEvent.BeforeUpdate, new DrawUIEventParams(element, gameTime));
        element.CalculateDerivedValuesFromAncestors();
        element.Update(gameTime);
        foreach (Element child in element._children)
        {
            RecursiveUpdate(child, gameTime);
        }
        element.DispatchUIEvent(UIEvent.AfterUpdate, new DrawUIEventParams(element, gameTime));
    }
    
    public RootElement(SpriteBatch spriteBatch) : base()
    {
        SetUpElement(this); // Set root element to self

        SpriteBatch = spriteBatch;

        UIState = new UIState(this);

        // Create the rectangle texture
        RectangleTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        RectangleTexture.SetData([Color.White]);
    }
}