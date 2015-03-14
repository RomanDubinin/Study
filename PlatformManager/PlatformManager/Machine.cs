using System;

namespace PlatformManager
{
	internal class Machine : IMachine
	{
		private readonly int MaxSpeed;
		private readonly int SharpTurn;
		private int CurrentSpeed;
		private int CurrentDirection;
		private readonly int DeltaDirectionUp;
		private readonly int DeltaSpeedUp;
		private readonly int DeltaDirectionFalling;
		private readonly int DeltaSpeedFalling;
		private readonly int DeltaBreaking;

		public Machine(int ms, int dr, int ds)
		{
			MaxSpeed = ms;
			SharpTurn = ms;
			DeltaDirectionUp = dr;
			DeltaSpeedUp = ds;
			CurrentDirection = 0;
			CurrentSpeed = 0;
			DeltaSpeedFalling = DeltaSpeedUp/5;
			DeltaDirectionFalling = DeltaDirectionUp;
			DeltaBreaking = DeltaDirectionUp*2;
		}

		public Tuple<int, int> GetWheelsSpeed() // returns left and right wheel speed
		{
			if (CurrentDirection == 0)
				return Tuple.Create(CurrentSpeed, CurrentSpeed);
			if (CurrentSpeed == 0)
				return Tuple.Create(CurrentDirection, -CurrentDirection);

			if (CurrentSpeed > 0)
			{
				return Tuple.Create(
					Math.Min(255, CurrentSpeed + CurrentDirection),
					Math.Min(255, CurrentSpeed - CurrentDirection));
			}
			return Tuple.Create(
				Math.Max(-255, CurrentSpeed - CurrentDirection),
				Math.Max(-255, CurrentSpeed + CurrentDirection));
		}

		private void Up()
		{
			if (CurrentSpeed >= 0)
				CurrentSpeed = Math.Min(MaxSpeed/2, CurrentSpeed + DeltaSpeedUp);
			else
				CurrentSpeed = Math.Min(MaxSpeed/2, CurrentSpeed + DeltaBreaking);
		}

		private void Down()
		{
			if (CurrentSpeed <= 0)
				CurrentSpeed = Math.Max(-MaxSpeed/2, CurrentSpeed - DeltaSpeedUp);
			else
				CurrentSpeed = Math.Max(-MaxSpeed/2, CurrentSpeed - DeltaBreaking);
		}

		private void Left()
		{
			if (CurrentDirection <= 0)
				CurrentDirection = Math.Max(-SharpTurn, CurrentDirection - DeltaDirectionUp);
			else
				CurrentDirection = Math.Max(-SharpTurn, CurrentDirection - DeltaBreaking);
		}

		private void Right()
		{
			if (CurrentDirection >= 0)
				CurrentDirection = Math.Min(SharpTurn, CurrentDirection + DeltaDirectionUp);
			else
				CurrentDirection = Math.Min(SharpTurn, CurrentDirection + DeltaBreaking);
		}

		private void SpeedFalling()
		{
			if (Math.Abs(CurrentSpeed) < DeltaSpeedFalling)
				CurrentSpeed = 0;
			if (CurrentSpeed != 0)
				CurrentSpeed = CurrentSpeed - (DeltaSpeedFalling*Math.Abs(CurrentSpeed)/CurrentSpeed);
		}

		private void DirectionFalling()
		{
			if (Math.Abs(CurrentDirection) < DeltaDirectionFalling)
				CurrentDirection = 0;
			if (CurrentDirection != 0)
				CurrentDirection = CurrentDirection - (DeltaDirectionFalling*Math.Abs(CurrentDirection)/CurrentDirection);
		}

		public void ChangeVelocityVector(KeyBoardState keys)
		{
			if (keys.state[(int) NavigationKeys.PlaneUp] && !keys.state[(int) NavigationKeys.PlaneDown])
				Up();
			else if (keys.state[(int) NavigationKeys.PlaneDown] && !keys.state[(int) NavigationKeys.PlaneUp])
				Down();
			else
				SpeedFalling();

			if (keys.state[(int) NavigationKeys.PlaneLeft] && !keys.state[(int) NavigationKeys.PlaneRight])
				Left();
			else if (keys.state[(int) NavigationKeys.PlaneRight] && !keys.state[(int) NavigationKeys.PlaneLeft])
				Right();
			else
				DirectionFalling();
		}
	}
}