using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    public class GetCommand : ICommand
    {
        public GetCommand(string[] words)
        {
            if (!words[0].Equals("-get"))
                throw new ArgumentException("The command should start with the word '-get'");

            if (words.Length < 2)
                throw new ArgumentException("The command '-get' should have at least one argument");

            if (words[1].Equals("all"))
            {
                if (words.Length > 2)
                    throw new ArgumentException("The command should not have additional arguments");

                ReadAll = true;
            }
            else
            {
                Vins = new string[words.Length - 1];
                Array.Copy(words, 1, Vins, 0, Vins.Length);
            }
        }

        bool ReadAll { get; set; }

        string[] Vins { get; set; }

        public void Process(CarDatabase cars)
        {
            if (ReadAll)
            {
                var carsArray = cars.GetCars();

                for (int n = 1; n <= carsArray.Length; n++)
                {
                    Console.WriteLine(String.Format("{0}. {1}", n, carsArray[n - 1]));
                }
            }
            else
            {
                for (int n = 1; n <= Vins.Length; n++)
                {
                    Car car = cars.Get(Vins[n - 1]);
                    if (car != null)
                        Console.WriteLine(String.Format("{0}. {1}", n, car));
                    else
                        Console.WriteLine(String.Format("{0}. The car with specified VIN \"{1}\" is not found in the database", n, Vins[n - 1]));
                }
            }
        }
    }
}
