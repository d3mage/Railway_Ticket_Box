using System;

namespace BLL.Entities
{
    public class Train
    {
        public ulong trainNumber { get; }
        public DateTime departure { get; }

        public Train(ulong train, DateTime departure)
        {
            trainNumber = train;
            this.departure = departure; 
        }
    }
}
