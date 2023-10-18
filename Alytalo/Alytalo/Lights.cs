using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alytalo
{
    public class Lights
    {
        public Boolean Switched { get; set; }
        public string Dimmer 
        {
            get
            {
                return dimmer;
            }
            set
            {
                dimmer = value;

                if (dimmer == "0")
                {
                    Switched = false;
                }
                else
                {
                    Switched = true;
                }

            }
        }
        private string dimmer;
     } 
}
