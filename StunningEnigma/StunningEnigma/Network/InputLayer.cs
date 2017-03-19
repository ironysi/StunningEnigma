using System.Collections.Generic;

namespace StunningEnigma.Network
{
    class InputLayer:INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; }

        public InputLayer(int nrOfNeurons, bool biasNeuron, double[] inputs)
        {
            InitializeNeuronsAndSynapses(nrOfNeurons);

            if (biasNeuron)
            {
                BiasNeuron bias = new BiasNeuron(Neurons);
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
