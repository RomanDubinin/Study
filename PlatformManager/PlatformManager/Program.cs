using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace PlatformManager
{
    class Program
    {
        private static IMachine machine;
        private static bool[] navigationKeyPressed = new bool[4];
        private static Timer timer;
        private static SerialPort port;
        private static Form form;

        private static string IntToValidString(int val)
        {
            
            StringBuilder strVal = new StringBuilder();
            if (val < 0)
                strVal.Append("-");
            else
                strVal.Append("+");
            strVal.Append(Math.Abs(val).ToString("D" + 3));
            return strVal.ToString();
        }

        private static void form_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
                navigationKeyPressed[(int)navigationKeys.Up] = true;
            if (e.KeyCode == Keys.Down)
                navigationKeyPressed[(int)navigationKeys.Down] = true;
            if (e.KeyCode == Keys.Left)
                navigationKeyPressed[(int)navigationKeys.Left] = true;
            if (e.KeyCode == Keys.Right)
                navigationKeyPressed[(int)navigationKeys.Right] = true;
        }

        private static void form_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
                navigationKeyPressed[(int)navigationKeys.Up] = false;
            if (e.KeyCode == Keys.Down)
                navigationKeyPressed[(int)navigationKeys.Down] = false;
            if (e.KeyCode == Keys.Left)
                navigationKeyPressed[(int)navigationKeys.Left] = false;
            if (e.KeyCode == Keys.Right)
                navigationKeyPressed[(int)navigationKeys.Right] = false;
        }

        private static void PerformAction(object sender, EventArgs e)
        {
            Console.Clear();
            machine.ChangeVelocityVector(new KeyBoardState(navigationKeyPressed));
            Tuple<int, int> wheels = machine.GetWheelsSpeed();

            StringBuilder data = new StringBuilder();
            data.Append("ride");
            data.Append(IntToValidString(wheels.Item1));
            data.Append(IntToValidString(wheels.Item2));

            Console.WriteLine(data);
            port.Write(data.ToString());
        }

        static void Main(string[] args)
        {

            form = new Form();
            form.KeyDown += form_KeyDown;
            form.KeyUp += form_KeyUp;

            port = new SerialPort("COM22", 115200);
            port.Open();
            machine = new Machine(255, 20, 20);

            //new Action(PerformAction).BeginInvoke(null, null);
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(PerformAction);
            timer.Start();
            Application.Run(form);

        }
    }
}