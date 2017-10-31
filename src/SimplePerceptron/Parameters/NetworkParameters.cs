using SimplePerceptron.Parameters.Activation;

namespace SimplePerceptron.Parameters
{
    public class NetworkParameters
    {
        public LearnParameters LearnParameters;
        public int InputCount;
        public HiddenLayerParameters[] HiddenLayers;
        public int HiddenLayersCount => HiddenLayers.Length;
        public int OutputCount;
        public int OutputLayerNumber => 1 + HiddenLayersCount;
        public ActivationMethod OutputActivationMethod;
        public IActivation OutputActivation => Parameters.Activations[OutputActivationMethod];
    }
}
