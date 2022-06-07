using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingOnShips
{
    public class ShipGenerator
    {
        public object block = new object();//выделение памяти на заглушку
        public Tunnel Tunnel { get; }
        public int ShipCount { get; private set; }//сколько  кораблей всего нужно создать

        private int shipWasCreated = 0;//сколько на данный момент создано кораблей

        public ShipGenerator(Tunnel tunnel, int shipCount)
        {
            Tunnel = tunnel;
            ShipCount = shipCount;

        }
        public void Start()
        {
            while (shipWasCreated < ShipCount)
            {

                var ship = new Ship(CreateType(), CreateSize());
                if (Tunnel.AddShip(ship))
                {
                    shipWasCreated++;
                    try
                    {
                        Thread.Sleep(100);
                    }
                    catch
                    {
                    }
                }

            }

        }

        static public Sizes CreateSize()
        {
            var sizes = Enum.GetValues(typeof(Sizes));
            return (Sizes)sizes.GetValue(new Random().Next(sizes.Length));
        }

        static public Types CreateType()
        {
            var types = Enum.GetValues(typeof(Types));
            return (Types)types.GetValue(new Random().Next(types.Length));
        }
    }
}

