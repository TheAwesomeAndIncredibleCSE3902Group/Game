// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

namespace AwesomeRPG.UI;

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
public enum UIControl { MoveUp, MoveLeft, MoveRight, MoveDown, Interact, Return };

public enum UIEvent { BeforeDraw, AfterDraw, BeforeUpdate, AfterUpdate, Select, Unselect, ButtonDown, ButtonUp, ButtonPress }; // KeyDown, KeyUp, KeyPress
