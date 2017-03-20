using System;
using System.Collections.Generic;

namespace StunningEnigma.Network
{
    class OutputLayer : INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; } = new List<INeuron>();

        public OutputLayer(int nrOfNeurons, INeuralLayer previousLayer)
        {
            this.BiasNeuron = false;
            InitializeNeurons(nrOfNeurons, previousLayer);
        }

        private void InitializeNeurons(int nrOfNeurons, INeuralLayer previousLayer)
        {
            Random rnd = new Random();

            for (int i = 0; i < nrOfNeurons; i++)
            {
                Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1), previousLayer.Neurons);
                Neurons.Add(neuron);
            }
        }


    }
}
