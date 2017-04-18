using System;
using System.Linq;
using StunningEnigma.Network;

namespace StunningEnigma.NetworkLogic
{
    public static class ADAM
    {
        private static readonly double betaOne = 0.9;
        private static readonly double betaTwo = 0.999;
        private static readonly double learningRate = 0.001;
        private static readonly double epsilon = 0.00000001;



        public static void BackProp(NeuralNet net, double[] targetOutputs)
        {
            BackpropOutputLayer(net.OutputLayer, targetOutputs);

            if (net.DropoutLayer != null)
                BackpropHiddenLayer(net.DropoutLayer);

            BackpropHiddenLayer(net.HiddenLayer);
        }

        private static void BackpropHiddenLayer(INeuralLayer layer)
        {
            foreach (Neuron neuron in layer.Neurons.OfType<Neuron>())
            {
                CalculateDelta(neuron);                          // calculates "delta" error of neuron

                neuron.InputSynapses.ForEach(CalculateGradient); // calculates gradients for all input synapses

                for (int j = 1; j < neuron.InputSynapses.Count + 1; j++)
                {
                    WeightUpdate(neuron.InputSynapses[j - 1], j);
                }
            }

            foreach (Neuron neuron in layer.Neurons.OfType<Neuron>()) // batch update
            {
                neuron.Update();
            }
        }

        private static void BackpropOutputLayer(INeuralLayer layer, double[] outputs)
        {
            for (int i = 0; i < layer.Neurons.Count; i++)
            {
                Neuron neuron = (Neuron)layer.Neurons[i];

                CalculateDelta((Neuron)layer.Neurons[i], outputs[i]);
                neuron.InputSynapses.ForEach(CalculateGradient);

                for (int j = 1; j < neuron.InputSynapses.Count + 1; j++)
                {
                    WeightUpdate(neuron.InputSynapses[j - 1], j);
                }
            }

            foreach (Neuron neuron in layer.Neurons.OfType<Neuron>()) // batch update
            {
                neuron.Update();
            }
        }

        private static double CalcM(Synapse synapse)
        {
            return (betaOne * synapse.PrevM) + (1 - betaOne) * synapse.Gradient;
        }
        private static double CalcCurrentM(double M, int t)
        {
            return M / (1 - Math.Pow(betaOne, t));
        }


        private static double CalcV(Synapse synapse)
        {
            return (betaTwo * synapse.PrevV) + (1 - betaTwo) * Math.Pow(synapse.Gradient, 2);
        }

        private static double CalcCurrentV(double V, int t)
        {
            return V / (1 - Math.Pow(betaTwo, t));
        }

        private static void WeightUpdate(Synapse synapse, int t)
        {
            double M = CalcCurrentM(CalcM(synapse), t);
            double V = CalcCurrentV(CalcV(synapse), t);

            synapse.NewWeight = synapse.Weight - ((learningRate * M) / (Math.Sqrt(V) + epsilon));

            synapse.PrevV = V;
            synapse.PrevM = M;
        }



        //Gradient sh*t down from here

        private static void CalculateDelta(Neuron neuron, double desiredOutput)
        {
            neuron.Delta = (-1 * (neuron.OutValue - desiredOutput) * Utilities.SigmoidDerivative(neuron.NetValue));
        }

        private static void CalculateDelta(Neuron neuron)
        {
            double derivative = Utilities.SigmoidDerivative(neuron.NetValue);
            double sumOfWeightsTimesDelta = neuron.OutputSynapses.Sum(y => y.Weight * y.OutputNeuron.Delta);

            neuron.Delta = derivative * sumOfWeightsTimesDelta;
        }

        private static void CalculateGradient(Synapse synapse)
        {
            synapse.Gradient = synapse.InputNeuron.OutValue * synapse.OutputNeuron.Delta;
        }

    }
}
