using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineDemo;

public class PlayerTest
{
    int thing;
    int thing2;
    StateMachine sm;
    public enum States {Walking,Idle};

    public PlayerTest()
    {
        thing = 0;
        sm = new StateMachine();
    }

    public void Update()
    {
        sm.Update();
    }
}
