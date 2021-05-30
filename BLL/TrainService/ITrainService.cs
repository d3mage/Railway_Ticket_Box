using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TrainService
{
    public interface ITrainService
    {
        public void add(ulong train, string dispatch, string destination, DateTime departure);
        public void delete(ulong train);
        public void trainExists(ulong train, bool shouldExist);
        public String getAllTrains();
        public String getSingleTrain(ulong train);
        public String searchByKeyword(string keyword);
    }
}
