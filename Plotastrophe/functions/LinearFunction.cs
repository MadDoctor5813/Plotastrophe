﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plotastrophe.functions
{
    class LinearFunction : PlotFunction
    {
        public LinearFunction(Func<Point, Point> transform) : base(transform)
        {
        }

        protected override double Evaluate(double x)
        {
            return (((x + D) * K) * A) + C;
        }
    }
}
