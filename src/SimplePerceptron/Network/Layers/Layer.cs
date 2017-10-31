using System.Collections.Generic;
using System.Linq;
using SimplePerceptron.Network.Neurons;

namespace SimplePerceptron.Network.Layers
{
    abstract class Layer<T> : ILayer
        where T : Neuron
    {
        public readonly List<T> Neurons;

        protected Layer(IEnumerable<T> neurons)
        {
            Neurons = neurons.ToList();
        }

        public int Length => Neurons.Count;

        public Neuron this[int index] => Neurons[index];
    }
    
}
