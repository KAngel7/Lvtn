using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNetwork
{
    class Constants
    {
        public  const double RPROP_INIT_UPDATE_VALUE = 0.0;
        public static double RPROP_MIN_UPDATE_VALUE = Math.Pow(Math.E, -6);
        public  const double RPROP_MAX_UPDATE_VALUE = 50.0;
        public  const double RPROP_DECREASE_FACTOR = 0.5;
        public const double RPROP_INCREASE_FACTOR = 1.2; 
    }
}
