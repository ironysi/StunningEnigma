﻿using System;
using System.Linq;
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
                CalculateGradient(neuron);
             //   PerformGradientCheck(neuron);
                GradientDescent(neuron, learningRate, momentum);
            }
        }

        private static void BackpropForOutputLayer(INeuralLayer layer, double[] outputs, double learningRate, double momentum)
        {
            for (int i = 0; i < layer.Neurons.Count; i++)
            {
                CalculateGradient((Neuron)layer.Neurons[i], outputs[i]);
              //  PerformGradientCheck((Neuron)layer.Neurons[i]);
                GradientDescent((Neuron)layer.Neurons[i], learningRate, momentum);
            }
        }


        /// <summary>
        /// Update weights / learning method
        /// </summary>
        private static void GradientDescent(Neuron neuron, double learningRate, double momentum)
        {
            double previosError = neuron.Error;

            foreach (Synapse synapse in neuron.InputSynapses)
            {
                //previosError = synapse.Delta;
                synapse.Delta = learningRate * neuron.Gradient * synapse.InputNeuron.OutValue; // calculate error delta for particular synapse
                synapse.Weight += synapse.Delta + momentum * previosError; // changes weight of that synapse based on error
            }
        }

        private static void VanillaGradientDescent(Neuron neuron, double learningRate)
        {
            foreach (Synapse synapse in neuron.InputSynapses)
            {
                double update = learningRate * neuron.Gradient;
                synapse.Weight = synapse.Weight - update;
            }
        }

        private static void GradientDescentMomentum(Neuron neuron, double learningRate, double momentum)
        {

        }

        private static void ADAM(Neuron neuron)
        {

        }


        #region Gradient Descent 

        private static void CalculateGradient(Neuron neuron, double? target = null)
        {
            if (target == null)
                neuron.Gradient = neuron.OutputSynapses.Sum(a => a.Delta * a.Weight)
                    * Utilities.SigmoidDerivative(neuron.OutValue); //Hidden neurons
            else
                neuron.Gradient = CalculateError(target.Value, neuron)
                    * Utilities.SigmoidDerivative(neuron.OutValue); //output neurons
        }

        private static double CalculateError(double targetValue, Neuron neuron)
        {
            neuron.Error = targetValue - neuron.OutValue;

            return neuron.Error;
        }

        private static void PerformGradientCheck(Neuron neuron)
        {
            const double epsilon = 0.0001;

            double parameter1 =
                Utilities.Sigmoid(neuron.InputSynapses.Sum(x => x.Weight * x.InputNeuron.OutValue) + epsilon);
            double parameter2 =
                Utilities.Sigmoid(neuron.InputSynapses.Sum(x => x.Weight * x.InputNeuron.OutValue) - epsilon);

            neuron.GradientCheck = (parameter1 - parameter2) / (2 * epsilon);
        }
        #endregion
    }
}
