using System.Collections.Generic;
using SimplePerceptron.Network.Sinapses;
using SimplePerceptron.Parameters.Activation;

namespace SimplePerceptron.Network.Neurons
{
    abstract class Neuron
    {
        protected readonly IActivation Activation;
        
        public double Delta { get; set; }
        public double LastOutput { get; set; } = -1;
        public List<Sinapse> InSinapses { get; set; } = new List<Sinapse>();
        public List<Sinapse> OutSinapses { get; set; } = new List<Sinapse>();

        protected Neuron(IActivation activation)
        {
            Activation = activation;
        }

        public abstract double GetOutput();

        protected double GetInputSinapsesSignal()
        {
            double sum = 0;
            for (int i = 0; i < InSinapses.Count; i++)
            {
                sum += InSinapses[i].Grab();
            }
            return Activation.Activate(sum);
        }

        protected double GetOutSinapsesDeltaWeight()
        {
            double sum = 0;
            for (int i = 0; i < OutSinapses.Count; i++)
            {
                sum += OutSinapses[i].GetDeltaWeight();
            }
            return sum;
        }

        protected void UpdateInSinapsesWeights()
        {
            for (int i = 0; i < InSinapses.Count; i++)
            {
                InSinapses[i].UpdateWeight();
            }
        }
    }
}
