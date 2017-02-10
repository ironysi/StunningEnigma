using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    class Program
    {
        #region Escaping static Hell 
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
        #endregion

        private void Run()
        {
            double[] inputs = new double[] { 1, 2, 3, 4, 5 };

            NeuralNet net = new NeuralNet(inputs, 2, 3);

        }
    }
}
