using System;
using System.Linq;
using Newtonsoft.Json;
using SimplePerceptron.Network;

namespace SimplePerceptron.Trains
{
    public class NetworkTeacher
    {
        private readonly INetwork _network;
        private readonly TrainSet[] _trainSet;
        private readonly TeacherParameters _parameters;

        private double _epochError;
        private double _lastEpochError;
        private ulong _epochCount;

        private double[] _bestParams;
        private double _minEpochError = Double.MaxValue;
        private bool _activeLearning;

        private bool _reinitWeights;
        private bool _stopLearn;
        private int _setCounter;

        public bool IsLearning => _activeLearning;

        public int CurrentDataSet
        {
            get
            {
                if (!IsLearning) return 0;
                return _setCounter;
            }
        }

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
            _activeLearning = true;
            _epochError = 1;
            _epochCount = 0;

            var len = _trainSet.Length;
            var outputCount = _trainSet[0].OutputCount;

            double[][] results = new double[len][];
            double[][] ideals = new double[len][];

            double lastEpochError = 1;

            while (!_stopLearn && _epochError > _parameters.MinErrorEpsilon)
            {
                _epochCount++;

                double error;

                for (int i = 0; i < len; i++)
                {
                    if (_stopLearn) break;
                    _setCounter = i;

                    var set = _trainSet[i];

                    _network.SetInput(set.Input);

                    ideals[i] = set.Output;
                    results[i] = _network.GetOutput();
                    _network.Learn(ideals[i]);
                }

                // если цикл по сету был прерван (_stopLearn)
                error = _network.CalcSetError(results.Take(_setCounter + 1).ToArray(),
                    ideals.Take(_setCounter + 1).ToArray());

                _lastEpochError = _epochError;
                _epochError = error / outputCount;

                OnEpochResults();

                if (_parameters.MemorizeBestWeights && _epochError < _minEpochError)
                {
                    _minEpochError = _epochError;
                    _bestParams = _network.Weights;
                }

                if (_reinitWeights)
                    DoReinitWeights();
            }
            _activeLearning = false;

        }

        public string SerializeBestWeights()
        {
            return JsonConvert.SerializeObject(_bestParams);
        }

        public event EventHandler<EpochErrorEventArgs> EpochResults;

        protected virtual void OnEpochResults()
        {
            EpochResults?.Invoke(this,
                new EpochErrorEventArgs(_epochError, _lastEpochError - _epochError, _epochCount));
        }
    }
}
