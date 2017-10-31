using System;

namespace SimplePerceptron.Parameters.Activation
{
    public class SigmoidActivation : IActivation
    {
        public double Activate(double x)
        {
            var result = 1 / (1 + Math.Exp(-x));
            return result;
        }

        public double DerivativeActivate(double x)
        {
            return (1 - x) * x;
        }
    }
}
