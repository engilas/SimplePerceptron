# Simple Perceptron

Implementation of perceptron (artificial neural network).

Example project: /src/Example/

### Network creation:

```C#
var learnParameters = new LearnParameters
{
    LearnSpeed = learnSpeed, //set learn speed
    Moment = moment //set moment
};

//create parameters object
var networkParameters = new NetworkParameters
{
    InputCount = inputCount,
    LearnParameters = learnParameters,
    HiddenLayers = new[]
    {
        new HiddenLayerParameters
        {
            ActivationMethod = ActivationMethod.Sigmoid, //Sigmoid or ReLU
            HasBias = true,
            Count = 16
        }
        // more hidden layers
    },

    OutputActivationMethod = ActivationMethod.Sigmoid, //Sigmoid or ReLU
    OutputCount = outputCount
};

INetwork network = new Network(networkParameters);
```

### Network training

First, create a trainset

If dot above or below line (y = x)
```C#
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
```

Then, create teacher
```C#
var teacher = new NetworkTeacher(network, CreateTrainSet().ToArray(), new TeacherParameters{});
```

Start learning
```C#
var cts = new CancellationTokenSource();
var task = Task.Run(() => teacher.Learn(cts.Token));
```

When train is complete, iterate over the points 

```C#
for (double x = 0; x <= 1; x += 0.05)
{
    for (double y = 1; y >= 0; y -= 0.025)
    {
        var result = network.Iterate(new[] {x,y});
        Console.Write(result[0] > 0.5 ? "*" : "X");
    }

    Console.WriteLine();
}
```
