using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace CarDatabaseConsoleApp
{
    class Program
    {
        const string DATABASE_FILE = "CarDatabase.xml";

        static ICommand ParseLine(string str)
        {
            string[] words = str.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0)
            {
                if (words[0].Equals("-q") || words[0].Equals("-quit"))
                    return new QuitCommand();

                if (words[0].Equals("-h") || words[0].Equals("-help"))
                    return new HelpCommand();

                if (words[0].Equals("-get"))
                    return new GetCommand(words);

                if (words[0].Equals("-delete"))
                    return new DeleteCommand(words);

                if (words[0].Equals("-add"))
                    return new AddCommand(words);

                if (words[0].Equals("-types"))
                    return new DisplayCarTypesCommand(words);

                if (words[0].Equals("-modify"))
                    return new ModifyCommand(words);

            }

            return null;
        }

        static void Main(string[] args)
        {
            CarDatabase carsDatabase = CarDatabase.Load(DATABASE_FILE);

            try
            {
                Console.WriteLine("Введите -h для справки по командам. -q для выхода из приложения.");

                string inputStr;
                while (true)
                {
                    Console.WriteLine();
                    try
                    {
                        inputStr = Console.ReadLine().Trim();
                        var command = ParseLine(inputStr);
                        if (command == null)
                            Console.WriteLine("Ошибка ввода. Введите -h для справки по командам. -q для выхода из приложения.");
                        else if (command is QuitCommand)
                            break;
                        else
                            command.Process(carsDatabase);
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }
            }
            finally
            {
                CarDatabase.Save(DATABASE_FILE, carsDatabase);
            }
        }
    }
}
