using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    class VirtualMachine: IMachine
    {
        private const int startSpeed = 50;
        private int maxSpeed;
        private int sharpTurn;
        private int currentSpeed;
        private int currentDirection;
        private int deltaRotate;
        private int deltaSpeedUp;

        public VirtualMachine(int ms, int dr, int ds)
        {
            maxSpeed = ms;
            sharpTurn = ms;
            deltaRotate = dr;
            deltaSpeedUp = ds;
            currentDirection = 0;
            currentSpeed = 0;
        }

        private Tuple<int, int> GetWheelsSpeed() // returns left and right wheel speed
        {
            if (currentDirection < 0)
                return Tuple.Create(currentSpeed - currentDirection, currentSpeed + currentDirection);
            if (currentDirection > 0)
                return Tuple.Create(currentSpeed + currentDirection, currentSpeed - currentDirection);
            else
                return Tuple.Create(currentSpeed, currentSpeed);
        }

        private void Up()
        {
            currentSpeed = Math.Min(maxSpeed / 2, currentSpeed + deltaSpeedUp);
        }

        private void Down()
        {
            currentSpeed = Math.Max(-maxSpeed / 2, currentSpeed - deltaSpeedUp);
        }

        private void Left()
        {
            currentDirection = Math.Max(-sharpTurn, currentDirection - deltaRotate);
        }

        private void Right()
        {
            currentDirection = Math.Min(sharpTurn, currentDirection + deltaRotate);
        }

        public void ChangeVelocityVector(KeyBoardState keys)
        {
            if (keys.state[(int)navigationKeys.Up] && !keys.state[(int)navigationKeys.Down])
                Up();
            else if (keys.state[(int)navigationKeys.Down] && !keys.state[(int)navigationKeys.Up])
                Down();
            else 
                //TODO: скорость к 0 speed = (abs(speed)-delta)
                ;

            if (keys.state[(int)navigationKeys.Left] && !keys.state[(int)navigationKeys.Right])
                Left();
            else if (keys.state[(int)navigationKeys.Right] && !keys.state[(int)navigationKeys.Left])
                Right();
            else
                //TODO: dir к 0
                ;

            Tuple<int, int> wheels = GetWheelsSpeed();

            Console.WriteLine(wheels.Item1 + "    " + wheels.Item2);
        }
    }
}
