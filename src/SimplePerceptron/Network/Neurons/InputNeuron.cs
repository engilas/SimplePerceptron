namespace SimplePerceptron.Network.Neurons
{
    class InputNeuron : Neuron
    {
        public double Input { get; set; }

        public InputNeuron() : base(null)
        {
        }

        public override double GetOutput()
        {
            return Input;
        }
    }
}
