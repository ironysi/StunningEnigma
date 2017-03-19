using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    class OutputLayer:INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<Neuron> Neurons { get; set; }

        public OutputLayer(int nrOfNeurons, bool biasNeuron, int nrOfHiddenNeurons)
        {
            InitializeNeurons(nrOfNeurons);
            InitializeWeights(nrOfHiddenNeurons);
        }

        private void InitializeNeurons(int nrOfNeurons)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1));
                Neurons.Add(neuron);
            }
        }

        private void InitializeWeights(int nrOfHiddenNeurons)
        {
            foreach (Neuron neuron in Neurons)
            {
                for (int i = 0; i < nrOfHiddenNeurons; i++)
                {
                    Synapse synapse = new Synapse(Utilities.DoubleBetween(0, 1));
                    neuron.InputSynapses.Add(synapse);
                }
            }
        }

    }
}
