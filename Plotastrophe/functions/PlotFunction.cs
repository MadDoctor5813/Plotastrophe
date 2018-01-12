using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Plotastrophe.functions
{
    abstract class PlotFunction
    {

        public double A { get; set; } = 1;
        public double C { get; set; } = 0;
        public double K { get; set; } = 1;
        public double D { get; set; } = 0;

        public double Start { get; set; } = -1000;
        public double End { get; set; } = 1000;

        public Polyline Polyline { get; }

        private const double DX = 0.1;

        public PlotFunction()
        {
            Polyline = new Polyline();
            //basic appearance for now
            Polyline.Stroke = System.Windows.Media.Brushes.Bisque;
            Polyline.StrokeThickness = 2;
            RegenShape();
        }

        protected abstract double Evaluate(double x);

        public void RegenShape()
        {
            Polyline.Points.Clear();
            for (double i = Start; i < End; i += DX)
            {
                Polyline.Points.Add(new System.Windows.Point(i, Evaluate(i)));
            }
        }

    }
}
