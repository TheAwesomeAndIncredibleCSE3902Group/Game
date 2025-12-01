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
        Console.WriteLine("Using key on level ({0},{1}), targetID {2}", X, Y, ID);
    }
}