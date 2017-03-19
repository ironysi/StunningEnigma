using System;
using System.Collections.Generic;
using System.Linq;

namespace StunningEnigma
{
    public class NeuralNet
    {
        public INeuralLayer InputLayer { get; set; }
        public INeuralLayer HiddenLayer { get; set; } // possibly more hidden layers 
        public INeuralLayer OutputLayer { get; set; }

        public double NNError { get; set; }

        public NeuralNet(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {
            InputLayer = new InputLayer(inputNeuronsCount, false, null, hiddenNeuronsCount);
            HiddenLayer = new HiddenLayer(hiddenNeuronsCount, false, inputNeuronsCount, outputNeuronsCount);
            OutputLayer = new OutputLayer(outputNeuronsCount,false,hiddenNeuronsCount);

            FeedForward.FeedForwardPropagation(this);
        }
    }
}