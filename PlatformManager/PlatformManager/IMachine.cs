using System;

namespace PlatformManager
{
	internal interface IMachine
	{
		void ChangeVelocityVector(KeyBoardState state);
		Tuple<int, int> GetWheelsSpeed();
	}
}
