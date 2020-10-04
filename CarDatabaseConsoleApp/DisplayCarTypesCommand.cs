using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    class DisplayCarTypesCommand : ICommand
    {
        public DisplayCarTypesCommand(string[] words)
        {
            if (!words[0].Equals("-types"))
                throw new ArgumentException("The command should start with the word '-types'");
        }

        public void Process(CarDatabase cars)
        {
            Console.WriteLine("Valid values for the car type:");
            foreach (var name in Enum.GetNames(typeof(CarType)))
                Console.WriteLine(name);
        }

    }
}
