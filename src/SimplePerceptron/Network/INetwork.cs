namespace SimplePerceptron.Network
{
    public interface INetwork
    {
        double LearnSpeed { get; set; }
        double Moment { get; set; }

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
