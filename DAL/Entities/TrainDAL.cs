using System;

namespace DAL.Entities
{
    [Serializable]
    public class TrainDAL
    {
        public ulong trainNumber;
        public DateTime departure;

        public TrainDAL() { }

        public TrainDAL(ulong train, DateTime departure)
        {
            trainNumber = train;
            this.departure = departure; 
        }

    }
}
