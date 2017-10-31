namespace SimplePerceptron.Network.Neurons
{
    class BiasNeuron : HiddenNeuron
    {
        public BiasNeuron() : base(null)
        {
        }

        public override double GetOutput()
        {
            return 1;
        }

        public override void Learn()
        {
        }
    }
}
