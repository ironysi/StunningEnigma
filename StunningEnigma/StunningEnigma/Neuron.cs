using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;


namespace StunningEnigma
{
    public class Neuron : INeuron
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
        public Neuron(double netValue, List<INeuron> inputNeurons):this(netValue)
        {
            foreach (INeuron inputNeuron in inputNeurons)
            {
                Synapse synapse = new Synapse(Utilities.DoubleBetween(0, 1), inputNeuron, this);

                inputNeuron.OutputSynapses.Add(synapse); // creates output synapse for input neuron 
                                                         // -->> therefore I never have to initialize output synapses

                InputSynapses.Add(synapse); // creates input synapse for my neuron
            }
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