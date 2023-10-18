using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Alytalo
{
    public class Sauna
    {
        
        public Boolean SaunaSwitched { get; set; }
        public double SaunaTemperature { get; set; }
        public int SaunaMaxTemp { get; set; }

        public Thermostat Thermostat { get; set; } 

        public Sauna(Thermostat thermostat) 
        { 
            Thermostat = thermostat;
            SaunaTemperature = Thermostat.Temperature;
        }

        public void SaunaOn()
        {
            SaunaSwitched = true;
            SaunaTemperature += 0.5;
        }

        public void SaunaOff() 
        { 
            SaunaSwitched = false;
            SaunaTemperature--;
        }
                
    }
}
