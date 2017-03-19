using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    class HiddenLayer:INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; }


        public HiddenLayer(int nrOfNeurons, bool biasNeuron, INeuralLayer inputLayer)
        {
            InitializeNeurons(nrOfNeurons, inputLayer);
           
            ///TODO: BIAS
            if (biasNeuron)
            {
                
            }
        }

        private void InitializeNeurons(int nrOfNeurons, INeuralLayer inputLayer)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1), inputLayer.Neurons);
                Neurons.Add(neuron);
            }
        }

    }
}
