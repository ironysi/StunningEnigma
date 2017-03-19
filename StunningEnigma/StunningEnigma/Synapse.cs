namespace StunningEnigma
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

        public double Value { get; set; }
        public double NewValue { get; set; }
        public double Delta { get; set; }

        public Synapse(double value)
        {
            Value = value;
        }

        public void ApplyChanges()
        {
            Value = NewValue;
        }
    }
}