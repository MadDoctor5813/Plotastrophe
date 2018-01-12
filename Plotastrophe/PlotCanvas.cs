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

        public PlotCanvas(Canvas canvas)
        {
            mCanvas = canvas;
            functions = new List<PlotFunction>();
            for (int i = 0; i < 40; i++)
            {
                LinearFunction func = new LinearFunction(ToCanvasCoords);
                func.A = new Random().Next(2);
                AddFunction(func);
                func.RegenShape();
            }
        }

        private void AddFunction(PlotFunction function)
        {
            functions.Add(function);
            mCanvas.Children.Add(function.Polyline);
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
