using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace PlatformManager
{
    class Program
    {
        private static IMachine machine = new TestMachine();
        public delegate void Function();
        private static bool[] navigationKeyPressed = new bool[4];
 
        private static void form_KeyDown(object sender, KeyEventArgs e)
        {
 
            if (e.KeyCode == Keys.Up)
                navigationKeyPressed[(int)navigationKeys.Up]  = true;
            if (e.KeyCode == Keys.Down)
                navigationKeyPressed[(int)navigationKeys.Down] = true;
            if (e.KeyCode == Keys.Left)
                navigationKeyPressed[(int)navigationKeys.Left] = true;
            if (e.KeyCode == Keys.Right)
                navigationKeyPressed[(int)navigationKeys.Right] = true;
 
            //Console.WriteLine(navigationKeyPressed
            //    .Where(x => x == true)
            //    .Count());
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
 
        private static void PerformAction()
        {
            while (true)
            {
                Console.Clear();
                machine.ChangeVelocityVector(new KeyBoardState(navigationKeyPressed));
            }
        }
 
        static void Main(string[] args)
        {
 
            Form form = new Form();
            form.KeyDown += form_KeyDown;
            form.KeyUp += form_KeyUp;
 
            new Action(PerformAction).BeginInvoke(null, null);
            Application.Run(form);
 
        }
    }
}