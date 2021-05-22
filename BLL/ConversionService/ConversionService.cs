using System.Collections.Generic;
using BLL.Entities;
using DAL.Entities; 

namespace BLL.ConversionService
{
    static class ConversionService<T,Y> 
    {
        public static void conversion(List<T> convertive, List<Y> converted)
        {
            if(convertive is List<Train> trains && converted is List<TrainDAL> trainsDAL)
            {
                conversionTrainToDAL(trains, trainsDAL); 
            }
            else if(convertive is List<TrainDAL> trains1DAL  && converted is List<Train> trains1)
            {
                conversionDALToTrain(trains1DAL, trains1);
            }
        }

        public static void  conversionTrainToDAL(List<Train> trains, List<TrainDAL> trainsDAL)
        {
            foreach(Train train in trains)
            {
                trainsDAL.Add(new TrainDAL(train.trainNumber, train.dispatch, train.destination, train.departure));
            }
        }
        public static void  conversionDALToTrain(List<TrainDAL> trainsDAL, List<Train> trains)
        {
            foreach(TrainDAL train in trainsDAL)
            {
                trains.Add(new Train(train.trainNumber, train.dispatch, train.destination, train.departure));
            }
        }
    }
}
