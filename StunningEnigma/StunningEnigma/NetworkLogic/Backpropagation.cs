using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StunningEnigma.Network;

namespace StunningEnigma.NetworkLogic
{
    public static class Backpropagation
    {
        public static void BackProp(NeuralNet net)
        {
            
        }

        private static void BackpropForOutputLayer(INeuralLayer layer, double[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                CalculateGradient(layer.Neurons[i], outputs[i]);
            }
        }

        private static void UpdateWeights(Neuron neuron,double learningRate , double momentum)
        {
            double previosError = neuron.Error;
            neuron.Error = learningRate * neuron.Gradient;

            foreach (var synapse in neuron)
            {
                previosError = synapse.WeightDelta;
                synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value;
                synapse.Weight += synapse.WeightDelta + momentum * previosError;
            }
        }

        #region Gradient

        private static void CalculateGradient(Neuron neuron, double? target = null)
        {
            if (target == null)
                neuron.Gradient = neuron.OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * Utilities.SigmoidDerivative(neuron.OutValue);
            else
                neuron.Gradient = CalculateError(target.Value, neuron) * Utilities.SigmoidDerivative(neuron.OutValue);
        }

        private static double CalculateError(double targetValue, Neuron neuron)
        {
            neuron.Error = 0.5 * Math.Pow((targetValue - neuron.OutValue), 2);

            return neuron.Error;
        }

        #endregion
    }
}
