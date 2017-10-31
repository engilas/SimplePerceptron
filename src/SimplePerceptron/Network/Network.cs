using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SimplePerceptron.Network.Layers;
using SimplePerceptron.Serialization;
using SimplePerceptron.Network.Neurons;
using SimplePerceptron.Network.Sinapses;
using SimplePerceptron.Parameters;

namespace SimplePerceptron.Network
{
    public class Network : INetwork
    {
        private readonly NetworkParameters _parameters;
        private InputLayer _inputLayer;
        private HiddenLayer[] _hiddenLayers;
        private OutputLayer _outputLayer;
        private List<Sinapse> _sinapses;


        private IEnumerable<ILayer> Layers
        {
            get
            {
                yield return _inputLayer;
                foreach (var hiddenLayer in _hiddenLayers)
                {
                    yield return hiddenLayer;
                }
                yield return _outputLayer;
            }
        }

        public Network(NetworkParameters parameters, double[] sinapses = null)
        {
            //input
            if (parameters.InputCount <= 0)
                throw new Exception("Wrong neuron count on input layer");
            _parameters = parameters;

            InitLayers();
            InitSinapses(sinapses);
        }

        private void InitLayers()
        {
            var inputNeurons = new InputNeuron[_parameters.InputCount];
            for (int i = 0; i < _parameters.InputCount; i++)
            {
                inputNeurons[i] = new InputNeuron();
            }

            _inputLayer = new InputLayer(inputNeurons);

            //hidden
            if (_parameters.HiddenLayers == null || _parameters.HiddenLayers.Length == 0)
                throw new Exception("Wrong hidden layer _parameters");

            var hiddenNeurons = new HiddenNeuron[_parameters.HiddenLayersCount][];
            _hiddenLayers = new HiddenLayer[_parameters.HiddenLayersCount];
            for (int i = 0; i < _parameters.HiddenLayersCount; i++)
            {
                int layerNum = i + 1;
                var hidderParams = _parameters.HiddenLayers[i];
                hiddenNeurons[i] = new HiddenNeuron[hidderParams.Count + 1];
                for (int j = 0; j < hidderParams.Count; j++)
                {
                    hiddenNeurons[i][j] = new HiddenNeuron(hidderParams.Activation);
                }
                if (hidderParams.HasBias)
                {
                    hiddenNeurons[i][hidderParams.Count] = new BiasNeuron();
                }
                else //удаляем место под bias
                {
                    hiddenNeurons[i] = hiddenNeurons[i].SkipLast(1).ToArray();
                }

                _hiddenLayers[i] = new HiddenLayer(layerNum, hiddenNeurons[i]);
            }

            //output
            if (_parameters.OutputCount <= 0)
                throw new Exception("Wrong neuron count on output layer");

            var outputNeurons = new OutputNeuron[_parameters.OutputCount];
            for (int i = 0; i < _parameters.OutputCount; i++)
            {
                outputNeurons[i] = new OutputNeuron(_parameters.OutputActivation);
            }

            _outputLayer = new OutputLayer(outputNeurons);
        }

        private void InitSinapses(double[] weights = null)
        {
            var parameters = _parameters.LearnParameters;

            _sinapses = new List<Sinapse>();

            var layers = Layers.ToArray();
            int weightsCounter = 0;
            for (int i = 0; i < layers.Length - 1; i++)
            {
                var layer = layers[i];
                for (int j = 0; j < layer.Length; j++)
                {
                    var item = layer[j];
                    var nextLayer = layers[i + 1];
                    for (int k = 0; k < nextLayer.Length; k++)
                    {
                        var nextItem = nextLayer[k];

                        if (nextItem is BiasNeuron)
                            continue;

                        Sinapse sinapse;

                        if (weights == null)
                            sinapse = new Sinapse(item, nextItem, parameters);
                        else
                            sinapse = new Sinapse(item, nextItem, parameters, weights[weightsCounter++]);

                        _sinapses.Add(sinapse);
                    }
                }
            }
        }

        public double[] GetResult(double[] input)
        {
            _inputLayer.SetInput(input);
            var result = _outputLayer.GetOutput();
            ResetOutput();
            return result;
        }

        public void ReinitNetworkWeights()
        {
            foreach (var sinapse in _sinapses)
            {
                sinapse.ResetWeight();
            }
        }

        public double[] Weights => _sinapses.Select(x => x.Weight).ToArray();

        public string Serialize()
        {
            return JsonConvert.SerializeObject(
                new SerializedNetwork
                {
                    Parameters = _parameters,
                    Weights = _sinapses.Select(x => x.Weight).ToArray()
                });
        }

        private void ResetOutput()
        {
            for (var i = 0; i < _hiddenLayers.Length; i++)
            {
                _hiddenLayers[i].ResetLastOutput();
            }
        }

        public void SetInput(double[] input)
        {
            _inputLayer.SetInput(input);
        }

        public double[] GetOutput() => _outputLayer.GetOutput();

        public void Learn(double[] expectedResults)
        {
            _outputLayer.Learn(expectedResults);

            for (int i = _hiddenLayers.Length - 1; i >= 0; i--)
            {
                _hiddenLayers[i].Learn();
            }

            ResetOutput();
        }

        public double CalcError(double[][] result, double[][] ideal)
        {
            double error = 0;
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    error += Math.Pow(ideal[i][j] - result[i][j], 2) / 2;
                }
            }
            error /= result.Length;
            return error;
        }
    }
}

