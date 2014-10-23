using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    class TestMachine: IMachine
    {
        private void Up()
        {
            Console.WriteLine("UP");
        }

        private void Down()
        {
            Console.WriteLine("Down");
        }

        private void Left()
        {
            Console.WriteLine("Left");
        }

        private void Right()
        {
            Console.WriteLine("Right");
        }

        public void ChangeVelocityVector(KeyBoardState keys)
        {
            if (keys.state[0])
                Up();
            if (keys.state[1])
                Down();
            if (keys.state[2])
                Left();
            if (keys.state[3])
                Right();
        }
    }
}
