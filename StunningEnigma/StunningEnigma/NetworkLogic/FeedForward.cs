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
          //  SetDropToFalse(neuralNet.DropoutLayer); //set all neurons back to normal state
          //  DropNeurons(neuralNet.DropoutLayer); //drop neurons
          //  FeedLayer(neuralNet.DropoutLayer);
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
            foreach (Neuron neuron in layer.Neurons.OfType<Neuron>())
            {
                if (neuron.IsDropped == false)
                {
                    neuron.CalculateNetValue();
                    neuron.ActivationFunction();
                }
            }
        }

        private static void DropNeurons(INeuralLayer layer)
        {
            double percentage = layer.Neurons.Count() - 1;
            percentage = Math.Ceiling(percentage * 0.5);
            Random random = new Random();

            for (int i = 0; i < percentage; i++)
            {
                int x = random.Next(layer.Neurons.OfType<Neuron>().Count());

                layer.Neurons.OfType<Neuron>().ElementAt(x).IsDropped = true;
            }
        }

        private static void SetDropToFalse(INeuralLayer layer)
        {
            foreach (Neuron neuron in layer.Neurons.OfType<Neuron>())
            {
                neuron.IsDropped = false;
            }
        }

    }
}
