using System;
using System.Collections.Generic;
using DAL.Entities; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests
{
    static class testData
    {
        public static String GetConnectionString()
        {
            return "test.xml";
        }

        public static String GetInvalidConnectionString()
        {
            return "corrupted.xml";
        }

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
    }
}
