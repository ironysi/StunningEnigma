using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    class OutputLayer : INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; }

        public OutputLayer(int nrOfNeurons, bool biasNeuron, INeuralLayer previousLayer)
        {
            InitializeNeurons(nrOfNeurons, previousLayer);
        }

        private void InitializeNeurons(int nrOfNeurons, INeuralLayer previousLayer)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1), previousLayer.Neurons);
                Neurons.Add(neuron);
            }
        }


    }
}
