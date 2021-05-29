using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    [Serializable]
    public class CarDAL
    {
        public ulong trainNumber;
        public ushort carNumber;
        public bool[] sitsTaken;

        public CarDAL() { }

        public CarDAL(ulong train, ushort car, bool[] sits = null)
        {
            trainNumber = train;
            carNumber = car;
            sitsTaken = sits ?? new bool[30];
        }
    }
}
