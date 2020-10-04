using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    public class DeleteCommand : ICommand
    {
        public DeleteCommand(string[] words)
        {
            if (!words[0].Equals("-delete"))
                throw new ArgumentException("The command should start with the word '-delete'");

            if (words.Length < 2)
                throw new ArgumentException("The command '-delete' should have at least one argument");

            Vins = new string[words.Length - 1];
            Array.Copy(words, 1, Vins, 0, Vins.Length);
        }

        string[] Vins { get; set; }

        public void Process(CarDatabase cars)
        {
            for (int n = 1; n <= Vins.Length; n++)
            {
                if (cars.Remove(Vins[n - 1]))
                    Console.WriteLine(String.Format("{0}. The car with specified VIN \"{1}\" has been removed from the database.", n, Vins[n - 1]));
                else
                    Console.WriteLine(String.Format("{0}. The car with specified VIN \"{1}\" is not found in the database", n, Vins[n - 1]));
            }
        }
    }
}
