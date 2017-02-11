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
        }

        public void Train()
        {
            CalculateAllNetValues();
            CalculateAllOutValues();
        }

        private void CalculateAllNetValues()
        {
            CalcNetValuesForLayer(HiddenLayer, InputLayer);
            CalcNetValuesForLayer(OutputLayer, HiddenLayer);
        }

        private void CalcNetValuesForLayer(NeuralLayer targetLayer, NeuralLayer previousLayer)
        {
            for (int i = 0; i < targetLayer.Neurons.Count; i++)
            {
                Neuron neuron = targetLayer.Neurons[i];
                double sum = 0;

                if (!neuron.IsBias)
                {
                    for (int j = 0; j < previousLayer.Neurons.Count; j++)
                    {
                        //gets synapse with index i of each input neuron j 
                        double weight = previousLayer.Neurons[j].Synapses[previousLayer.Neurons[j].NetValue][i].Value;
                        sum += weight * previousLayer.Neurons[j].NetValue;
                    }
                }
                neuron.NetValue = sum;
            }
        }

        private void CalculateAllOutValues()
        {
            CalculateOutValuesForLayer(HiddenLayer);
            CalculateOutValuesForLayer(OutputLayer);
        }

        private void CalculateOutValuesForLayer(NeuralLayer layer)
        {
            foreach (Neuron neuron in layer.Neurons)
            {
                neuron.ActivationFunction(neuron.NetValue);
            }
        }

        private double TotalError()
        {


            return 0;
        }

    }
}