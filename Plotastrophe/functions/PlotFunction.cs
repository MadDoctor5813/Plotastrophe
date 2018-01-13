using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Plotastrophe.functions
{
    public abstract class PlotFunction
    {

        public double A { get; set; } = 1;
        public double C { get; set; } = 0;
        public double K { get; set; } = 1;
        public double D { get; set; } = 0;

        public double Start { get; set; } = -50;
        public double End { get; set; } = 50;

        public Path PlotPath { get; }

        private const double DX = 0.01;

        public PlotCanvas Canvas { get; set; }

        public PlotFunction()
        {
            PlotPath = new Path();
            //basic appearance for now
            PlotPath.Stroke = System.Windows.Media.Brushes.Blue;
            PlotPath.StrokeThickness = 5;
            //setup events
            PlotPath.MouseLeftButtonDown += OnClick;
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                PlotPath.Stroke = System.Windows.Media.Brushes.Red;
            }
            else
            {
                PlotPath.Stroke = System.Windows.Media.Brushes.Blue;
            }
        }

        private void OnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Canvas.Select(this);
            e.Handled = true;
        }

        protected abstract double Parent(double x);

        protected double Evaluate(double x)
        {
            return (Parent((x + D) * K) * A) + C;
        }

        protected virtual List<double> Asymptotes()
        {
            return new List<double>();
        }

        public void RegenShape()
        {
            //find first valid start location
            double validStart = Start;
            for (; validStart < End; validStart += DX)
            {
                if (IsValidDouble(Evaluate(validStart)))
                {
                    break;
                }
            }
            PathFigure figure = new PathFigure();
            figure.StartPoint = Canvas.ToCanvasCoords(new Point(validStart, Evaluate(validStart)));
            PathGeometry geo = new PathGeometry();
            for (double i = validStart; i < End; i += DX)
            {   
                double result = Evaluate(i);
                if (IsValidDouble(result) && IsInRange(result))
                {
                    if (IsAsymptote(i - DX, i))
                    {
                        //create a new path figure
                        geo.Figures.Add(figure);
                        figure = new PathFigure();
                        figure.StartPoint = Canvas.ToCanvasCoords(new Point(i + DX, Evaluate(i + DX)));
                    }
                    LineSegment segment = new LineSegment();
                    segment.Point = Canvas.ToCanvasCoords(new Point(i, result));
                    figure.Segments.Add(segment);
                }
            }
            geo.Figures.Add(figure);
            PlotPath.Data = geo;
        }

        private bool IsAsymptote(double x1, double x2)
        {
            foreach (double asymptote in Asymptotes())
            {
                if (x2 >= asymptote && x1 <= asymptote)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidDouble(double x)
        {
            return !(double.IsNaN(x) || double.IsInfinity(x));
        }

        private bool IsInRange(double x)
        {
            return x < 5000 && x > -5000;
        }
    }
}
