using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    class AddCommand : ICommand
    {
        public AddCommand(string[] words)
        {
            if (!words[0].Equals("-add"))
                throw new ArgumentException("The command should start with the word '-add'");

            string vin = null, vendor = null, model = null;
            DateTime? dateTime = null;
            CarType carType = CarType.Sedan; // any value
            bool carTypeInitialized = false;

            for (int i=1; i<words.Length; i++)
            {
                switch (words[i])
                {
                    case "-vin":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-vin'.");

                        vin = words[i + 1];

                        i++;
                        break;

                    case "-year":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-year'.");

                        if (!int.TryParse(words[i + 1], out int year))
                            throw new ArgumentException("Invalid year value \"" + words[i + 1] + "\"");

                        dateTime = new DateTime(year, 1, 1);

                        i++;
                        break;

                    case "-vendor":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-vendor'.");

                        vendor = words[i + 1];

                        i++;
                        break;

                    case "-model":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-model'.");

                        model = words[i + 1];
                        i++;

                        break;

                    case "-type":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-type'.");

                        if (!Enum.TryParse<CarType>(words[i + 1], out carType))
                            throw new ArgumentException("Invalid car type value \"" + words[i + 1] + "\"");

                        carTypeInitialized = true;
                        i++;
                        break;

                    default:
                        throw new ArgumentException("Invalid argument \"" + words[i] + "\"");
                }
            }

            if (!carTypeInitialized || vin == null || vendor == null || model == null || !dateTime.HasValue)
            {
                StringBuilder sb = new StringBuilder("Some arguments are missing: ");

                if (!carTypeInitialized)
                    sb.Append("Car type, ");

                if (vin == null)
                    sb.Append("VIN, ");

                if (vendor == null)
                    sb.Append("vendor, ");

                if (model == null)
                    sb.Append("model, ");

                if (!dateTime.HasValue)
                    sb.Append("year, ");

                throw new ArgumentException(sb.ToString());
            }

            m_newCar = new Car(vin, dateTime.Value, vendor, model, carType);
        }

        public void Process(CarDatabase cars)
        {
            cars.Add(m_newCar);

        }

        private Car m_newCar;
    }
}
