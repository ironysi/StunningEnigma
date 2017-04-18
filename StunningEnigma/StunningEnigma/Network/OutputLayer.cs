using System;
using System.Collections.Generic;

namespace StunningEnigma.Network
{
    class OutputLayer : INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; } = new List<INeuron>();

        public OutputLayer(int nrOfNeurons, INeuralLayer previousLayer, bool negative)
        {
            this.BiasNeuron = false;
            InitializeNeurons(nrOfNeurons, previousLayer, negative);
        }

        private void InitializeNeurons(int nrOfNeurons, INeuralLayer previousLayer, bool negative)
        {
            if (negative)
            {
                for (int i = 0; i < nrOfNeurons; i++)
                {
                    Neuron neuron = new Neuron(Utilities.DoubleBetween(-1, 1, 42), previousLayer.Neurons, negative);
                    Neurons.Add(neuron);
                }
            }
            else
            {
                for (int i = 0; i < nrOfNeurons; i++)
                {
                    Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1, 42), previousLayer.Neurons);
                    Neurons.Add(neuron);
                }
            }

        }


    }
}
