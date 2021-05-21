using BLL;
using System;
using System.Collections.Generic;
namespace PL
{
    public class Menu
    {
        private Dictionary<string, Dictionary<string, Action<string>>> menuFunctions;
        private Dictionary<string, Action<string>> currentFunctions;
        private Dictionary<string, string> entries;

        public Menu()
        {
            menuFunctions = GetMenuFunctions();
            entries = GetMenuEntries();
            menu("initial");
        }
        
        public Dictionary<string, Dictionary<string, Action<string>>> GetMenuFunctions()
        {
            Dictionary<string, Dictionary<string, Action<string>>> menus = new Dictionary<string, Dictionary<string, Action<string>>>();
            menus.Add("initial", GetInitialFunctions());
            menus.Add("add", GetAddFunctions());
            return menus; 
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
            Console.WriteLine("Added " + entity);
        }

        public Dictionary<string, Action<string>> GetInitialFunctions()
        {
            Dictionary<string, Action<string>> functions = new Dictionary<string, Action<string>>();
            functions.Add("add", delegate { menu("add"); });
            functions.Add("delete", delegate { menu("delete"); });
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

        private Dictionary<string, string> GetMenuEntries()
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            entries.Add("initial", initialMenu);
            entries.Add("add", addMenu);
            entries.Add("delete", deleteMenu);
            return entries; 
        }

        private string initialMenu = "\nWhat do you want to do?\n\"Add\"\n\"Delete\"\n\"Exit\"";
        private string addMenu = "\nWhat do you want to add?\nTrain\nCar\nBooking\n";
        private string deleteMenu = "\nWhat do you want to delete?\nTrain\nCar\nBooking\n";
    }
}
