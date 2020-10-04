using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    [Serializable]
    public class Car
    {
        public string VIN { get; set; } // Unique identifier
        public DateTime IssueDate { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public CarType CarType { get; set; }

        public Car()
        { }

        public Car(string vin, DateTime issueDate, string vendor, string model, CarType carType)
        {
            VIN = vin;
            IssueDate = issueDate;
            Vendor = vendor;
            Model = model;
            CarType = carType;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} VIN=\"{4}\"", CarType, Vendor, Model, IssueDate.Year, VIN);
        }
    }
}
