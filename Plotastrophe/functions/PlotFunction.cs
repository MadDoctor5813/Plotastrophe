using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public Polyline Polyline { get; }

        private const double DX = 0.5;

        PlotCanvas canvas;

        public PlotFunction(PlotCanvas canvas)
        {
            Polyline = new Polyline();
            this.canvas = canvas;
            //basic appearance for now
            Polyline.Stroke = System.Windows.Media.Brushes.Blue;
            Polyline.StrokeThickness = 5;
            //setup events
            Polyline.MouseLeftButtonDown += OnClick;
            RegenShape();
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                Polyline.Stroke = System.Windows.Media.Brushes.Red;
            }
            else
            {
                Polyline.Stroke = System.Windows.Media.Brushes.Blue;
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
            Polyline.Points.Clear();
            for (double i = Start; i < End; i += DX)
            {
                Polyline.Points.Add(canvas.ToCanvasCoords(new Point(i, Evaluate(i))));
            }
        }

    }
}
