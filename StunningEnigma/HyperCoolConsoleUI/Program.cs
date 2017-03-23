using System;
using DataSet;
using StunningEnigma;
using StunningEnigma.Network;

namespace HyperCoolConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            while (true)
            {
                Console.WriteLine(@"******** Welcome! ********");
                Console.WriteLine("Please select which Neural Network you want to run.\n");
                Console.WriteLine("1.\tIris NN");
                Console.WriteLine("2.\tBreast Cancer NN");
                Console.WriteLine("3.\tXOR Gate");
                Console.WriteLine("4.\tWine NN");
                Console.WriteLine("5.\tIris Grid Search NN");

                int c;
                int.TryParse(Console.ReadLine(), out c);

                switch (c)
                {
                    case 1:
                        p.RunIris(0.001);
                        break;
                    case 2:
                        p.RunBCancer(0.001, 0.1, true);
                        break;
                    case 3:
                        p.RunANDGate(0.01);
                        break;
                    case 4:
                        p.RunWine(0.001, 0.1);
                        break;
                    case 5:
                        // p.GridSearch(new int[] { 8, 16, 32, 64 }, new double[] { 0.1, 0.01, 0.001 });
                        break;
                    case 6:
                        p.Test();
                        break;
                    default:
                        Console.WriteLine("Pick a number...");
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void Test()
        {
            Console.WriteLine("Hello world, is that You?");
        }

        private void RunWine(double desiredErrorPercentage, double learningRate = 0.1, bool doYouWantToPrint = false)
        {
            string[] categories =
            {
                "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric","numeric",
                "numeric", "categorical"
            };
            // Output data (quality is 6 numbers)
            Data data = new Data("winequality-red.csv", ';', 11, 6, 0.8, categories);
            // batchsize: 8 and LR: 0.1 
            NeuralNet net = new NeuralNet(11, 14, 6);

            net.Momentum = 0.7;
            net.LearningRate = 0.1;

            SetData(net, data);

            net.Train(8);
            net.Test();
        }


        private void RunBCancer(double desiredErrorPercentage, double learningRate = 0.1, bool doYouWantToPrint = false)
        {
            string[] columnTypes =
            {
                "numeric", "categorical", "numeric", "numeric", "numeric", "numeric", "numeric",
                "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric",
                "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric",
                "numeric", "numeric", "numeric", "numeric", "numeric", "numeric", "numeric"
            };
            int[] outputColumns = { 1 };
            int[] ignoredColumns = { 0 };

            Data data = new Data("Breasts.txt", ',', 30, 1, 0.8, columnTypes, outputColumns, ignoredColumns);

            // batchsize: 8 and LR: 0.1 

            NeuralNet net = new NeuralNet(30, 30, 1);
            net.LearningRate = 0.1;
            net.Momentum = 0.3;

            net = SetData(net, data);

            net.Train(8);
            net.Test();
            
        }
        private void RunIris(double desiredErrorPercentage, double learningRate = 0.1, bool doYouWantToPrint = false)
        {
            string[] categories = { "numeric", "numeric", "numeric", "numeric", "categorical" };
            Data data = new Data("Iris.txt", ',', 4, 3, 0.8, categories);

            // batchsize: 16 and LR: 0.1 
            NeuralNet net = new NeuralNet(4, 3, 3);

            net.BiasSize = 2;
            net.LearningRate = 0.1;
            net.Momentum = 0.5;

            net = SetData(net, data);

            net.Train(16);
            net.Test();
        }


        private void RunANDGate(double desiredErrorPercentage, double learningRate = 0.1, bool doYouWantToPrint = false)
        {

            double[][] input = new double[4][];
            input[0] = new double[] { 0, 0 };
            input[1] = new double[] { 1, 1 };
            input[2] = new double[] { 1, 0 };
            input[3] = new double[] { 0, 1 };

            double[][] output = new double[4][];
            output[0] = new double[] { 1 };
            output[1] = new double[] { 1 };
            output[2] = new double[] { 0 };
            output[3] = new double[] { 0 };

            NeuralNet net = new NeuralNet(2, 2, 1);
            net.LearningRate = 0.4;
            net.Momentum = 0.9;
            net.BiasSize = 6;

            net.TrainingInputs = input;
            net.TrainingOutputs = output;

            net.TestingInputs = input;
            net.TestingOutputs = output;

            net.Train(4);
            net.Test();
        }

        private NeuralNet SetData(NeuralNet net, Data data)
        {
            net.TrainingInputs = data.GetLearningInputs();
            net.TrainingOutputs = data.GetLearningOutputs();

            net.TestingInputs = data.GetTestingInputs();
            net.TestingOutputs = data.GetTestingOutputs();

            return net;
        }


        //private void GridSearch(int[] batchSizes, double[] learningRates)
        //{
        //    for (int i = 0; i < batchSizes.Length; i++)
        //    {
        //        for (int k = 0; k < learningRates.Length; k++)
        //        {
        //            Console.WriteLine("\nBatch size: {0}\t\t\t\t\t\t\tLearning rate: {1}", batchSizes[i],
        //                learningRates[k]);

        //            string[] categories = { "numeric", "numeric", "numeric", "numeric", "categorical" };
        //            Data data = new Data("Iris.txt", ',', 4, 3, 0.8, categories);

        //            // batchsize: 16 and LR: 0.1 
        //            NeuralNet net = new NeuralNet(4, 3, 3, learningRates[k], batchSizes[i]);

        //            net.RunUntilDesiredError(data, 1500);
        //        }
        //    }
        //}
    }
}
