using System.Collections.Generic;
using System.Linq;

namespace StunningEnigma.Network
{
    class BiasNeuron:INeuron
    {
        public double OutValue { get; set; }

        public List<Synapse> OutputSynapses { get; set; } = new List<Synapse>();

        public BiasNeuron(List<INeuron> inputNeurons, double biasSize)
        {
            OutValue = biasSize;

            foreach (INeuron inputNeuron in inputNeurons.OfType<Neuron>())
            {
                Synapse synapse = new Synapse(Utilities.DoubleBetween(0, 1, 42), this, (Neuron)inputNeuron);

                inputNeuron.OutputSynapses.Add(synapse); // creates output synapse for input neuron 
                                                         // -->> therefore I never have to initialize output synapses
            }
        }
        public void ActivationFunction()
        {
            OutValue = OutValue;
        }

    }
}
