using DataSet;
using StunningEnigma;

namespace HyperCoolConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            string[] categories = new string[] { "numeric", "numeric", "numeric", "numeric", "categorical" };
            Data data = new Data("Iris.txt", ',', 4, 3, 0.8, categories);
            NeuralNet net = new NeuralNet(4,3,3);

            net.Train(data.GetLearningInputs(), data.GetLearningOutputs());

        }
    }
}
