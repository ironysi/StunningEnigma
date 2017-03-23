using System;
using System.Linq;
using System.Runtime.Remoting;
using StunningEnigma.Network;

namespace StunningEnigma.NetworkLogic
{
    public static class Backpropagation
    {
        public static void BackProp(NeuralNet net, double[] targetOutputs)
        {
            BackpropForOutputLayer(net.OutputLayer, targetOutputs, net.LearningRate, net.Momentum);
            BackpropForHiddenLayer(net.HiddenLayer, net.LearningRate, net.Momentum);
        }


        private static void BackpropForHiddenLayer(INeuralLayer layer, double learningRate, double momentum)
        {
            //loops thru all neurons except BIAS 
            foreach (Neuron neuron in layer.Neurons.OfType<Neuron>())
            {
                CalculateDelta(neuron);                          // calculates "delta" error of neuron

                neuron.InputSynapses.ForEach(CalculateGradient); // calculates gradients for all input synapses

                //neuron.InputSynapses.ForEach(PerformGradientCheck);

                neuron.InputSynapses.ForEach(y => ChangeWeight(y, learningRate, momentum)); //changes weigths 
            }
        }

        private static void BackpropForOutputLayer(INeuralLayer layer, double[] outputs, double learningRate, double momentum)
        {
            for (int i = 0; i < layer.Neurons.Count; i++)
            {
                Neuron neuron = (Neuron)layer.Neurons[i];

                CalculateDelta((Neuron)layer.Neurons[i], outputs[i]);

                neuron.InputSynapses.ForEach(CalculateGradient);
                neuron.InputSynapses.ForEach(y => ChangeWeight(y, learningRate, momentum));

                //neuron.InputSynapses.ForEach(PerformGradientCheck);
            }
        }

        private static void ChangeWeight(Synapse synapse, double learningRate, double momentum)
        {
            // LearningRate * Gradient  + momentum * previous weight adjustment 
            double adjustment = (learningRate * synapse.Gradient) + (momentum * synapse.PreviousWeightChange);
            synapse.PreviousWeightChange = adjustment;

            synapse.Weight = synapse.Weight + adjustment;
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

        private static void PerformGradientCheck(Synapse synapse)
        {
            const double epsilon = 0.0001;

            double parameter1 =
                Utilities.SigmoidDerivative(synapse.Weight + epsilon);
            double parameter2 =
                Utilities.SigmoidDerivative(synapse.Weight - epsilon);

            double x = (parameter1 - parameter2) / (2 * epsilon);
            double y = synapse.Gradient;
        }

    }
}
