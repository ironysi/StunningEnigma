namespace StunningEnigma
{
    public class Synapse
    {
        private string id;
        public double Value { get; set; }
        public double Affect { get; set; }

        public Synapse(double value)
        {
            Value = value;
        }
    }
}