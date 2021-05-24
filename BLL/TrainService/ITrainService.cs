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
        public String getAllTrains();
    }
}
