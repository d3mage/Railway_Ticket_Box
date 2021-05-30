using System;

namespace BLL.CarService
{
    public interface ICarService
    {
        public void add(ulong train, ushort car);
        public void delete(ulong train, ushort car, bool isCarEmpty);
        public void carExists(ulong train, ushort car, bool shouldExist);
        public bool isCarEmpty(ulong train, ushort car);
        public void sitChangeState(ulong train, ushort car, int sit, bool isTaken);
        public String getCarVacantSits(ulong train, ushort car);
        public String getTrainCars(ulong train);
        public String getPercentage(ulong train);
    }
}
