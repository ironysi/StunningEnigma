using System.Collections.Generic;
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

        private double[][] inputs;
        private double[][] outputs;

        public double NNError { get; set; }

        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, double[][] inputs, double[][] outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;

            InputLayer = new InputLayer(inputNeuronsCount, true, inputs[0]);
            HiddenLayer = new HiddenLayer(hiddenNeuronsCount, true, InputLayer);
            OutputLayer = new OutputLayer(outputNeuronsCount, HiddenLayer);

            FeedForward.FeedForwardPropagation(this);
        }

        public void Train()
        {
            for (int i = 0; i < 1000; i++)
            {
                
            }
        }
    }
}