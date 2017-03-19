using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StunningEnigma
{
    class InputLayer:INeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<INeuron> Neurons { get; set; }

        public InputLayer(int nrOfNeurons, bool biasNeuron, List<double> inputs)
        {
            InitializeNeuronsAndSynapses(nrOfNeurons, inputs);

            if (biasNeuron)
            {
                //Add a bias Neuron
            }
        }

        public void InitializeNeuronsAndSynapses(int nrOfNeurons, List<double> inputs)
        {
            foreach (double input in inputs)
            {
                Neuron neuron = new Neuron(input);

                Neurons.Add(neuron);
            }
        }
    }
}
