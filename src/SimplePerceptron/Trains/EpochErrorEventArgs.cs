using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePerceptron.Trains
{
    public class EpochErrorEventArgs : EventArgs
    {
        public double Error { get; set; }
        public double Delta { get; set; }
        public ulong EpochCount { get; set; }

        public EpochErrorEventArgs(double error, double delta, ulong epochCount)
        {
            Error = error;
            Delta = delta;
            EpochCount = epochCount;
        }
    }
}
