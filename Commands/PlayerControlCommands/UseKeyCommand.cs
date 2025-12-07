using System;
using AwesomeRPG.Map;

namespace AwesomeRPG.Commands;

public class UseKeyCommand: ICommand
{
    private readonly int X;
    private readonly int Y;
    private readonly int ID;
    public UseKeyCommand(int targetX, int targetY, int targetID)
    {
        X = targetX;
        Y = targetY;
        ID = targetID;
    }

    public void Execute()
    {
        RoomAtlas.Instance.UnlockLock(X, Y, ID);
    }
}