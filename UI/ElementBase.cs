// This is the root element that will contain every other class. This should only be used once.

using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI;

public abstract class ElementBase
{
    public Rectangle OffsetAndSize { get; set; } = new Rectangle();
    public Point DerivedAbsolutePosition { get; private set; } = new Point(0, 0);
    public bool DerivedAncestorIsSelected { get; private set; } = false;
    public ElementBase[] Children { get; } = [];
    public bool IsSelectable { get; private set; } = false;
    public bool IsSelected = false;
    public ElementRoot RootElement { get; protected set; }

    public abstract void Draw(GameTime gameTime, ElementBase parent);

    protected void CalculateDerivedValuesFromAncestors(ElementBase parent)
    {
        DerivedAbsolutePosition = parent.DerivedAbsolutePosition + OffsetAndSize.Location;
        DerivedAncestorIsSelected = parent.IsSelected || parent.DerivedAncestorIsSelected;
    }

    protected void DrawChildren(GameTime gameTime)
    {
        // We will draw each of this element's children.
        foreach (ElementBase child in Children)
        {
            child.Draw(gameTime, this);
        }
    }

    public void MakeSelectable()
    {
        if (!IsSelectable)
        {
            IsSelectable = true;
            RootElement.RegisterSelectableElement(this);
        }
    }

    public void MakeUnselectable()
    {
        IsSelectable = false;
            RootElement.UnregisterSelectableElement(this);
    }
}