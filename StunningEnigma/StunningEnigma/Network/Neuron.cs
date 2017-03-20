using System;
using System.Collections.Generic;
using System.Linq;

namespace StunningEnigma.Network
{
    public class Neuron : INeuron
    {
        public List<Synapse> InputSynapses { get; set; } = new List<Synapse>();
        public List<Synapse> OutputSynapses { get; set; } = new List<Synapse>();
        public int NeuronId { get; set; }
        public double OutValue { get; set; }
        public double Gradient { get; set; }
        public double Error { get; set; }


        public Neuron(double outValue)
        {
            OutValue = outValue;
        }
        public Neuron(double outValue, List<INeuron> inputNeurons) : this(outValue)
        {
            foreach (INeuron inputNeuron in inputNeurons)
            {
                Synapse synapse = new Synapse(Utilities.DoubleBetween(0,1), inputNeuron, this);

                inputNeuron.OutputSynapses.Add(synapse); // creates output synapse for input neuron 
                                                         // -->> therefore I never have to initialize output synapses

                InputSynapses.Add(synapse); // creates input synapse for my neuron
            }
        }

        // Forward propagation
        public void ActivationFunction()
        {
            OutValue = Utilities.Sigmoid(InputSynapses.Sum(x => x.Weight * x.InputNeuron.OutValue));
        }


        #region BackProp

        //public double CalculateGradient(double? target = null)
        //{
        //    if (target == null)
        //        return Gradient = OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * Utilities.SigmoidDerivative(OutValue);

        //    return Gradient = CalculateError(target.Value) * Utilities.SigmoidDerivative(OutValue);
        //}

        //public double CalculateError(double targetValue)
        //{
        //    Error = 0.5 * Math.Pow((targetValue - OutValue), 2);

        //    return Error;
        //}


        #endregion


    }
}