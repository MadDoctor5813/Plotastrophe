using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotastrophe.functions
{
    class LinearFunction : PlotFunction
    {
        protected override double Evaluate(double x)
        {
            return (((x + D) * K) * A) + C;
        }
    }
}
