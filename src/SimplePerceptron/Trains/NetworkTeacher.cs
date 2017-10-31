using System;
using Newtonsoft.Json;
using SimplePerceptron.Network;

namespace SimplePerceptron.Trains
{
    class NetworkTeacher
    {
        private readonly INetwork _network;
        private readonly TrainSet[] _trainSet;
        private readonly TeacherParameters _parameters;


        private bool _reinitWeights;
        private bool _stopLearn;

        private double _epochError;
        private ulong _epochCount;

        private double[] _bestParams;
        private double _minEpochError = Double.MaxValue;

        public NetworkTeacher(INetwork network, TrainSet[] trainSet, TeacherParameters parameters)
        {
            _network = network;
            _trainSet = trainSet;
            _parameters = parameters;
        }

        public void ReinitWeights()
        {
            _reinitWeights = true;
        }

        public void StopLearn()
        {
            _stopLearn = true;
        }

        private void DoReinitWeights()
        {
            _network.ReinitNetworkWeights();
            _reinitWeights = false;
        }

        public void Learn()
        {
            _stopLearn = false;
            _epochError = 1;
            _epochCount = 0;

            var len = _trainSet.Length;
            var outputCount = _trainSet[0].OutputCount;

            double[][] results = new double[len][];
            double[][] ideals = new double[len][];
            

            while (!_stopLearn || _epochError > _parameters.MinErrorEpsilon)
            {
                _epochCount++;

                for (int i = 0; i < len; i++)
                {
                    var set = _trainSet[i];

                    _network.SetInput(set.Input);

                    ideals[i] = set.Output;
                    results[i] = _network.GetOutput();
                    _network.Learn(ideals[i]);
                }

                double error = _network.CalcError(results, ideals);

                _epochError = error / outputCount;


                if (_parameters.MemorizeBestWeights && _epochError < _minEpochError)
                {
                    _minEpochError = _epochError;
                    _bestParams = _network.Weights;
                }

                if (_reinitWeights)
                    DoReinitWeights();
            }
        }

        public string SerializeBestWeights()
        {
            return JsonConvert.SerializeObject(_bestParams);
        }
    }
}
