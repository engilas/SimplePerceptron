using SimplePerceptron.Parameters;

namespace SimplePerceptron.Serialization
{
    public class SerializedNetwork
    {
        public NetworkParameters Parameters { get; set; }
        public double[] Weights { get; set; }
    }
}
