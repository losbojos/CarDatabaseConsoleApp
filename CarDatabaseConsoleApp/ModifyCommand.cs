using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    class ModifyCommand : ICommand
    {
        public ModifyCommand(string[] words)
        {
            if (!words[0].Equals("-modify"))
                throw new ArgumentException("The command should start with the word '-modify'");

            for (int i = 1; i < words.Length; i++)
            {
                switch (words[i])
                {
                    case "-vin":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-vin'.");

                        m_vin = words[i + 1];

                        i++;
                        break;

                    case "-year":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-year'.");

                        if (!int.TryParse(words[i + 1], out int year))
                            throw new ArgumentException("Invalid year value \"" + words[i + 1] + "\"");

                        m_newDateTime = new DateTime(year, 1, 1);

                        i++;
                        break;

                    case "-vendor":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-vendor'.");

                        m_newVendor = words[i + 1];

                        i++;
                        break;

                    case "-model":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-model'.");

                        m_newModel = words[i + 1];
                        i++;

                        break;

                    case "-type":

                        if (i + 1 >= words.Length)
                            throw new ArgumentException("No value for the argument '-type'.");

                        if (!Enum.TryParse<CarType>(words[i + 1], out m_newCarType))
                            throw new ArgumentException("Invalid car type value \"" + words[i + 1] + "\"");

                        m_newCarTypeInitialized = true;
                        i++;
                        break;

                    default:
                        throw new ArgumentException("Invalid argument \"" + words[i] + "\"");
                }
            }

            if (m_vin == null)
                throw new ArgumentException("VIN number required.");
        }

        public void Process(CarDatabase cars)
        {
            var car = cars.Get(m_vin);
            if (car == null)
                throw new Exception(String.Format("The car with specified VIN \"{0}\"  is not found in the database", m_vin));

            if (m_newDateTime.HasValue)
                car.IssueDate = m_newDateTime.Value;

            if (m_newCarTypeInitialized)
                car.CarType = m_newCarType;

            if (m_newVendor != null)
                car.Vendor = m_newVendor;

            if (m_newModel != null)
                car.Model = m_newModel;
        }

        DateTime? m_newDateTime = null;

        CarType m_newCarType = CarType.Sedan; // any value
        bool m_newCarTypeInitialized = false;

        string m_vin = null, m_newVendor = null, m_newModel = null;

    }
}
