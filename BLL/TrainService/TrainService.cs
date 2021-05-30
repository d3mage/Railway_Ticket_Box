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
            readWrite.WriteData(trains);
        }

        public void trainExists(ulong train, bool shouldExist)
        {
            List<Train> trains = readWrite.ReadData();
            Train single = trains.Find(x => x.trainNumber == train);
            if (single == null && shouldExist == true) throw new TrainNumberException(); 
        }

        public String getAllTrains()
        {
            List<Train> trains = readWrite.ReadData();
            StringBuilder stringBuilder = new StringBuilder();

            foreach(Train train in trains)
            {
                stringBuilder.Append(train.ToString());
                stringBuilder.Append("\n");
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

        public String searchByKeyword(string keyword)
        {
            List<Train> trains = readWrite.ReadData();
            trains = trains.FindAll(x => x.dispatch.Equals(keyword) || x.destination.Equals(keyword));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (Train train in trains)
            {
                stringBuilder.Append(train.ToString());
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
