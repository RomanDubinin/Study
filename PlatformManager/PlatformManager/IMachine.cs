using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    interface IMachine
    {
        void Up();
        void Down();
        void Left();
        void Right();
    }
}
