using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StunningEnigma
{
    public interface INeuralLayer
    {
        bool BiasNeuron { get; set; }
        List<INeuron> Neurons { get; set; }

    }
}
