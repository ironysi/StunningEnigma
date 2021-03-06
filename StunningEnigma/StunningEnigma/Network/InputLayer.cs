﻿using System.Collections.Generic;

namespace StunningEnigma.Network
{
    class InputLayer:INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; } = new List<INeuron>();

        public InputLayer(int nrOfNeurons, bool biasNeuron, double biasSize = 1)
        {
            InitializeNeuronsAndSynapses(nrOfNeurons);

            if (biasNeuron)
            {
                BiasNeuron bias = new BiasNeuron(Neurons, biasSize);
                Neurons.Add(bias);
            }
        }

        public void InitializeNeuronsAndSynapses(int nrOfNeurons)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                 Neuron neuron = new Neuron(0);

                Neurons.Add(neuron);
            }
        }
    }
}
