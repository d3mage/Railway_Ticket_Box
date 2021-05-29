using System.Collections.Generic;

namespace BLL.Entities
{
    public class Car
    {
        public ulong trainNumber { get; }
        public ushort carNumber { get; }
        public bool[] sitsTaken { get; set; }

        public Car(ulong train, ushort car, bool[] sits = null)
        {
            trainNumber = train;
            carNumber = car;
            sitsTaken = sits ?? new bool[30];
        }
    }
}
