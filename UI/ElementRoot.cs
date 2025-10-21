// This is the root element that will contain every other class. This should only be used once.
// Unlike the other elements, this will also contain references to objects needed to draw the UI.

using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.UI;

public class ElementRoot : ElementBase
{
    public SpriteBatch SpriteBatch { get; } // The SpriteBatch being used to draw the UI
    public Texture2D RectangleTexture { get; } // The RectangleTexture used to draw rectangles-- a 1x1 white pixel

    public override void Draw(IElement parent)
    {
        // We will ignore the parent element because it will be null for the parent element.
        // The root element does not draw anything, (no background color or animatable sprite)

        // We will draw each of this element's children.
        foreach (IElement child in Children)
        {
            child.Draw(this);
        }
    }

    public ElementRoot(SpriteBatch spriteBatch)
    {
        RootElement = this; // Set root element to self

        SpriteBatch = spriteBatch;

        // Create the rectangle texture
        RectangleTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        RectangleTexture.SetData([Color.White]);
    }
}