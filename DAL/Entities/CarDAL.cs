using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    [Serializable]
    public class CarDAL
    {
        public ulong trainNumber;
        public ushort carNumber;
        public List<bool> sitsVacancy;

        public CarDAL() { }

        public CarDAL(ulong train)
        {
            trainNumber = train; 
        }
    }
}
