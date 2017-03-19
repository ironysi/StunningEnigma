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
        public List<Neuron> Neurons { get; set; }

        public InputLayer(int nrOfNeurons, bool biasNeuron, List<double> inputs, int numberOfHiddenNeurons)
        {
            InitializeNeuronsAndSynapses(nrOfNeurons, inputs, numberOfHiddenNeurons);

            if (biasNeuron)
            {
                //Add a bias Neuron
            }
        }

        public void InitializeNeuronsAndSynapses(int nrOfNeurons, List<double> inputs, int numberOfHiddenNeurons)
        {
            foreach (double input in inputs)
            {
                Neuron neuron = new Neuron(input);

                for (int i = 0; i < numberOfHiddenNeurons; i++)
                {
                    Synapse synapse = new Synapse(Utilities.DoubleBetween(0,1));

                    neuron.OutputSynapses.Add(synapse);
                }
                Neurons.Add(neuron);
            }
        }
    }
}
