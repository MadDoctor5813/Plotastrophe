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

        protected override double Parent(double x)
        {
            return x;
        }
    }

    class QuarticFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Pow(x, 4);
        }
    }

    class QuinticFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Pow(x, 5);
        }
    }

    class Reciprocal : PlotFunction
    {

        protected override double Parent(double x)
        {
            return 1 / x;
        }

        protected override List<double> Asymptotes()
        {
            return new List<double> { -D };
        }
    }

    class ExponentialFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Pow(2, x);
        }
    }

    class LogarithmicFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Log10(x);
        }

        protected override List<double> Asymptotes()
        {
            return new List<double> { -D };
        }
    }

    class SinFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Sin(x);
        }
    }

    class CosFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Cos(x);
        }
    }

    class TanFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Tan(x);
        }

        protected override List<double> Asymptotes()
        {
            List<double> asymptotes = new List<double>();
            for (double i = Math.PI / 2; i < End; i += Math.PI)
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            for (double i = -Math.PI / 2; i > Start; i -= Math.PI)
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            return asymptotes;
        }
    }

    class SecFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return 1 / Math.Cos(x);
        }

        protected override List<double> Asymptotes()
        {
            List<double> asymptotes = new List<double>();
            for (double i = Math.PI / 2; i < End; i += Math.PI)
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            for (double i = -Math.PI / 2; i > Start; i -= Math.PI)
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            return asymptotes;
        }
    }

    class CscFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return 1 / Math.Sin(x);
        }

        protected override List<double> Asymptotes()
        {
            List<double> asymptotes = new List<double>();
            for (double i = 0; i < End; i += (Math.PI))
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            for (double i = 0; i > Start; i -= (Math.PI))
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            return asymptotes;
        }
    }

    class CotFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return 1 / Math.Tan(x);
        }

        protected override List<double> Asymptotes()
        {
            List<double> asymptotes = new List<double>();
            for (double i = 0; i < End; i += (Math.PI))
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            for (double i = 0; i > Start; i -= (Math.PI))
            {
                asymptotes.Add(((1 / K) * i) - D);
            }
            return asymptotes;
        }
    }

    class QuadraticFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Pow(x, 2);
        }
    }

    class SquareRootFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Sqrt(x);
        }
    }

    class CubicFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return Math.Pow(x, 3);
        }
    }

    class RationalFunction : PlotFunction
    {

        protected override double Parent(double x)
        {
            return 1 / Math.Pow(x, 2);
        }

        protected override List<double> Asymptotes()
        {
            return new List<double> { -D };
        }
    }

}
