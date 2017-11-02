namespace SimplePerceptron.Network
{
    public interface INetwork
    {
        void UpdateLearnSpeed(double value);
        void UpdateMoment(double value);

        void SetInput(double[] input);
        double[] GetOutput();

        double CalcSetError(double[][] result, double[][] ideal);
        double CalcIterationError(double[] result, double[] ideal);

        void Learn(double[] expectedResults);
        double[] GetResult(double[] input);
        void ReinitNetworkWeights();
        double[] Weights { get; }

        double[] Iterate(double[] input);

        string Serialize();
    }
}
