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

        public bool isCarEmpty(ulong train, ushort car)
        {
            List<Car> cars = readWrite.ReadData();
            Car single = cars.Find(x => x.trainNumber == train);
            foreach(bool taken in single.sitsTaken)
            {
                if (taken) return false;
            }
            return true;
        }

        public void carExists(ulong train, ushort car, bool shouldExist)
        {
            List<Car> cars = readWrite.ReadData();
            Car single = cars.Find(x => x.trainNumber == train);
            if (single == null && shouldExist == true) throw new CarNumberException();
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

        public String getPercentage(ulong train)
        {
            StringBuilder builder = new StringBuilder();
            List<Car> cars = readWrite.ReadData();
            cars = cars.FindAll(x => x.trainNumber == train);
            foreach (Car car in cars)
            {
                builder.Append(car.carNumber);
                builder.Append(calculateTakenPercentage(car.sitsTaken));
                builder.Append("%\n");
            }
            return builder.ToString();
        }

        private String calculateTakenPercentage(bool[] sits)
        {
            int taken = 0; 
            foreach(bool isTaken in sits)
            {
                if (isTaken) taken++;
            }
            return " " + (taken * 100 / 30).ToString();
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
