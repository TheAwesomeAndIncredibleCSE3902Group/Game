using AwesomeRPG.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG.Commands
{
    public interface ICollisionCommand
    {
        void Execute(CollisionInfo collision);
    }
}
