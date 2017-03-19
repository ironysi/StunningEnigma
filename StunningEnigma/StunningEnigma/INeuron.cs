using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    public interface INeuron
    {
        void Pulse();
        void ActivationFunction(double value);

        double OutValue { get; set; }
        int NeuronId { get; set; }

    }
}
