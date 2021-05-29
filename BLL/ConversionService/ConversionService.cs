using System.Collections.Generic;
using BLL.Entities;
using DAL.Entities; 

namespace BLL.ConversionService
{
    public static class ConversionService<T,Y> 
    {
        public static void conversion(List<T> convertive, List<Y> converted)
        {
            if (convertive is List<Train> trains && converted is List<TrainDAL> trainsDAL)
            {
                conversionTrainToDAL(trains, trainsDAL);
            }
            else if (convertive is List<TrainDAL> trains1DAL && converted is List<Train> trains1)
            {
                conversionDALToTrain(trains1DAL, trains1);
            }
            else if (convertive is List<Car> cars && converted is List<CarDAL> carsDAL)
            {
                conversionCarToDAL(cars, carsDAL);
            }
            else if (convertive is List<CarDAL> cars1DAL && converted is List<Car> cars1)
            {
                conversionDALToCar(cars1DAL, cars1);
            }
            else if (convertive is List<Booking> bookings && converted is List<BookingDAL> bookingsDAL)
            {
                conversionBookingToDAL(bookings, bookingsDAL);
            }
            else if (convertive is List<BookingDAL> bookings1DAL && converted is List<Booking> bookings1)
            {
                conversionDALToBooking(bookings1DAL, bookings1);
            }
        }

        private static void  conversionTrainToDAL(List<Train> trains, List<TrainDAL> trainsDAL)
        {
            foreach(Train train in trains)
            {
                trainsDAL.Add(new TrainDAL(train.trainNumber, train.dispatch, train.destination, train.departure));
            }
        }
        private static void  conversionDALToTrain(List<TrainDAL> trainsDAL, List<Train> trains)
        {
            foreach(TrainDAL train in trainsDAL)
            {
                trains.Add(new Train(train.trainNumber, train.dispatch, train.destination, train.departure));
            }
        }
        private static void  conversionCarToDAL(List<Car> cars, List<CarDAL> carsDAL)
        {
            foreach(Car car in cars)
            {
                carsDAL.Add(new CarDAL(car.trainNumber, car.carNumber, car.sitsTaken));
            }
        }
        private static void  conversionDALToCar(List<CarDAL> carsDAL, List<Car> cars)
        {
            foreach(CarDAL car in carsDAL)
            {
                cars.Add(new Car(car.trainNumber, car.carNumber, car.sitsTaken));
            }
        }
        private static void  conversionBookingToDAL(List<Booking> bookings, List<BookingDAL> bookingsDAL)
        {
            foreach(Booking booking in bookings)
            {
                bookingsDAL.Add(new BookingDAL(booking.trainNumber, booking.carNumber, booking.sitNumber));
            }
        }
        private static void  conversionDALToBooking(List<BookingDAL> bookingsDAL, List<Booking> bookings)
        {
            foreach(BookingDAL booking in bookingsDAL)
            {
                bookings.Add(new Booking(booking.trainNumber, booking.carNumber, booking.sitNumber));
            }
        }
    }
}
