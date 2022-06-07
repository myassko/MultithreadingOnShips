using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingOnShips
{
    public class PierLoader
    {
        public Tunnel Tunnel { get; }

        public Types ShipType { get; } //какой тип корабля может загружать причал

        public bool IsAlive { get; set; } = true;


        public PierLoader(Tunnel tunnel, Types type)
        {
            Tunnel = tunnel;
            ShipType = type;
        }

        public void Start()
        {
            while (IsAlive)
            {
                try
                {
                    Thread.Sleep(1000);// дает фору генератору
                    Ship ship = Tunnel.GetShip(ShipType);
                    var th = Thread.CurrentThread;
                    if (ship != null)
                    {
                        while (ship.CheckCount())
                        {
                            Thread.Sleep(100);
                            ship.Add(10);
                            Console.WriteLine($"Идет загрузка {ship.Count}-{th.Name}\n");
                        }
                        Console.WriteLine($"Корабль завершил погрузку\nТип-{ship.Type}-Размер-{ship.Size} Название потока-{th.Name}\n\n");
                    }
                }
                catch
                {
                }
            }
        }
        public void Break()
        {
            IsAlive = false;
        }
    }
}
