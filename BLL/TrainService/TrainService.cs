using System;
using System.Collections.Generic;
using System.Text;
using BLL.Entities;
using DAL.Entities; 

namespace BLL.TrainService
{
    public class TrainService : ITrainService
    {
        IReadWriteService<Train, TrainDAL> readWrite; 

        public TrainService(IReadWriteService<Train, TrainDAL> readWrite)
        {
            this.readWrite = readWrite; 
        }

        public void add(ulong train, string dispatch, string destination, DateTime departure)
        {
            Train added = new Train(train, dispatch, destination, departure);
            List<Train> trains = readWrite.ReadData();
            trains.Add(added);
            readWrite.WriteData(trains);
        }

        public void delete(ulong train)
        {
            List<Train> trains = readWrite.ReadData();
            trains.Remove(trains.Find(x => x.trainNumber == train));
        }

        public String getAllTrains()
        {
            List<Train> trains = readWrite.ReadData();
            StringBuilder stringBuilder = new StringBuilder();

            foreach(Train train in trains)
            {
                stringBuilder.Append(train.ToString()); 
            }

            return stringBuilder.ToString();
        }

        public String getSingleTrain(ulong train)
        {
            List<Train> trains = readWrite.ReadData();
            Train single = trains.Find(x => x.trainNumber == train);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(single.trainNumber);
            stringBuilder.Append(single.departure); 
            return stringBuilder.ToString(); 
        }


        //public bool trainExists(ulong train)
        //{
        //    List<Train> trains = readWrite.ReadData();
        //    Train single = trains.Find(x => x.trainNumber == train);
        //    return single != null; 
        //}
    }
}
