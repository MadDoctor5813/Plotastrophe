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

        public void RegenShape()
        {
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(Start, Evaluate(Start));
            for (double i = Start; i < End; i += DX)
            {
                double result = Evaluate(i);
                if (result != double.NaN && result != double.PositiveInfinity && result != double.NegativeInfinity)
                {
                    LineSegment segment = new LineSegment();
                    segment.Point = canvas.ToCanvasCoords(new Point(i, result));
                    figure.Segments.Add(segment);
                }
            }
            PathGeometry geo = new PathGeometry();
            geo.Figures.Add(figure);
            PlotPath.Data = geo;
        }

    }
}
