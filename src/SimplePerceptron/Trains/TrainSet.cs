namespace SimplePerceptron.Trains
{
    public class TrainSet
    {
        public double[] Input;
        public double[] Output;

        public int InputCount => Input.Length;
        public int OutputCount => Output.Length;

        public TrainSet(double[] input, double[] output)
        {
            Input = input;
            Output = output;
        }
    }
}
