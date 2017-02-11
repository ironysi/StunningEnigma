using System.Collections.Generic;
using System.Linq;

namespace StunningEnigma
{
    public class NeuralNet
    {
        public NeuralLayer InputLayer { get; set; }
        public NeuralLayer HiddenLayer { get; set; }
        public NeuralLayer OutputLayer { get; set; }
        public double NNError { get; set; }

        public NeuralNet(double[] inputs, int hiddenNeuronsCount, int outputNeuronsCount)
        {
            InputLayer = new NeuralLayer(inputs, hiddenNeuronsCount, true);
            HiddenLayer = new NeuralLayer(hiddenNeuronsCount, outputNeuronsCount, true);
            OutputLayer = new NeuralLayer(outputNeuronsCount);

            CalculateAllNetValues();
        }

        private void CalculateAllNetValues()
        {
            for (int i = 0; i < HiddenLayer.Neurons.Count; i++)
            {
                Neuron neuron = HiddenLayer.Neurons[i];
                double sum = 0;

                if (neuron.IsBias == false)
                {
                    for (int j = 0; j < InputLayer.Neurons.Count; j++)
                    {
                        //gets synapse with index i of each input neuron j 
                        double weight = InputLayer.Neurons[j].Synapses[InputLayer.Neurons[j].NetValue][i].Value;
                        sum += weight * InputLayer.Neurons[j].NetValue;
                    }
                }
                neuron.NetValue = sum;
            }
        }

    }
}