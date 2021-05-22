using System;
using System.Collections.Generic;
using BLL.Entities;
using DAL.Entities;

namespace Tests
{
    static class testData
    {
        public static String GetIntConnectionString() => "test.xml";
        public static String GetTrainConnectionString() => "testTrain.xml";
        public static String GetInvalidConnectionString() => "corrupted.xml";

        public static List<int> GetIntList()
        {
            List<int> toReturn = new List<int>() { 1, 2, 3 };
            return toReturn;
        }

        public static List<Train> GetTrainList()
        {
            Train train1 = new(202019, DateTime.Today);
            Train train2 = new(2021019, DateTime.Today);
            List<Train> toReturn = new List<Train>() { train1, train2 };
            return toReturn;
        }
        public static List<TrainDAL> GetTrainDALList()
        {
            TrainDAL train1 = new(202019, DateTime.Today);
            TrainDAL train2 = new(2021019, DateTime.Today);
            List<TrainDAL> toReturn = new List<TrainDAL>() { train1, train2 };
            return toReturn;
        }
    }
}
