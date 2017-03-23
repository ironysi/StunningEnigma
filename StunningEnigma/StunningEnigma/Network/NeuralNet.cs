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

        public double[][] TrainingInputs { get; set; }
        public double[][] TrainingOutputs { get; set; }

        public double[][] TestingInputs { get; set; }
        public double[][] TestingOutputs { get; set; }


        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {
            InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
            HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, BiasSize);
            OutputLayer = new OutputLayer(outputNeuronsCount, HiddenLayer);
        }

        public void Train(int batchSize)
        {
            double[][] inputs = CreateBatch(TrainingInputs, batchSize);
            double[][] outputs = CreateBatch(TrainingOutputs, batchSize);

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < batchSize; j++)
                {
                    FeedForward.FeedForwardPropagation(this, inputs[j]);
                    Backpropagation.BackProp(this, outputs[j]);
                }
            }
        }

        public void Test()
        {
            double m = TestingInputs.Length;

            for (int j = 0; j < m; j++)
            {
                FeedForward.FeedForwardPropagation(this, TestingInputs[j]);
                Backpropagation.BackProp(this, TestingOutputs[j]);

                PrintOutputLayer(TestingOutputs[j], CalculateError(TestingOutputs[j]));
            }
            
            Console.ReadLine();
        }

        private double[][] CreateBatch(double[][] data, int batchSize)
        {
            Random rnd = new Random();
            double[][] returnData = new double[batchSize][];

            for (int i = 0; i < batchSize; i++)
            {
                int randomNumber = rnd.Next(0, data.Length);
                returnData[i] = data[randomNumber];
            }

            return returnData;
        }


        private double CalculateError(double[] targets)
        {
            double e = 0;

            for (int i = 0; i < OutputLayer.Neurons.Count; i++)
            {
                e =+ Math.Pow((OutputLayer.Neurons[i].OutValue - targets[i]), 2);
            }

            return 0.5 * e;
        }


        private void PrintOutputLayer(double[] targets, double layerError)
        {
            for (int i = 0; i < OutputLayer.Neurons.Count; i++)
            {
                double error = Math.Abs(OutputLayer.Neurons[i].OutValue - targets[i]);

                Console.WriteLine("Neuron {0}: {1}\t\t target: {2}\t\t error: {3}"
                    , i, OutputLayer.Neurons[i].OutValue, targets[i], error);
            }
            Console.WriteLine("Layer error is:{0}\n", layerError);
        }
    }
}