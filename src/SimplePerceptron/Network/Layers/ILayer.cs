using SimplePerceptron.Network.Neurons;

namespace SimplePerceptron.Network.Layers
{
    interface ILayer
    {
        int Length { get; }
        Neuron this[int index] { get; }
    }
}
