using System.Collections.Generic;

namespace StunningEnigma
{
    public interface INeuralLayer
    {
        bool BiasNeuron { get; set; }
        List<INeuron> Neurons { get; set; }

    }
}
