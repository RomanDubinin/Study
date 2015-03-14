using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlatformManager
{
	internal class Program
	{
		private static IMachine PlaneMachine;
		private static IMachine LadderMashine;
		private static readonly bool[] NavigationKeyPressed = new bool[8];
		private static Timer Timer;
		private static SerialPort Port;
		private static Form Form;

		private static string IntToValidString(int val)
		{
			var strVal = new StringBuilder();
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
				NavigationKeyPressed[(int) NavigationKeys.PlaneUp] = true;
			if (e.KeyCode == Keys.Down)
				NavigationKeyPressed[(int) NavigationKeys.PlaneDown] = true;
			if (e.KeyCode == Keys.Left)
				NavigationKeyPressed[(int) NavigationKeys.PlaneLeft] = true;
			if (e.KeyCode == Keys.Right)
				NavigationKeyPressed[(int) NavigationKeys.PlaneRight] = true;

			if (e.KeyCode == Keys.W)
				NavigationKeyPressed[(int)NavigationKeys.LadderUp] = true;
			if (e.KeyCode == Keys.S)
				NavigationKeyPressed[(int)NavigationKeys.LadderDown] = true;
			if (e.KeyCode == Keys.A)
				NavigationKeyPressed[(int)NavigationKeys.LadderLeft] = true;
			if (e.KeyCode == Keys.D)
				NavigationKeyPressed[(int)NavigationKeys.LadderRight] = true;
		}

		private static void form_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
				NavigationKeyPressed[(int) NavigationKeys.PlaneUp] = false;
			if (e.KeyCode == Keys.Down)
				NavigationKeyPressed[(int) NavigationKeys.PlaneDown] = false;
			if (e.KeyCode == Keys.Left)
				NavigationKeyPressed[(int) NavigationKeys.PlaneLeft] = false;
			if (e.KeyCode == Keys.Right)
				NavigationKeyPressed[(int) NavigationKeys.PlaneRight] = false;

			if (e.KeyCode == Keys.W)
				NavigationKeyPressed[(int)NavigationKeys.LadderUp] = false;
			if (e.KeyCode == Keys.S)
				NavigationKeyPressed[(int)NavigationKeys.LadderDown] = false;
			if (e.KeyCode == Keys.A)
				NavigationKeyPressed[(int)NavigationKeys.LadderLeft] = false;
			if (e.KeyCode == Keys.D)
				NavigationKeyPressed[(int)NavigationKeys.LadderRight] = false;
		}

		private static void PerformAction(object sender, EventArgs e)
		{
			Console.Clear();
			PlaneMachine.ChangeVelocityVector(new KeyBoardState(NavigationKeyPressed.Take(4).ToArray()));
			LadderMashine.ChangeVelocityVector(new KeyBoardState(NavigationKeyPressed.Skip(4).ToArray()));

			var planeMachineWheels = PlaneMachine.GetWheelsSpeed();
			var ladderMachineWheels = LadderMashine.GetWheelsSpeed();

			var planeData = new StringBuilder();
			planeData.Append("ride");
			planeData.Append(IntToValidString(planeMachineWheels.Item1));
			planeData.Append(IntToValidString(planeMachineWheels.Item2));

			var ladderData = new StringBuilder();
			ladderData.Append("ladder");
			ladderData.Append(IntToValidString(ladderMachineWheels.Item1));
			ladderData.Append(IntToValidString(ladderMachineWheels.Item2));

			Console.WriteLine(planeData);
			Console.WriteLine(ladderData);
			//Port.Write(planeData.ToString());
			//Port.Write(ladderData.ToString());
		}

		private static void Main(string[] args)
		{
			Form = new Form();
			Form.KeyDown += form_KeyDown;
			Form.KeyUp += form_KeyUp;

			//Port = new SerialPort("COM3", 115200);
			//Port.Open();
			PlaneMachine = new Machine(255, 20, 20);
			LadderMashine = new Machine(255, 20, 20);

			Timer = new Timer();
			Timer.Interval = 100;
			Timer.Tick += PerformAction;
			Timer.Start();
			Application.Run(Form);
		}
	}
}