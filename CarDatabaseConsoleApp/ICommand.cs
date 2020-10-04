using System;
using System.Collections.Generic;
using System.Text;

namespace CarDatabaseConsoleApp
{
    interface ICommand
    {
        void Process(CarDatabase carDatabase);
    }
}
