using System;
using System.Collections.Generic;

namespace StunningEnigma.Network
{
    class HiddenLayer:INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; } = new List<INeuron>();


        public HiddenLayer(int nrOfNeurons, bool biasNeuron, INeuralLayer inputLayer, double biasSize = 1)
        {
            InitializeNeurons(nrOfNeurons, inputLayer);
           
            if (biasNeuron)
            {
                BiasNeuron bias = new BiasNeuron(inputLayer.Neurons, biasSize);
                Neurons.Add(bias);
            }
        }

        private void InitializeNeurons(int nrOfNeurons, INeuralLayer inputLayer)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                
                Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1, 42), inputLayer.Neurons);
                Neurons.Add(neuron);
            }
        }


    }
}
