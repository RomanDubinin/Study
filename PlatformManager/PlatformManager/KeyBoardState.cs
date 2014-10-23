using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformManager
{
    public enum navigationKeys
    {
        Up,
        Down,
        Left,
        Right
    }

    class KeyBoardState
    {
        public bool[] state { get; private set; }

        public KeyBoardState(bool[] s)
        {
            state = new bool[s.Length];
            s.CopyTo(state, 0);
        }
    }
}
