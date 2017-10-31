using SimplePerceptron.Parameters.Activation;

namespace SimplePerceptron.Parameters
{
    public class HiddenLayerParameters
    {
        public int Count;
        public bool HasBias;
        public ActivationMethod ActivationMethod;
        public IActivation Activation => Parameters.Activations[ActivationMethod];
    }
}
