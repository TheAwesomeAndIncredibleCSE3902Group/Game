// This is the root element that will contain every other class. This should only be used once.
// Unlike the other elements, this will also contain references to objects needed to draw the UI.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.UI;

public class ElementRoot : ElementBase
{
    public SpriteBatch SpriteBatch { get; } // The SpriteBatch being used to draw the UI
    public Texture2D RectangleTexture { get; } // The RectangleTexture used to draw rectangles-- a 1x1 white pixel
    private readonly SortedDictionary<int, SortedDictionary<int, List<ElementBase>>> _selectableElementsXY; // [X, Y, Z] used to find next in y direction
    private readonly SortedDictionary<int, SortedDictionary<int, List<ElementBase>>> _selectableElementsYX; // [Y, X, Z] used to find next in x direction
    private readonly Dictionary<ElementBase, Point> _selectableElementsLocations; // The locations of where the elements were initially added

    public Point SelectionLocation = Point.Zero; // This is the "location" of the currently selected element.

    // HELP i am insane i dont know why i did it like this BUT i did !!!!!!
    public void RegisterSelectableElement(ElementBase element)
    {
        _selectableElementsLocations.Add(element, element.DerivedAbsolutePosition);
        // Add it to the XY 2d dictionary
        bool XY_X_AlreadyExists = _selectableElementsXY.TryGetValue(element.DerivedAbsolutePosition.X, out SortedDictionary<int, List<ElementBase>> XY_X_Dict);

        if (!XY_X_AlreadyExists)
        {
            // If the X dict doesn't exist make a new one and add it to the XY dict
            XY_X_Dict = [];
            _selectableElementsXY.Add(element.DerivedAbsolutePosition.X, XY_X_Dict);
        }

        // Now, we will see if there is a list that already exists for the Y. If not, we make a new list for this value.
        // Otherwise we will add it to the new list.
        bool XY_Y_AlreadyExists = XY_X_Dict.TryGetValue(element.DerivedAbsolutePosition.Y, out List<ElementBase> XY_Y_List);
        if (!XY_Y_AlreadyExists)
        {
            XY_X_Dict.Add(element.DerivedAbsolutePosition.Y, new List<ElementBase>([element]));
        }
        else
        {
            XY_Y_List.Add(element);
        }

        // Now we do the same thing with the YX 2d dictionary.
        // Add it to the YX 2d dictionary
        bool YX_Y_AlreadyExists = _selectableElementsYX.TryGetValue(element.DerivedAbsolutePosition.Y, out SortedDictionary<int, List<ElementBase>> YX_Y_Dict);

        if (!YX_Y_AlreadyExists)
        {
            // If the Y dict doesn't exist make a new one and add it to the YX dict
            XY_X_Dict = [];
            _selectableElementsXY.Add(element.DerivedAbsolutePosition.Y, YX_Y_Dict);
        }

        // Now, we will see if there is a list that already exists for the X. If not, we make a new list for this value.
        // Otherwise we will add it to the new list.
        bool YX_X_AlreadyExists = XY_X_Dict.TryGetValue(element.DerivedAbsolutePosition.Y, out List<ElementBase> YX_X_List);
        if (!YX_X_AlreadyExists)
        {
            YX_Y_Dict.Add(element.DerivedAbsolutePosition.Y, new List<ElementBase>([element]));
        }
        else
        {
            YX_X_List.Add(element);
        }
    }
    
    public void UnregisterSelectableElement(ElementBase element)
    {
        bool elementWasRegistered = _selectableElementsLocations.TryGetValue(element, out Point InitialElemPosition);
        if (!elementWasRegistered)
        {
            throw new Exception("Tried to unregister a selectable element that was not previously registered.");
        }
        _selectableElementsXY[InitialElemPosition.X][InitialElemPosition.Y].Remove(element);
        if (_selectableElementsXY[InitialElemPosition.X][InitialElemPosition.Y].Count == 0)
        {
            _selectableElementsXY[InitialElemPosition.X].Remove(InitialElemPosition.Y);
            if (_selectableElementsXY[InitialElemPosition.X].Count == 0)
            {
                _selectableElementsXY.Remove(InitialElemPosition.X);
            }
        }
        _selectableElementsYX[InitialElemPosition.Y][InitialElemPosition.X].Remove(element);
        if (_selectableElementsYX[InitialElemPosition.Y][InitialElemPosition.X].Count == 0)
        {
            _selectableElementsYX[InitialElemPosition.Y].Remove(InitialElemPosition.X);
            if (_selectableElementsYX[InitialElemPosition.Y].Count == 0)
            {
                _selectableElementsYX.Remove(InitialElemPosition.Y);
            }
        }
    }
    

    public override void Draw(GameTime gameTime, ElementBase parent)
    {
        // We will ignore the parent element because it would be null for the parent element.
        // The root element does not draw anything for itself, (no background color or animatable sprite)
        // We will not run CalculateDerivedValuesFromAncestors. DerivedAbsolutePosition is always 0, 0 and does not need to be updated

        // We will draw each of this element's children.
        DrawChildren(gameTime);
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