using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;


namespace StunningEnigma
{
    public class Neuron
    {
        public List<Synapse> InputSynapses { get; set; } = new List<Synapse>();
        public List<Synapse> OutputSynapses { get; set; } = new List<Synapse>();
        public int NeuronId { get; set; }
        public double NetValue { get; set; }
        public double OutValue { get; set; }
        public double Error { get; set; }

        public Neuron(double netValue)
        {
            NetValue = netValue;
        }

        /// <summary>
        ///  Calculates OutputValue of neuron
        /// </summary>
        public void Pulse()
        {
            ActivationFunction(NetValue);
        }

        public void ActivationFunction(double value)
        {
            OutValue = Utilities.Sigmoid(value);
        }

        public void CalculateError(double targetValue)
        {
            Error = 0.5 * Math.Pow((targetValue - OutValue), 2);
        }


    }
}