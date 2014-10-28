using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    class Machine : IMachine
    {
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
            deltaDirectionFalling = deltaDirectionUp ;
            deltaBreaking = deltaDirectionUp * 2;
        }

        public Tuple<int, int> GetWheelsSpeed() // returns left and right wheel speed
        {
            if(currentDirection == 0)
                return Tuple.Create(currentSpeed, currentSpeed);
            else if(currentSpeed == 0)
                return Tuple.Create(currentDirection, -currentDirection);

            if (currentSpeed > 0)
            {
                return Tuple.Create(
                    Math.Min(255, currentSpeed + currentDirection),
                    Math.Min(255, currentSpeed - currentDirection));
            }
            else
                return Tuple.Create(
                    Math.Max(-255, currentSpeed - currentDirection),
                    Math.Max(-255, currentSpeed + currentDirection));
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

        private void SpeedFalling()
        {
            if (Math.Abs(currentSpeed) < deltaSpeedFalling)
                currentSpeed = 0;
            if (currentSpeed != 0)
                currentSpeed = currentSpeed - (deltaSpeedFalling * Math.Abs(currentSpeed) / currentSpeed);
        }

        private void DirectionFalling()
        {
            if (Math.Abs(currentDirection) < deltaDirectionFalling)
                currentDirection = 0;
            if (currentDirection != 0)
                currentDirection = currentDirection - (deltaDirectionFalling * Math.Abs(currentDirection) / currentDirection);
        }

        public void ChangeVelocityVector(KeyBoardState keys)
        {
            if (keys.state[(int)navigationKeys.Up] && !keys.state[(int)navigationKeys.Down])
                Up();
            else if (keys.state[(int)navigationKeys.Down] && !keys.state[(int)navigationKeys.Up])
                Down();
            else
                SpeedFalling();

            if (keys.state[(int)navigationKeys.Left] && !keys.state[(int)navigationKeys.Right])
                Left();
            else if (keys.state[(int)navigationKeys.Right] && !keys.state[(int)navigationKeys.Left])
                Right();
            else
                DirectionFalling();
        }
    }
}
