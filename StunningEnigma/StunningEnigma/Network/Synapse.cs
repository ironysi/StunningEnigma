﻿namespace StunningEnigma.Network
{
    public class Synapse
    {
        /// <summary>
        /// Neurons that send the signal
        /// </summary>
        public INeuron InputNeuron { get; set; }

        /// <summary>
        /// Neurons that recieve the signal
        /// </summary>
        public Neuron OutputNeuron { get; set; }

        public double Weight { get; set; }
        public double NewWeight { get; set; }
        public double Gradient { get; set; }
        public double PreviousWeightChange { get; set; }
        public double PrevM { get; set; }
        public double PrevV { get; set; }

        public Synapse(double weight, INeuron inputNeuron, Neuron outputNeuron)
        {
            Weight = weight;
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
            PreviousWeightChange = 0;
            PrevM = 0;
            PrevV = 0;
        }

        public void Update()
        {
            Weight = NewWeight;
        }
    }
}