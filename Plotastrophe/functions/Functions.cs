using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plotastrophe.functions
{
    class LinearFunction : PlotFunction
    {
        public LinearFunction(PlotCanvas canvas) : base(canvas)
        {
        }

        protected override double Parent(double x)
        {
            return x;
        }
    }

    class QuarticFunction : PlotFunction
    {
        public QuarticFunction(PlotCanvas canvas) : base(canvas)
        {
        }

        protected override double Parent(double x)
        {
            return Math.Pow(x, 4);
        }
    }

    class QuinticFunction : PlotFunction
    {
        public QuinticFunction(PlotCanvas canvas) : base(canvas)
        {
        }

        protected override double Parent(double x)
        {
            return Math.Pow(x, 5);
        }
    }

    class RationalFunction : PlotFunction
    {
        public RationalFunction(PlotCanvas canvas) : base(canvas)
        {
        }

        protected override double Parent(double x)
        {
            return 1 / x;
        }

        protected override double[] Asymptotes()
        {
            return new double[1] { -D };
        }
    }

}
