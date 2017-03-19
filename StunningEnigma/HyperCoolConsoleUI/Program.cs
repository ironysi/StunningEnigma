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
            p.Run();
        }

        private void Run()
        {
            string[] categories = new string[] { "numeric", "numeric", "numeric", "numeric", "categorical" };
            Data data = new Data("Iris.txt", ',', 4, 3, 0.8, categories);
            NeuralNet net = new NeuralNet(3,3,2, data.GetLearningInputs());
            
        }
    }
}
