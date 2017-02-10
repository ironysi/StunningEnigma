namespace StunningEnigma
{
    public class NeuralNet
    {
        public NeuralLayer InputLayer { get; set; }
        public NeuralLayer HiddenLayer { get; set; }
        public NeuralLayer OutputLayer { get; set; }
        public double NNError { get; set; }

        public NeuralNet(double[] inputs, int hiddenNeuronsCount, int outputNeuronsCount)
        {
            InputLayer = new NeuralLayer(inputs, hiddenNeuronsCount, true);
            HiddenLayer = new NeuralLayer(hiddenNeuronsCount, outputNeuronsCount, true);
            OutputLayer = new NeuralLayer(outputNeuronsCount);
        }

    }
}