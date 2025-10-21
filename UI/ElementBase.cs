// This is the root element that will contain every other class. This should only be used once.

using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace Sprint0.UI;

public abstract class ElementBase : IElement
{
    public Rectangle OffsetAndSize { get; set; } = new Rectangle();
    public IElement[] Children { get; } = [];
    public bool IsSelectable { get; set; } = false;
    public bool IsFloating { get; set; } = false;
    public Color BackgroundColor { get; set; } = new Color(255, 255, 255, 0);
    public AnimatableSprite BackgroundSprite { get; set; } = null;
    public Spacing Margin { get; set; } = new Spacing(0);
    public Spacing Padding { get; set; } = new Spacing(0);
    public ElementRoot RootElement { get; protected set; }
    public FlowDirection Flow { get; set; } = FlowDirection.Row;
    
    public abstract void Draw(IElement parent);

    public Point GetSpaceTaken() // width and height of the area that the element takes up, including margin
    {
        return new Point(OffsetAndSize.Width + Margin.Left + Margin.Right, OffsetAndSize.Height + Margin.Top + Margin.Bottom);
    }
}