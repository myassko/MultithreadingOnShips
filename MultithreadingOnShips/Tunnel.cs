using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingOnShips
{
    public class Tunnel
    {
        public object block = new object();// для синхронизации потоков
        public List<Ship> tunnel { get; }
        public int MinShipsInTunnel { get; } = 0;
        public int MaxShipsInTunnel { get; } = 5;

        private int shipCounter = 0;//в данный момент


        public Tunnel()
        {
            tunnel = new List<Ship>();//выделяем память 
        }


        public bool AddShip(Ship ship)
        {
            try
            {
                lock (block)
                {
                    var th = Thread.CurrentThread;//поток ,который работает на данный момент
                    if (shipCounter < MaxShipsInTunnel)
                    {
                        Console.WriteLine($"Корабль прибыл в тоннель\nТип-{ship.Type} Размер-{ship.Size} Название потока-{th.Name}\n\n");
                        Monitor.PulseAll(block);
                        tunnel.Add(ship);
                        shipCounter += 1;
                    }
                    else
                    {
                        Console.WriteLine($"В тоннеле нет места\nНазвание потока-{th.Name}\n\n");
                        Monitor.Wait(block);
                        return false;
                    }
                }
            }
            catch
            {

            }
            return true;
        }
        public Ship GetShip(Types type)
        {
            try
            {
                lock (block)
                {
                    var th = Thread.CurrentThread;
                    if (shipCounter > MinShipsInTunnel)
                    {
                        Monitor.PulseAll(block);
                        foreach (var sh in tunnel)
                        {
                            if (sh.Type == type)//ищем нужный тип корабля
                            {
                                shipCounter -= 1;
                                Console.WriteLine($"Корабль отправился к причалу\nТип-{sh.Type} Размер-{sh.Size} Название потока-{th.Name}\n\n");
                                tunnel.Remove(sh);

                                return sh;
                            }
                        }
                    }
                    else
                        Console.WriteLine($"В данный момент корабли в тоннеле отсутствуют\nНазвание потока-{th.Name}\n\n");
                    Monitor.Wait(block);
                }
            }
            catch
            {

            }
            return null;
        }
    }
}
