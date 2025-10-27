using AwesomeRPG.UI;

namespace AwesomeRPG.Commands;

public class CommandUIControl : ICommand
{
    private Game1 _myGame;
    private UIControl _myUIControl;
    private UIControlEvent _myUIControlEvent;
    public void Execute()
    {
        _myGame.RootUIElement.UIState.RunControlActions(_myUIControl, _myUIControlEvent);
    }
    
    public CommandUIControl(Game1 game, UIControl control, UIControlEvent uiControlEvent)
    {
        _myGame = game;
        _myUIControl = control;
        _myUIControlEvent = uiControlEvent;
    }
}
