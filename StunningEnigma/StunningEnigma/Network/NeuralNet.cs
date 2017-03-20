using System;
using System.Collections.Generic;
using System.Linq;
using StunningEnigma.NetworkLogic;

namespace StunningEnigma.Network
{
    public class NeuralNet
    {
        public INeuralLayer InputLayer { get; set; }
        public INeuralLayer HiddenLayer { get; set; } // possibly more hidden layers 
        public INeuralLayer OutputLayer { get; set; }
        public double Momentum { get; set; }
        public double LearningRate { get; set; }
        public double BiasSize { get; set; }

        private readonly double[][] inputs;
        private readonly double[][] outputs;
        private readonly double m;

        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, double[][] inputs, double[][] outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;
            m = inputs.Length;

            InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
            HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, BiasSize);
            OutputLayer = new OutputLayer(outputNeuronsCount, HiddenLayer);
        }

        public void Train()
        {
            for (int i = 0; i < 10000; i++)
            {
                List<double> errors = new List<double>();

                for (int j = 0; j < m; j++)
                {
                    FeedForward.FeedForwardPropagation(this, inputs[j]);
                    Backpropagation.BackProp(this, outputs[j]);

                    errors.Add(CalculateError(outputs[j]));
                }
                if (i % 1000 == 0)
                {
                    Console.WriteLine(errors.Sum());

                }

            }
            Console.ReadLine();
        }

        private double CalculateError(double[] targets)
        {
            double e = 0;
            for (int i = 0; i < OutputLayer.Neurons.Count; i++)
            {
                e = targets[i] - OutputLayer.Neurons[i].OutValue;
                Console.WriteLine("Neuron {0}: {1}\t\t target: {2}\t\t error: {3}", i, OutputLayer.Neurons[i].OutValue, targets[i], e);
            }
            Console.WriteLine();
            return Math.Abs(e) / targets.Length;
        } 

    }
}