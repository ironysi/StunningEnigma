using System.Collections.Generic;
using StunningEnigma.Network;

namespace StunningEnigma
{
    public interface INeuron
    {
        void ActivationFunction();
        double OutValue { get; set; }

        List<Synapse> OutputSynapses { get; set; }
    }
}
