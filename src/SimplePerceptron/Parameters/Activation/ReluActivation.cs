namespace SimplePerceptron.Parameters.Activation
{
    public class ReluActivation : IActivation
    {
        private const double Coeff = 0.01;

        public double Activate(double x)
        {
            if (x > 0) return x;
            else return Coeff * x;
        }

        public double DerivativeActivate(double x)
        {
            if (x > 0) return 1;
            else return Coeff;
        }
    }
}
