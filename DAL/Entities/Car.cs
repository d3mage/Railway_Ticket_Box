using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class Car
    {
        public ulong trainNumber { get; }
        public List<bool> sitsVacancy { get; set; }

        public Car() { }

        public Car(ulong train)
        {
            trainNumber = train; 
        }
    }
}
