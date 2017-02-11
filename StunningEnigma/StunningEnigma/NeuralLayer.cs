using System;
using System.Collections.Generic;

namespace StunningEnigma
{
    public class NeuralLayer
    {
        public bool BiasNeuron { get; set; }
        public List<Neuron> Neurons { get; set; } = new List<Neuron>();

        #region Constructors
        /// <summary>
        /// Input Layer
        /// </summary>
        public NeuralLayer(double[] inputs, int numberOfHiddenNeurons, bool biasNeuron)
        {
            BiasNeuron = biasNeuron;

            InitializeNeuronsAndSynapses(inputs.Length, numberOfHiddenNeurons);

            if (biasNeuron == true)
            {
                CreateBiasNeuron(numberOfHiddenNeurons);
            }
        }

        /// <summary>
        /// Hidden layer
        /// </summary>
        public NeuralLayer(int numberOfHiddenNeurons, int numberOfOutputNeurons, bool biasNeuron)
        {
            BiasNeuron = biasNeuron;

            InitializeNeuronsAndSynapses(numberOfHiddenNeurons, numberOfOutputNeurons);

            if (biasNeuron == true)
            {
                CreateBiasNeuron(numberOfOutputNeurons);
            }
        }

        /// <summary>
        /// Output Layer
        /// </summary>
        public NeuralLayer(int numberOfOutputNeurons)
        {
            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                Neuron neuron = new Neuron(new Random(42).NextDouble());
                Neurons.Add(neuron);
            }
        }

        private void InitializeNeuronsAndSynapses(int nrOfNeurons, int nrOfSynapses)
        {
            for (int i = 0; i < nrOfNeurons; i++)
            {
                Neuron neuron = new Neuron(new Random(42).NextDouble());
                List<Synapse> synapses = new List<Synapse>();

                for (int j = 0; j < nrOfSynapses; j++)
                {
                    if (neuron.Synapses.ContainsKey(neuron.NetValue))
                    {
                        synapses.Add(new Synapse(new Random().NextDouble()));

                        neuron.Synapses[neuron.NetValue] = synapses;
                    }
                    else
                    {
                        synapses.Add(new Synapse(new Random(42).NextDouble()));

                        neuron.Synapses.Add(neuron.NetValue, synapses);
                    }
                }
                Neurons.Add(neuron);
            }
        }
        private void CreateBiasNeuron(int numberOfIterations)
        {
            Neuron neuron = new Neuron(1);
            List<Synapse> synapses = new List<Synapse>();

            for (int j = 0; j < numberOfIterations; j++)
            {
                if (neuron.Synapses.ContainsKey(neuron.NetValue))
                {
                    synapses.Add(new Synapse(new Random(42).NextDouble()));

                    neuron.Synapses[neuron.NetValue] = synapses;
                }
                else
                {
                    synapses.Add(new Synapse(new Random(42).NextDouble()));

                    neuron.Synapses.Add(neuron.NetValue, synapses);
                }
            }
            neuron.IsBias = true;
            Neurons.Add(neuron);
        }
        #endregion
    }
}