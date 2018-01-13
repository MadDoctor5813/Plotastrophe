using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotastrophe.functions
{
    public class FunctionFactory<T> where T : PlotFunction, new()
    {

        private PlotCanvas canvas;

        public FunctionFactory(PlotCanvas canvas)
        {
            this.canvas = canvas;
        }

        public void Create()
        {
            PlotFunction func = new T();
            func.RegenShape();
            canvas.AddFunction(func);
        }


    }
}
