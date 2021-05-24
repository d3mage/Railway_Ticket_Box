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
            List<int> toReturn = new() { 1, 2, 3 };
            return toReturn;
        }

        public static List<Train> GetTrainList()
        {
            Train train1 = new(202019, "Kyiv", "Odesa", DateTime.Today);
            Train train2 = new(2021019, "Kyiv", "Kharkiv", DateTime.Today);
            List<Train> toReturn = new() { train1, train2 };
            return toReturn;
        }

        public static Train GetTrain()
        {
            return new Train(129382, "Kyiv", "Mykolaiv", DateTime.Today);
        }

        public static List<TrainDAL> GetTrainDALList()
        {
            TrainDAL train1 = new(202019, "Kyiv", "Odesa", DateTime.Today);
            TrainDAL train2 = new(2021019, "Kyiv", "Kharkiv", DateTime.Today);
            List<TrainDAL> toReturn = new() { train1, train2 };
            return toReturn;
        }
    }
}
