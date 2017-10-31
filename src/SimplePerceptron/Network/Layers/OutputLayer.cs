using System;
using System.Collections.Generic;
using SimplePerceptron.Network.Neurons;

namespace SimplePerceptron.Network.Layers
{
    class OutputLayer : Layer<OutputNeuron>
    {
        public OutputLayer(IEnumerable<OutputNeuron> neurons) : base(neurons)
        {
        }

        public double[] GetOutput()
        {
            double[] result = new double[Length];
            for (int i = 0; i < Length; ++i)
            {
                result[i] = Neurons[i].GetOutput();
            }
            return result;
        } 

        public void Learn(double[] ideals)
        {
            if (ideals.Length != Length)
                throw new ArgumentException("Input count not equal to neurons count");

            for (int i = 0; i < Length; i++)
            {
                Neurons[i].Learn(ideals[i]);
            }
        }
    }
}
