using System;
using System.Collections.Generic;
using SimplePerceptron.Parameters.Activation;

namespace SimplePerceptron.Parameters
{
    static class Parameters
    {
        public static bool StopLearn = false;
        public static bool ReInitWeights = false;
        public static bool RestartLearn = false;

        public static Dictionary<ActivationMethod, IActivation> Activations = new Dictionary<ActivationMethod, IActivation>
        {
            [ActivationMethod.Relu] = new ReluActivation(),
            [ActivationMethod.Sigmoid] = new SigmoidActivation()
        };

        static Random r = new Random();

        public static double GetRandomWeight()
        {
            return r.NextDouble() + r.Next(-3, 3);
        }
    }
}
