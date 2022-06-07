using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingOnShips
{
    public class Ship
    {

        public int Count { get; private set; }
        public Types Type { get; }
        public Sizes Size { get; }

        public Ship(Types type, Sizes size)
        {
            Type = type;
            Size = size;
        }

        public void Add(int count)
        {
            Count += count;
        }

        public bool CheckCount()
        {
            return Count < (int)Size;
        }

    }
}
