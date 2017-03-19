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
        public INeuron OutputNeuron { get; set; }

        public double Weight { get; set; }
        public double Delta { get; set; }

        public Synapse(double weight, INeuron inputNeuron, INeuron outputNeuron)
        {
            Weight = weight;
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
        }

    }
}