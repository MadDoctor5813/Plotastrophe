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
    abstract class PlotFunction
    {

        public double A { get; set; } = 1;
        public double C { get; set; } = 0;
        public double K { get; set; } = 1;
        public double D { get; set; } = 0;

        public double Start { get; set; } = -100;
        public double End { get; set; } = 100;

        public Path PlotPath { get; }

        private const double DX = 0.5;

        PlotCanvas canvas;

        public PlotFunction(PlotCanvas canvas)
        {
            PlotPath = new Path();
            this.canvas = canvas;
            //basic appearance for now
            PlotPath.Stroke = System.Windows.Media.Brushes.Blue;
            PlotPath.StrokeThickness = 5;
            //setup events
            PlotPath.MouseLeftButtonDown += OnClick;
            RegenShape();
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
            canvas.Select(this);
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
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(Start, Evaluate(Start));
            PathGeometry geo = new PathGeometry();
            for (double i = Start; i < End; i += DX)
            {   
                double result = Evaluate(i);
                if (result != double.NaN && result != double.PositiveInfinity && result != double.NegativeInfinity)
                {
                    if (IsAsymptote(i - DX, i))
                    {
                        //create a new path figure
                        geo.Figures.Add(figure);
                        figure = new PathFigure();
                        figure.StartPoint = canvas.ToCanvasCoords(new Point(i, result));
                    }
                    LineSegment segment = new LineSegment();
                    segment.Point = canvas.ToCanvasCoords(new Point(i, result));
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
    }
}
