using SimplePerceptron.Parameters.Activation;

namespace SimplePerceptron.Network.Neurons
{
    class OutputNeuron : Neuron
    {
        public OutputNeuron(IActivation activation) : base(activation)
        {
        }

        public void Learn(double ideal)
        {
            if (InSinapses.Count == 0) return;

            var output = GetOutput();
            Delta = (ideal - output) * Activation.DerivativeActivate(output);
            UpdateInSinapsesWeights();
        }

        public override double GetOutput() => GetInputSinapsesSignal();
    }
}
