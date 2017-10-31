using System;
using SimplePerceptron.Parameters.Activation;

namespace SimplePerceptron.Network.Neurons
{
    class HiddenNeuron : Neuron
    {
        public HiddenNeuron(IActivation activation) : base(activation)
        {
        }

        public virtual void Learn()
        {
            if (InSinapses.Count == 0 || OutSinapses.Count == 0) return;

            var output = GetOutput();
            Delta = Activation.DerivativeActivate(output) * GetOutSinapsesDeltaWeight();
            UpdateInSinapsesWeights();
        }

        public override double GetOutput()
        {
            if (Math.Abs(LastOutput - (-1)) < 0.000001)
            {
                LastOutput = GetInputSinapsesSignal();
            }
            return LastOutput;
        }
    }
}
