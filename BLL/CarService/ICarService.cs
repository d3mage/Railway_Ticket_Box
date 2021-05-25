using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CarService
{
    public interface ICarService
    {
        public void add(ulong train, ushort car);
        public void delete(ulong train, ushort car, bool isCarEmpty);
        public void sitChangeState(ulong train, ushort car, int sit, bool isTaken);
        public String getCarVacantSits(ulong train, ushort car);
        public String getTrainCars(ulong train);
    }
}
