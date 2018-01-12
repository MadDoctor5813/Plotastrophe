using Plotastrophe.functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Plotastrophe
{
    class PlotCanvas
    {

        private Canvas mCanvas;

        private const double PLOT_SIZE = 100;

        private List<PlotFunction> functions;

        private PlotFunction selected = null;

        public PlotCanvas(Canvas canvas)
        {
            mCanvas = canvas;
            canvas.MouseLeftButtonDown += CanvasOnClick;
            functions = new List<PlotFunction>();
            LinearFunction l1 = new LinearFunction(this);
            LinearFunction l2 = new LinearFunction(this);
            l2.A = 3;
            l2.RegenShape();
            AddFunction(l1);
            AddFunction(l2);
        }

        private void AddFunction(PlotFunction function)
        {
            functions.Add(function);
            mCanvas.Children.Add(function.Polyline);
        }

        public void Select(PlotFunction func)
        {
            func.SetSelected(true);
            selected?.SetSelected(false);
            selected = func;
        }

        private void CanvasOnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Deselect();
        }

        public void Deselect()
        {
            selected?.SetSelected(false);
            selected = null;
        }

        public Point ToCanvasCoords(Point plotCoords)
        {
            double canvasX;
            double canvasY;
            //rescale coords
            canvasX = (plotCoords.X / PLOT_SIZE) * mCanvas.Width;
            canvasY = (plotCoords.Y / PLOT_SIZE) * mCanvas.Height;
            //invert the y coordinates
            canvasY = -canvasY;
            //move origin to center
            canvasX += mCanvas.Width / 2;
            canvasY += mCanvas.Height / 2;
            return new Point(canvasX, canvasY);
        }

    }
}
