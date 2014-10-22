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
        private int currentSpeed;
        private double currentDirection;
        private double epsilon;
        private double rotateCoef;
        private double speedUpCoef;

        VirtualMachine(int ms, double e, double rc, double sc)
        {
            maxSpeed = ms;
            epsilon = e;
            rotateCoef = rc;
            speedUpCoef = sc;
            currentDirection = 0;
            currentSpeed = 0;
        }

        private Tuple<int, int> GetWheelsSpeedV1(int speed, double direction) // returns left and right wheel speed
        {
            int newSpeed = (int)(1 - Math.Abs(direction)) * maxSpeed;
            if (direction < 0)
                return Tuple.Create(newSpeed, currentSpeed - newSpeed);
            if (direction > 0)
                return Tuple.Create(currentSpeed - newSpeed, newSpeed);
            else
                return Tuple.Create(currentSpeed / 2, currentSpeed / 2);
        }

        private Tuple<int, int> GetWheelsSpeedV2(int speed, double direction) // returns left and right wheel speed
        {
            int newSpeed = (int)(1 - Math.Abs(direction)) * maxSpeed;
            if (direction < 0)
                return Tuple.Create(newSpeed, currentSpeed);
            if (direction > 0)
                return Tuple.Create(currentSpeed, newSpeed);
            else
                return Tuple.Create(currentSpeed, currentSpeed);
        }

        public void Up()
        {
            if(Math.Abs(currentSpeed) < epsilon)
                currentSpeed = startSpeed;

            else if(currentSpeed < 0)
                currentSpeed = (int)(currentSpeed / speedUpCoef);

            else if(currentSpeed > 0)
                currentSpeed = Math.Min(255, (int)(currentSpeed * speedUpCoef));

            if (Math.Abs(currentDirection) < epsilon)
                currentDirection = 0;
            else
                currentDirection /= rotateCoef;


            Tuple<int, int> wheelsSpeed = GetWheelsSpeedV1(currentSpeed, currentDirection);

            Console.WriteLine("%d   %d", wheelsSpeed.Item1, wheelsSpeed.Item2);
        }


    }
}
