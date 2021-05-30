using BLL;
using BLL.CarService;
using BLL.TrainService;
using BLL.BookingService;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PL
{
    public class Menu
    {
        readonly ITrainService trainService;    
        readonly ICarService carService;
        readonly IBookingService bookingService; 

        private Dictionary<string, Dictionary<string, Action<string>>> menuFunctions;
        private Dictionary<string, Action<string>> currentFunctions;
        private Dictionary<string, string> entries;

        public Menu(ITrainService trainService, ICarService carService, IBookingService bookingService)
        {
            this.trainService = trainService;
            this.carService = carService;
            this.bookingService = bookingService;
            menuFunctions = GetMenuFunctions();
            entries = GetMenuEntries();
            menu("initial");
        }
        
        public void menu(string type)
        {
            string func = "";

            string entry = entries[type]; 

            while (!func.Equals("exit"))
            {
                currentFunctions = menuFunctions[type];
                Console.WriteLine(entry);
                try
                {
                    func = GetInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                    if (func.Equals("exit")) break;
                    currentFunctions[func].Invoke(func);
                }
                catch (Exception e) when (e is InvalidInputException || e is KeyNotFoundException)
                {
                    Console.WriteLine("No such option.");
                }
            }
        }

        public void add(string entity)
        {
            if (entity.Equals("train"))
            {
                addTrain();
            }
            else if (entity.Equals("car"))
            {
                addCar();
            }
            else addBooking(); 
        }

        public void addTrain()
        {
            try
            {
                Console.WriteLine("Enter number of train (6-18 digits): " );
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,18}"));
                trainService.trainExists(train, false);
                Console.WriteLine("Enter train dispatch station: " );
                string dispatch = GetInputService.GetVerifiedInput(@"[A-Za-z]{3,20}");
                Console.WriteLine("Enter train destination station: " );
                string destination = GetInputService.GetVerifiedInput(@"[A-Za-z]{3,20}");
                Console.WriteLine("Enter departure date (dd/MM/yyyy): ");
                DateTime departure = DateTime.ParseExact(GetInputService.GetVerifiedInput(@"\d\d?/\d\d?/\d{4}"), "d/M/yyyy", CultureInfo.InvariantCulture);
                trainService.add(train, dispatch, destination, departure); 
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid data entered.");
            }
            catch (TrainNumberException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void addCar()
        {
            try
            {
                Console.WriteLine("Enter number of exitsting train (6-18 digits): " );
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,18}"));
                trainService.trainExists(train, true); 
                Console.WriteLine("Enter car number (2-4 digits): " );
                ushort car = Convert.ToUInt16(GetInputService.GetVerifiedInput(@"\d{2,4}"));
                carService.carExists(train, car, false);
                carService.add(train, car);
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid data entered.");
            }
            catch (TrainNumberException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void addBooking()
        {
            try
            {
                Console.WriteLine("Enter number of exitsting train (6-18 digits): ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,18}"));
                trainService.trainExists(train, true);
                Console.WriteLine("Enter number of existing car (2-4 digits): ");
                ushort car = Convert.ToUInt16(GetInputService.GetVerifiedInput(@"\d{2,4}"));
                carService.carExists(train, car, true);
                Console.WriteLine("Enter number of sit you want to book (1-30): ");
                byte sit = Convert.ToByte(GetInputService.GetVerifiedInput(@"^(?(?=[0-2])\d?\d|30)/"));
                bookingService.bookingExists(train, car, sit, false);
                bookingService.add(train, car, sit);
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid data entered.");
            }
        }

        public void delete(string entity)
        {
            if (entity.Equals("train"))
            {
                deleteTrain();
            }
            else if (entity.Equals("car"))
            {
                deleteCar();
            }
            else deleteBooking(); 
        }

        public void deleteTrain()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train to delete: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.delete(train);
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid data entered.");
            }
            catch (TrainNumberException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void deleteCar()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train to delete car from: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.trainExists(train, true);
                Console.WriteLine(carService.getTrainCars(train));
                Console.WriteLine("Select car to delete: ");
                ushort car = Convert.ToUInt16(GetInputService.GetVerifiedInput(@"\d{2,4}"));
                carService.carExists(train, car, true);
                bool isCarEmpty = carService.isCarEmpty(train, car);
                carService.delete(train, car, isCarEmpty);
            }
            catch (Exception e) when (e is TrainNumberException || e is CarNumberException || e is CarNotEmptyException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void deleteBooking()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train to delete booking from: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.trainExists(train, true);
                Console.WriteLine(carService.getTrainCars(train));
                Console.WriteLine("Select car to delete booking from: ");
                ushort car = Convert.ToUInt16(GetInputService.GetVerifiedInput(@"\d{2,4}"));
                carService.carExists(train, car, true);
                Console.WriteLine("Enter number of sit you want to delete (1-30): ");
                byte sit = Convert.ToByte(GetInputService.GetVerifiedInput(@"^(?(?=[0-2])\d?\d|30)/"));
                bookingService.bookingExists(train, car, sit, true);
                bookingService.delete(train, car, sit);

            }
            catch (Exception e) when (e is TrainNumberException || e is CarNumberException || e is BookingNumberException)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void view(string entity)
        {
            switch(entity)
            {
                case "all":
                    viewAllTrains();
                    break;
                case "single":
                    viewSingleTrain(); 
                    break;
                case "percentage":
                    viewPercentage();
                    break;
                case "vacancy":
                    viewVacancy(); 
                    break;
                case "booking":
                    viewBooking(); 
                    break;
            }
        }

        public void viewAllTrains()
        {
            Console.WriteLine(trainService.getAllTrains());
        }
        public void viewSingleTrain()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train you want to view: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.trainExists(train, true);
                Console.WriteLine(trainService.getSingleTrain(train));
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid data entered.");
            }
            catch (TrainNumberException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void viewPercentage()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train you want to view: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.trainExists(train, true);
                Console.WriteLine(carService.getPercentage(train));
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid data entered.");
            }
            catch (TrainNumberException e)
            {
                Console.WriteLine(e.Message);
            }
          
        }
        public void viewVacancy()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train you want to view: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.trainExists(train, true);
                Console.WriteLine(carService.getTrainCars(train));
                Console.WriteLine("Select car you want to view: ");
                ushort car = Convert.ToUInt16(GetInputService.GetVerifiedInput(@"\d{2,4}"));
                carService.carExists(train, car, true);
                carService.getCarVacantSits(train, car);
            }
            catch (Exception e) when (e is TrainNumberException || e is CarNumberException || e is CarNotEmptyException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void viewBooking()
        {
            try
            {
                Console.WriteLine(trainService.getAllTrains());
                Console.WriteLine("Select train you want to view: ");
                ulong train = Convert.ToUInt64(GetInputService.GetVerifiedInput(@"\d{6,20}"));
                trainService.trainExists(train, true);
                Console.WriteLine(carService.getTrainCars(train));
                Console.WriteLine("Select car you want to view: ");
                ushort car = Convert.ToUInt16(GetInputService.GetVerifiedInput(@"\d{2,4}"));
                carService.carExists(train, car, true);
                Console.WriteLine("Enter number of sit you want to view (1-30): ");
                byte sit = Convert.ToByte(GetInputService.GetVerifiedInput(@"^(?(?=[0-2])\d?\d|30)/"));
                Console.WriteLine(bookingService.getBooking(train, car, sit)); 
            }
            catch (Exception e) when (e is TrainNumberException || e is CarNumberException || e is BookingNumberException)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void search(string entity)
        {
            if(entity.Equals("keyword"))
            {
                searchByKeyword(); 
            }
            else
            {
                searchByDate();
            }
        }

        public void searchByKeyword()
        {
            Console.WriteLine("Enter keyword: ");
            string keyword = GetInputService.GetVerifiedInput(@"[A-Za-z]{3,20}");
            Console.WriteLine(trainService.searchByKeyword(keyword));
        }
        public void searchByDate()
        {
            Console.WriteLine("Enter search date (dd/MM/yyyy): ");
            DateTime date = DateTime.ParseExact(GetInputService.GetVerifiedInput(@"\d\d?/\d\d?/\d{4}"), "d/M/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(bookingService.getBookingByDate(date)); 
        }

        public Dictionary<string, Dictionary<string, Action<string>>> GetMenuFunctions()
        {
            Dictionary<string, Dictionary<string, Action<string>>> menus = new Dictionary<string, Dictionary<string, Action<string>>>();
            menus.Add("initial", GetInitialFunctions());
            menus.Add("add", GetAddFunctions());
            menus.Add("delete", GetDeleteFunctions());
            menus.Add("view", GetViewFunctions());
            menus.Add("search", GetSearchFunctions());
            return menus;
        }

        public Dictionary<string, Action<string>> GetInitialFunctions()
        {
            Dictionary<string, Action<string>> functions = new Dictionary<string, Action<string>>();
            functions.Add("add", delegate { menu("add"); });
            functions.Add("delete", delegate { menu("delete"); });
            functions.Add("view", delegate { menu("view"); });
            return functions;
        }

        public Dictionary<string, Action<string>> GetAddFunctions()
        {
            Dictionary<string, Action<string>> functions = new Dictionary<string, Action<string>>();
            functions.Add("train", delegate { add("train"); });
            functions.Add("car", delegate { add("car"); });
            functions.Add("booking", delegate { add("booking"); });
            return functions;
        }
        public Dictionary<string, Action<string>> GetDeleteFunctions()
        {
            Dictionary<string, Action<string>> functions = new Dictionary<string, Action<string>>();
            functions.Add("train", delegate { delete("train"); });
            functions.Add("car", delegate { delete("car"); });
            functions.Add("booking", delegate { delete("booking"); });
            return functions;
        }

        public Dictionary<string, Action<string>> GetViewFunctions()
        {
            Dictionary<string, Action<string>> functions = new Dictionary<string, Action<string>>();
            functions.Add("all", delegate { view("all"); }); 
            functions.Add("single", delegate { view("single"); }); 
            functions.Add("percentage", delegate { view("percentage"); }); 
            functions.Add("vacancy", delegate { view("vacancy"); }); 
            functions.Add("booking", delegate { view("booking"); }); 
            return functions;
        }

        public Dictionary<string, Action<string>> GetSearchFunctions()
        {
            Dictionary<string, Action<string>> functions = new Dictionary<string, Action<string>>();
            functions.Add("keyword", delegate { search("keyword"); });
            functions.Add("date", delegate { search("date"); });
            return functions;
        }

        private Dictionary<string, string> GetMenuEntries()
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            entries.Add("initial", initialMenu);
            entries.Add("add", addMenu);
            entries.Add("delete", deleteMenu);
            entries.Add("view", viewMenu);
            return entries; 
        }

        private string initialMenu = "\nWhat do you want to do?\n\"Add\"\n\"Delete\"\n\"View\"\n\"Exit\"";
        private string addMenu = "\nWhat do you want to add?\nTrain\nCar\nBooking";
        private string deleteMenu = "\nWhat do you want to delete?\nTrain\nCar\nBooking";
        private string viewMenu = "\nWhat do you want to view?\n\"All\" trains\n\"Single\" train" +
            "\nTrain booking \"percentage\"";
    }
}
