using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimplePerceptron.Network;
using SimplePerceptron.Parameters;
using SimplePerceptron.Parameters.Activation;
using SimplePerceptron.Trains;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //learn NN to draw line ( y = x )

            //network creation
            var network = CreateNetwork();

            //create network teacher and train set
            var teacher = new NetworkTeacher(network, CreateTrainSet().ToArray(), new TeacherParameters{});
            var dt = DateTime.Now;

            //subscribe to learning epoch results
            teacher.EpochResults += (sender, eventArgs) =>
            {
                if (DateTime.Now - dt > TimeSpan.FromSeconds(1))
                {
                    Console.WriteLine(
                        $"error: {eventArgs.Error}\r\ndelta: {eventArgs.Delta}\r\nepochCount: {eventArgs.EpochCount}\r\n");
                    dt = DateTime.Now;
                }
            };

            Console.WriteLine("Press ESC to stop learning and print results\n");

            //start learning in background
            var cts = new CancellationTokenSource();
            var task = Task.Run(() => teacher.Learn(cts.Token));
            while (true)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    cts.Cancel();
                    break;
                }
            }

            //Print results
            for (double x = 0; x <= 1; x += 0.05)
            {
                for (double y = 1; y >= 0; y -= 0.025)
                {
                    var result = network.Iterate(new[] {x,y});
                    Console.Write(result[0] > 0.5 ? "*" : "X");
                }

                Console.WriteLine();
            }
        }

        static INetwork CreateNetwork()
        {
            var learnParameters = new LearnParameters
            {
                LearnSpeed = 0.3, //set learn speed
                Moment = 0.7 //set moment
            };

            //create parameters object
            var networkParameters = new NetworkParameters
            {
                InputCount = 2,
                LearnParameters = learnParameters,
                HiddenLayers = new[]
                {
                    new HiddenLayerParameters
                    {
                        ActivationMethod = ActivationMethod.Relu, //Sigmoid or ReLU
                        HasBias = true,
                        Count = 4
                    }
                    // more hidden layers
                },

                OutputActivationMethod = ActivationMethod.Sigmoid, //Sigmoid or ReLU
                OutputCount = 1
            };

            return new Network(networkParameters);
        }

        static IEnumerable<TrainSet> CreateTrainSet()
        {
            Func<double, double, double> f = (x, y) => y > x ? 0 : 1;

            for (double x = 0; x <= 1; x += 0.1)
            for (double y = 0; y <= 1; y += 0.1)
            {
                double result = y <= f(x, y) ? 0 : 1;
                yield return new TrainSet(new[] {x, y}, new[] {result});
            }
        }
    }
}
