using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    class BiasNeuron:INeuron
    {
        public double OutValue { get; set; }
        public int NeuronId { get; set; }

        public void Pulse()
        {
            throw new NotImplementedException();
        }

        public void ActivationFunction(double value)
        {
            OutValue = Utilities.Sigmoid(value);
        }

        
    }
}
