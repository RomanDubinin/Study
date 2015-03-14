namespace PlatformManager
{
	public enum NavigationKeys
	{
		PlaneUp,
		PlaneDown,
		PlaneLeft,
		PlaneRight,
		LadderUp,
		LadderDown,
		LadderLeft,
		LadderRight
	}

	internal class KeyBoardState
	{
		public bool[] state { get; private set; }

		public KeyBoardState(bool[] s)
		{
			state = new bool[s.Length];
			s.CopyTo(state, 0);
		}
	}
}
