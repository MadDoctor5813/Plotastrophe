using Plotastrophe.functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Plotastrophe
{
    class FunctionButton<T> : Button where T : PlotFunction, new()
    {

        private PlotCanvas canvas;

        public FunctionButton(PlotCanvas canvas)
        {
            this.canvas = canvas;
        }

        protected override void OnClick()
        {
            PlotFunction func = new T() { Canvas = canvas };
            func.RegenShape();
            canvas.AddFunction(func);
        }

    }
}
