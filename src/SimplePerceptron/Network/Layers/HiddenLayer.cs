using System.Collections.Generic;
using SimplePerceptron.Network.Neurons;

namespace SimplePerceptron.Network.Layers
{
    class HiddenLayer : Layer<HiddenNeuron>
    {
        public int LayerNumber { get; }

        public HiddenLayer(int layerNum, IEnumerable<HiddenNeuron> neurons) : base(neurons)
        {
            LayerNumber = layerNum;
        }

        public void ResetLastOutput()
        {
            for (int i = 0; i < Neurons.Count; ++i)
            {
                Neurons[i].LastOutput = - 1;
            }
        }

        public void Learn()
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].Learn();
            }
        } 
    }
}
