using System;

namespace DAL.Entities
{
    [Serializable]
    public class TrainDAL
    {
        public ulong trainNumber;
        public string dispatch;
        public string destination;
        public DateTime departure;

        public TrainDAL() { }

        public TrainDAL(ulong train, string dispatch, string destination, DateTime departure)
        {
            trainNumber = train;
            this.dispatch = dispatch;
            this.destination = destination;
            this.departure = departure; 
        }

    }
}
