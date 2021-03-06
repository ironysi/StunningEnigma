﻿using System.Collections.Generic;
using System.Linq;

namespace StunningEnigma.Network
{
    public class Neuron : INeuron
    {
        public List<Synapse> InputSynapses { get; set; } = new List<Synapse>();
        public List<Synapse> OutputSynapses { get; set; } = new List<Synapse>();
        public double OutValue { get; set; }
        public double NetValue { get; set; }
        public double Delta { get; set; }
        public bool IsDropped { get; set; }


        public Neuron(double outValue)
        {
            OutValue = outValue;
        }
        public Neuron(double outValue, List<INeuron> inputNeurons, bool negative = false) : this(outValue)
        {
            if(negative)
            {
                foreach (INeuron inputNeuron in inputNeurons)
                {
                    Synapse synapse = new Synapse(Utilities.DoubleBetween(-1, 1, 42), inputNeuron, this);

                    inputNeuron.OutputSynapses.Add(synapse); // creates output synapse for input neuron 
                    // -->> therefore I never have to initialize output synapses

                    InputSynapses.Add(synapse); // creates input synapse for my neuron
                }
            }
            else
            {
                foreach (INeuron inputNeuron in inputNeurons)
                {
                    Synapse synapse = new Synapse(Utilities.DoubleBetween(0,1, 42), inputNeuron, this);

                    inputNeuron.OutputSynapses.Add(synapse); // creates output synapse for input neuron 
                    // -->> therefore I never have to initialize output synapses

                    InputSynapses.Add(synapse); // creates input synapse for my neuron
                }
            }
        }

        public void Update()
        {
            foreach (Synapse synapse in InputSynapses)
            {
                synapse.Update();
            }
        }

        public void CalculateNetValue()
        {
            NetValue = InputSynapses.Sum(x => x.Weight * x.InputNeuron.OutValue);
        }

        public void ActivationFunction()
        {
            OutValue = Utilities.Sigmoid(NetValue);
        }

    }
}