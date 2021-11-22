using System;
using System.Collections.Generic;
using System.Text;

namespace LinkDevice
{
    public class LinkStation
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Reach { get; private set; }

        public LinkStation(int x, int y, int reach)
        {
            X = x;
            Y = y;
            Reach = reach;
        }
    }
}