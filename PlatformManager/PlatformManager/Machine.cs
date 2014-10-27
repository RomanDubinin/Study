using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    class Machine : IMachine
    {
        private const int startSpeed = 50;
        private int maxSpeed;
        private int sharpTurn;
        private int currentSpeed;
        private int currentDirection;
        private int deltaDirectionUp;
        private int deltaSpeedUp;
        private int deltaDirectionFalling;
        private int deltaSpeedFalling;
        private int deltaBreaking;

        public Machine(int ms, int dr, int ds)
        {
            maxSpeed = ms;
            sharpTurn = ms;
            deltaDirectionUp = dr;
            deltaSpeedUp = ds;
            currentDirection = 0;
            currentSpeed = 0;
            deltaSpeedFalling = deltaSpeedUp / 5;
            deltaDirectionFalling = deltaDirectionUp / 2;
            deltaBreaking = deltaDirectionUp * 2;
        }

        public Tuple<int, int> GetWheelsSpeed() // returns left and right wheel speed
        {
            if(currentDirection == 0)
                return Tuple.Create(currentSpeed, currentSpeed);
            else
                return Tuple.Create(currentSpeed + currentDirection, currentSpeed - currentDirection);
        }

        private void Up()
        {
            if(currentSpeed >= 0)
                currentSpeed = Math.Min(maxSpeed / 2, currentSpeed + deltaSpeedUp);
            else
                currentSpeed = Math.Min(maxSpeed / 2, currentSpeed + deltaBreaking);
        }

        private void Down()
        {
            if(currentSpeed <= 0)
                currentSpeed = Math.Max(-maxSpeed / 2, currentSpeed - deltaSpeedUp);
            else
                currentSpeed = Math.Max(-maxSpeed / 2, currentSpeed - deltaBreaking);
        }

        private void Left()
        {
            if(currentDirection <=0)
                currentDirection = Math.Max(-sharpTurn, currentDirection - deltaDirectionUp);
            else
                currentDirection = Math.Max(-sharpTurn, currentDirection - deltaBreaking);
        }

        private void Right()
        {
            if(currentDirection >= 0)
                currentDirection = Math.Min(sharpTurn, currentDirection + deltaDirectionUp);
            else
                currentDirection = Math.Min(sharpTurn, currentDirection + deltaBreaking);
        }

        public void ChangeVelocityVector(KeyBoardState keys)
        {
            Console.WriteLine(deltaSpeedFalling);
            Console.WriteLine(deltaDirectionFalling);
            if (keys.state[(int)navigationKeys.Up] && !keys.state[(int)navigationKeys.Down])
                Up();
            else if (keys.state[(int)navigationKeys.Down] && !keys.state[(int)navigationKeys.Up])
                Down();
            else
            {
                if (currentSpeed != 0)
                    currentSpeed = currentSpeed - (deltaSpeedFalling * Math.Abs(currentSpeed) / currentSpeed);
            }

            if (keys.state[(int)navigationKeys.Left] && !keys.state[(int)navigationKeys.Right])
                Left();
            else if (keys.state[(int)navigationKeys.Right] && !keys.state[(int)navigationKeys.Left])
                Right();
            else
            {
                if (currentDirection != 0)
                    currentDirection = currentDirection - (deltaDirectionFalling * Math.Abs(currentDirection) / currentDirection);
            }
        }
    }
}
