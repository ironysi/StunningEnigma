using System;
using StunningEnigma.NetworkLogic;

namespace StunningEnigma.Network
{
    public class NeuralNet
    {
        public INeuralLayer InputLayer { get; set; }
        public INeuralLayer HiddenLayer { get; set; }
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


        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int dropoutLayerCount, int outputNeuronsCount, bool negative, double biasSize = 1)
        {
            BiasSize = biasSize;

            if (negative)
            {
                InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
                HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, negative, BiasSize);
                DropoutLayer = new HiddenLayer(dropoutLayerCount, true, HiddenLayer, negative, BiasSize);
                OutputLayer = new OutputLayer(outputNeuronsCount, DropoutLayer, negative);
            }
            else
            {
                InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
                HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, negative, BiasSize);
                DropoutLayer = new HiddenLayer(dropoutLayerCount, true, HiddenLayer, negative, BiasSize);
                OutputLayer = new OutputLayer(outputNeuronsCount, DropoutLayer, negative);
            }
        }

        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, double biasSize = 1, bool negative = false)
        {
            BiasSize = biasSize;

            if (negative)
            {
                InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
                HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, negative, BiasSize);
                OutputLayer = new OutputLayer(outputNeuronsCount, HiddenLayer, negative);
            }
            else
            {
                InputLayer = new InputLayer(inputNeuronsCount, true, BiasSize);
                HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer, negative, BiasSize);
                OutputLayer = new OutputLayer(outputNeuronsCount, HiddenLayer, negative);
            }

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
                    FeedForward.FeedForwardPropagation(this, inputs[j], true);
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
            if (layerError > 0.3)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error is:{0}\n", layerError);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine("Error is:{0}\n", layerError);
            }

        }
    }
}