using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StunningEnigma.Network;

namespace StunningEnigma
{
    public interface INeuron
    {
        void ActivationFunction();
        double OutValue { get; set; }
        int NeuronId { get; set; }

        List<Synapse> OutputSynapses { get; set; }

    }
}
