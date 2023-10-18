using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alytalo
{
    public class Thermostat
    {
        public int Temperature { get; set; } 
              
        public void SetTemperature(int InputTemperature)
        {
            Temperature = InputTemperature;
            if (InputTemperature >= 15 && InputTemperature <= 30) 
            {
                Temperature = InputTemperature;
            }
            else 
            {
                Temperature = 0;
                throw new ArgumentOutOfRangeException();
            }

        }

        public int GetTemperature()
        {
            return Temperature;
        }
    }
}

