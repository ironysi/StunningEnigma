using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
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
            foreach (Neuron neuron in layer.Neurons)
            {
                double value = 0.0;

                for (int i = 0; i < neuron.InputSynapses.Count; i++)
                {
                    value = value * neuron.InputSynapses[i].Weight;
                }

                neuron.NetValue = value;
                neuron.Pulse();
            }
        }


    }
}
