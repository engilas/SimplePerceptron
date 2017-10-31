using SimplePerceptron.Network.Neurons;
using SimplePerceptron.Parameters;

namespace SimplePerceptron.Network.Sinapses
{
    class Sinapse
    {
        private readonly LearnParameters _parameters;

        public double Weight { get; private set; }

        public Neuron From { get; }
        public Neuron To { get;  }

        private double _lastDelta;

        public Sinapse(Neuron from, Neuron to, LearnParameters parameters) : this(from, to, parameters, Parameters.Parameters.GetRandomWeight())
        {
        }

        public Sinapse(Neuron from, Neuron to, LearnParameters parameters, double weight)
        {
            _parameters = parameters;

            From = from;
            To = to;
            Weight = weight;

            To.InSinapses.Add(this);
            From.OutSinapses.Add(this);
        }

        public double Grab()
        {
            return Weight * From.GetOutput();
        }

        public double GetDeltaWeight()
        {
            return To.Delta * Weight;
        }

        public void UpdateWeight()
        {
            var delta = _parameters.LearnSpeed * To.Delta * From.GetOutput() + _parameters.Moment * _lastDelta;
            _lastDelta = delta;
            Weight += delta;
        }

        public void ResetWeight()
        {
            Weight = Parameters.Parameters.GetRandomWeight();
        }
    }
}
