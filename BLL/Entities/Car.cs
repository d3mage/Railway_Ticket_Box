using System.Collections.Generic;

namespace BLL.Entities
{
    public class Car
    {
        public ulong trainNumber { get; }
        public ushort carNumber { get; }
        public List<bool> sitsVacancy { get; set; }

        public Car(ulong train)
        {
            trainNumber = train; 
        }
    }
}
