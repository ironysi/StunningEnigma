using System;
using System.Linq;
using StunningEnigma.Network;

namespace StunningEnigma.NetworkLogic
{
    public static class FeedForward
    {
        public static void FeedForwardPropagation(NeuralNet neuralNet, double[] inputs)
        {
            FeedInput(neuralNet.InputLayer, inputs);
            FeedLayer(neuralNet.HiddenLayer);
            FeedLayer(neuralNet.OutputLayer);
        }

        private static void FeedInput(INeuralLayer layer, double[] inputs)
        {
            int i = 0;

            foreach (Neuron neuron in layer.Neurons.Where(x => x is Neuron))
            {
                neuron.OutValue = inputs[i];
                i++;
            }
        }

        private static void FeedLayer(INeuralLayer layer)
        {
            foreach (INeuron neuron in layer.Neurons)
            {
                neuron.ActivationFunction();
            }
        }


    }
}
