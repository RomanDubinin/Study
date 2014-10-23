using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    class TestMachine: IMachine
    {
        public void Up()
        {
            Console.WriteLine("UP");
        }

        public void Down()
        {
            Console.WriteLine("Down");
        }

        public void Left()
        {
            Console.WriteLine("Left");
        }

        public void Right()
        {
            Console.WriteLine("Right");
        }
    }
}
