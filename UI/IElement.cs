using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;

namespace Sprint0.UI;

public interface IElement
{
    Rectangle OffsetAndSize { set; get; } // This is relative to the parent.
    IElement[] Children { get; }
    bool IsSelectable { set; get; }
    bool IsFloating { set; get; } // This makes the element show Top to others as being size 0x0.
    Color BackgroundColor { set; get; }
    AnimatableSprite BackgroundSprite { get; set; }
    Spacing Margin { set; get; }
    Spacing Padding { set; get; }
    ElementRoot RootElement { get; }
    FlowDirection Flow { get; set; }

    Point GetSpaceTaken();
    public void Draw(IElement parent); // On draw, it will be rendered relative to its parent.

    public struct Spacing
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public Spacing(int all)
        {
            Left = all;
            Top = all;
            Right = all;
            Bottom = all;
        }
        public Spacing(int x, int y)
        {
            Left = x;
            Top = y;
            Right = x;
            Bottom = y;
        }
        public Spacing(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
    public enum FlowDirection { Row, Column, ReverseRow, ReverseColumn }
}



