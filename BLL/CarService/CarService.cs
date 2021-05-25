using System;
using System.Collections.Generic;
using System.Text;
using BLL.Entities;
using DAL.Entities;

namespace BLL.CarService
{
    public class CarService : ICarService
    {
        IReadWriteService<Car, CarDAL> readWrite;

        public CarService(IReadWriteService<Car, CarDAL> readWrite)
        {
            this.readWrite = readWrite;
        }

        public void add(ulong train, ushort car)
        {
            Car added = new Car(train, car);
            List<Car> cars = readWrite.ReadData();
            cars.Add(added);
            readWrite.WriteData(cars);
        }

        public void delete(ulong train, ushort car, bool isCarEmpty)
        {
            if (!isCarEmpty) throw new CarNotEmptyException(); 
            List<Car> cars = readWrite.ReadData();
            cars.Remove(cars.Find(x => x.trainNumber == train && x.carNumber == car));
            readWrite.WriteData(cars);
        }

        public void sitChangeState(ulong train, ushort car, int sit, bool isTaken)
        {
            List<Car> cars = readWrite.ReadData();
            Car current = cars.Find(x => x.trainNumber == train && x.carNumber == car);
            bool[] sits = current.sitsTaken;
            sits[sit] = isTaken;
            current.sitsTaken = sits;
            readWrite.WriteData(cars);
        }


        public String getCarVacantSits(ulong train, ushort car)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Car number: ");
            builder.Append(car);

            List<Car> cars = readWrite.ReadData();
            Car current = cars.Find(x => x.trainNumber == train && x.carNumber == car);
            bool[] sits = current.sitsTaken;
            for (int i = 0; i < sits.Length; ++i)
            {
                builder.Append("\nSit ");
                builder.Append(i + 1);
                builder.Append(sits[i] ? " Taken" : " Vacant");
            }

            return builder.ToString();
        }

        public String getTrainCars(ulong train)
        {
            StringBuilder builder = new StringBuilder();
            List<Car> cars = readWrite.ReadData();
            cars = cars.FindAll(x => x.trainNumber == train);
            foreach(Car car in cars)
            {
                builder.Append(car.carNumber);
                builder.Append("\n");
            }
            return builder.ToString(); 
        }
    }
}
