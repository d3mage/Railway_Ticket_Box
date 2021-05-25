using PL;
using BLL;
using BLL.TrainService;
using BLL.CarService;
using DAL.Entities;
using BLL.Entities;
using DAL; 
using DAL.Provider; 

namespace RailwayTicketBox
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlProvider<TrainDAL> traimXML = new();
            DataContext<TrainDAL> trainContext = new(traimXML, "train.xml");
            ReadWriteService<Train, TrainDAL> trainRW = new(trainContext);
            TrainService trainService = new TrainService(trainRW);
            
            XmlProvider<CarDAL> carXML = new();
            DataContext<CarDAL> carContext = new(carXML, "car.xml");
            ReadWriteService<Car, CarDAL> carRW = new(carContext);
            CarService carService = new CarService(carRW);


            Menu menu = new Menu(trainService, carService); 
        }
    }
}
