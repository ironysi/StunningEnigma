using System;
using StunningEnigma.Network;

namespace StunningEnigma.NetworkLogic
{
    public static class FeedForward
    {
        public static void FeedForwardPropagation(NeuralNet neuralNet)
        {
            FeedLayer(neuralNet.HiddenLayer);
            FeedLayer(neuralNet.OutputLayer);
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
