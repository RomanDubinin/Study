using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    interface IMachine
    {
        void ChangeVelocityVector(KeyBoardState state);
        Tuple<int, int> GetWheelsSpeed();
    }
}
