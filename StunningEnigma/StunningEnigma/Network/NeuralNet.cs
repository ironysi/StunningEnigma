﻿using System;
using System.Collections.Generic;
using System.Linq;
using StunningEnigma.NetworkLogic;

namespace StunningEnigma.Network
{
    public class NeuralNet
    {
        public INeuralLayer InputLayer { get; set; }
        public INeuralLayer HiddenLayer { get; set; } // possibly more hidden layers 
        public INeuralLayer DropoutLayer { get; set; }
        public INeuralLayer OutputLayer { get; set; }
        public double Momentum { get; set; }
        public double LearningRate { get; set; }
        public double BiasSize { get; set; }

        private struct MiniBatch
        {
            public double[][] inputs;
            public double[][] outputs;
            public int batchSize;
        }

        public double[][] TrainingInputs { get; set; }
        public double[][] TrainingOutputs { get; set; }

        public double[][] TestingInputs { get; set; }
        public double[][] TestingOutputs { get; set; }


        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int dropoutLayerCount, int outputNeuronsCount, double biasSize = 1)
        {
            BiasSize = biasSize;
            InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
            HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, BiasSize);
            DropoutLayer = new HiddenLayer(dropoutLayerCount, true, HiddenLayer, BiasSize);
            OutputLayer = new OutputLayer(outputNeuronsCount, DropoutLayer);
        }

        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, double biasSize = 1)
        {
            BiasSize = biasSize;
            InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
            HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, BiasSize);          
            OutputLayer = new OutputLayer(outputNeuronsCount, HiddenLayer);
        }

        public void Train(int batchSize)
        {
            for (int i = 0; i < 10000; i++)
            {
                MiniBatch batch = CreateBatch(TrainingInputs, TrainingOutputs, batchSize);

                double[][] inputs = batch.inputs;
                double[][] outputs = batch.outputs;

                for (int j = 0; j < batchSize; j++)
                {
                    FeedForward.FeedForwardPropagation(this, inputs[j]);
                    Backpropagation.BackProp(this, outputs[j]);
                    //ADAM.BackProp(this, outputs[j]);
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
                //ADAM.BackProp(this, TestingOutputs[j]);
                PrintOutputLayer(TestingOutputs[j], CalculateError(TestingOutputs[j]));
            }

            Console.ReadLine();
        }

        private static MiniBatch CreateBatch(double[][] inputs, double[][] outputs, int batchSize)
        {
            Random rnd = new Random();

            MiniBatch batch = new MiniBatch();
            batch.inputs = new double[batchSize][];
            batch.outputs = new double[batchSize][];
            batch.batchSize = batchSize;

            for (int i = 0; i < batchSize; i++)
            {
                int randomNumber = rnd.Next(0, batch.batchSize);

                batch.inputs[i] = inputs[randomNumber];
                batch.outputs[i] = outputs[randomNumber];
            }

            return batch;
        }


        private double CalculateError(double[] targets)
        {
            double e = 0;

            for (int i = 0; i < OutputLayer.Neurons.Count; i++)
            {
                e = +Math.Pow((OutputLayer.Neurons[i].OutValue - targets[i]), 2);
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