using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDatabaseConsoleApp
{
    public class CarDatabase
    {
        public CarDatabase()
        {
            m_cars = new Dictionary<string, Car>();
        }

        public void Add(Car car)
        {
            m_cars.Add(car.VIN, car);
        }

        public bool Remove(string vin)
        {
            return m_cars.Remove(vin);
        }

        public Car Get(string vin)
        {
            if (m_cars.TryGetValue(vin, out Car value))
                return value;

            return null;
        }

        public Car[] GetCars()
        {
            return m_cars.Values.ToArray();
        }

        public static CarDatabase Load(string fileName)
        {
            CarDatabase db = new CarDatabase();
            if (!File.Exists(fileName))
                db.InitDefault();
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    List<Car> list = (List<Car>)serializer.Deserialize(fs);
                    foreach (var entry in list)
                        db.Add(entry);
                }
            }

            return db;
        }

        public static void Save(string fileName, CarDatabase db)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
            var list = new List<Car>(db.m_cars.Count);
            foreach (var item in db.m_cars)
                list.Add(item.Value);
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
                serializer.Serialize(fs, list);
        }

        void Clear()
        {
            m_cars.Clear();
        }

        void InitDefault()
        {
            Add(new Car("ABCDEASDFS", new DateTime(2002, 01, 01), "Toyota", "Opa", CarType.Hatchback));
            Add(new Car("VINBIN", new DateTime(2005, 01, 01), "Honda", "Accord", CarType.Sedan));
            Add(new Car("291dfdas", new DateTime(2014, 01, 01), "Opel", "Mokko", CarType.Crossover));
        }

        private Dictionary<string, Car> m_cars;
    }

}
