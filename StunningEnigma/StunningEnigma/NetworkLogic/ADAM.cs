using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StunningEnigma.Network;

namespace StunningEnigma.NetworkLogic
{
    public static class ADAM
    {
        public static void Backprop()
        {
            
        }

        private static void BackpropHiddenLayer()
        {
            
        }

        private static void BackpropOutputLayer()
        {
                
        }







        private static void CalculateDelta(Neuron neuron, double desiredOutput)
        {
            neuron.Delta = (-1 * (neuron.OutValue - desiredOutput) * Utilities.SigmoidDerivative(neuron.NetValue));
        }

        private static void CalculateDelta(Neuron neuron)
        {
            double derivative = Utilities.SigmoidDerivative(neuron.NetValue);
            double sumOfWeightsTimesDelta = neuron.OutputSynapses.Sum(y => y.Weight * y.OutputNeuron.Delta);

            neuron.Delta = derivative * sumOfWeightsTimesDelta;
        }

        private static void CalculateGradient(Synapse synapse)
        {
            synapse.Gradient = synapse.InputNeuron.OutValue * synapse.OutputNeuron.Delta;
        }

    }
}
