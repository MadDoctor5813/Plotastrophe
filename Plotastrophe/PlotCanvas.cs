using Plotastrophe.functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Plotastrophe
{
    class PlotCanvas
    {

        private Canvas mCanvas;

        private List<PlotFunction> functions;

        public PlotCanvas(Canvas canvas)
        {
            mCanvas = canvas;
            functions = new List<PlotFunction>();
            for (int i = 0; i < 40; i++)
            {
                LinearFunction func = new LinearFunction();
                func.A = new Random().Next(10);
                AddFunction(func);
                func.RegenShape();
            }
        }

        private void AddFunction(PlotFunction function)
        {
            functions.Add(function);
            mCanvas.Children.Add(function.Polyline);
        }

    }
}
