using System.Collections.Generic;

namespace StunningEnigma
{
    public class Neuron
    {
        public Dictionary<double, List<Synapse>> Synapses { get; set; } =  new Dictionary<double, List<Synapse>>();
        public int NeuronId { get; set; }
        public double NetValue { get; set; }
        public double OutValue { get; set; }
        public double Error { get; set; }

        public Neuron(double netValue)
        {
            NetValue = netValue;
        }

    }
}