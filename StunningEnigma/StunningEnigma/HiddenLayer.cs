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
        public List<Neuron> Neurons { get; set; }


        public HiddenLayer(int nrOfNeurons, bool biasNeuron, int nrOfInputNeurons, int nrOfOutputNeurons)
        {
            InitializeNeurons(nrOfNeurons);
            InitializeWeights(nrOfInputNeurons, nrOfOutputNeurons);
           
            ///TODO: BIAS
            if (biasNeuron)
            {
                
            }
        }

        private void InitializeNeurons(int nrOfNeurons)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                Neuron neuron = new Neuron(Utilities.DoubleBetween(0, 1));
                Neurons.Add(neuron);
            }
        }

        private void InitializeWeights(int nrOfInputNeurons, int nrOfOutputNeurons)
        {
            foreach (Neuron neuron in Neurons)
            {
                for (int i = 0; i < nrOfInputNeurons; i++)
                {
                    Synapse synapse = new Synapse(Utilities.DoubleBetween(0,1));
                    neuron.InputSynapses.Add(synapse);
                }

                for (int i = 0; i < nrOfOutputNeurons; i++)
                {
                    Synapse synapse = new Synapse(Utilities.DoubleBetween(0, 1));
                    neuron.OutputSynapses.Add(synapse);
                }
            }   
        }

    }
}
