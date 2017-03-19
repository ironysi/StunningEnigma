using System;
using System.Collections.Generic;

namespace StunningEnigma.Network
{
    class BiasNeuron:INeuron
    {
        

        public double OutValue { get; set; }
        public int NeuronId { get; set; }

        public List<Synapse> OutputSynapses { get; set; }

        public BiasNeuron(List<INeuron> inputNeurons)
        {
            OutValue = 1;

            foreach (INeuron inputNeuron in inputNeurons)
            {
                Synapse synapse = new Synapse(Utilities.DoubleBetween(0, 1), inputNeuron, this);

                inputNeuron.OutputSynapses.Add(synapse); // creates output synapse for input neuron 
                                                         // -->> therefore I never have to initialize output synapses
            }
        }
        public void ActivationFunction()
        {
            OutValue = 1;
        }
    }
}
