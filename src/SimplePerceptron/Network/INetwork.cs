namespace SimplePerceptron.Network
{
    public interface INetwork
    {
        void SetInput(double[] input);
        double[] GetOutput();
        double CalcError(double[][] result, double[][] ideal);
        void Learn(double[] expectedResults);
        double[] GetResult(double[] input);
        void ReinitNetworkWeights();
        double[] Weights { get; }

        string Serialize();
    }
}
