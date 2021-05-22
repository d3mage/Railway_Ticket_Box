using System;

namespace BLL.Entities
{
    public class Train
    {
        public ulong trainNumber { get; }
        public string dispatch { get; }
        public string destination { get; }
        public DateTime departure { get; }

        public Train(ulong train, string dispatch, string destination, DateTime departure)
        {
            trainNumber = train;
            this.dispatch = dispatch;
            this.destination = destination;
            this.departure = departure; 
        }

        public override string ToString()
        {
            return trainNumber + " " + departure; 
        }
    }
}
