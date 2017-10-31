using System;
using System.Collections.Generic;
using SimplePerceptron.Network.Neurons;

namespace SimplePerceptron.Network.Layers
{
    class InputLayer : Layer<InputNeuron>
    {
        public InputLayer(IEnumerable<InputNeuron> neurons) : base(neurons)
        {
        }

        public void SetInput(double[] input)
        {
            if (input.Length != Length)
                throw new ArgumentException("Input data count must be equal to the input layer count");

            for (int i = 0; i < Length; i++)
            {
                Neurons[i].Input = input[i];
            }
        }
    }
    
}
