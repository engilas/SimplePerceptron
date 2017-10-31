namespace SimplePerceptron.Parameters.Activation
{
    public interface IActivation
    {
        double Activate(double input);
        double DerivativeActivate(double input);
    }
}
