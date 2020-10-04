using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    public class HelpCommand : ICommand
    {
        public void Process(CarDatabase cars)
        {
            Console.WriteLine("Possible operations:");
            Console.WriteLine("-add -vin VIN_NUMBER -year YEAR -vendor VENDOR -model MODEL -type TYPE: To add a new car. All parameters are required but can be set in any order.");
            Console.WriteLine("-delete VIN1 VIN2 VIN-n: To delete one or more cars from the database.");
            Console.WriteLine("-help (or -h) : To display help for all available operations.");
            Console.WriteLine("-quit (or -q) : To exit the application.");
            Console.WriteLine("-get all: To display a list of registered cars.");
            Console.WriteLine("-get VIN1 VIN2 VIN-n: To display one or more cars by specified VIN-s.");
            Console.WriteLine("-modify -vin VIN_NUMBER -year YEAR -vendor VENDOR -model MODEL -type TYPE: To modify a car with VIN=VIN_NUMBER. You can change any properties exept VIN-number.");
            Console.WriteLine("-types : To display valid car types.");
        }
    }
}
