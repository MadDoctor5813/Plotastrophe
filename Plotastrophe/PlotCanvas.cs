using Plotastrophe.functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Plotastrophe
{


    public class PlotCanvas
    {

        public enum SelectionState
        {
            None,
            Translate,
            Scale,
            Start,
            End
        }

        private Canvas mCanvas;

        private const double PLOT_SIZE = 100;

        private const double TRANSLATE_STEP = 1;
        private const double SCALE_STEP = 0.1;
        private const double RESTRICT_STEP = 1;

        private List<PlotFunction> functions;

        public SelectionState SelectMode { get; set; } = SelectionState.None;
        private PlotFunction selected = null;

        public PlotCanvas(Canvas canvas)
        {
            mCanvas = canvas;
            canvas.MouseLeftButtonDown += CanvasOnClick;
            functions = new List<PlotFunction>();
            SquareRootFunction l1 = new SquareRootFunction() { Canvas = this };
            l1.RegenShape();
            AddFunction(l1);
        }

        public void HandleKey(Key key)
        {
            if (selected != null)
            {
                switch (SelectMode)
                {
                    case SelectionState.Translate:
                        HandleKeyTranslate(key);
                        break;
                    case SelectionState.Scale:
                        HandleKeyScale(key);
                        break;
                    case SelectionState.Start:
                        HandleKeyStart(key);
                        break;
                    case SelectionState.End:
                        HandleKeyEnd(key);
                        break;
                }
            }
        }


        private void HandleKeyTranslate(Key key)
        {
            if (key == Key.Left)
            {
                selected.D += TRANSLATE_STEP;
            }
            else if (key == Key.Right)
            {
                selected.D -= TRANSLATE_STEP;
            }
            else if (key == Key.Up)
            {
                selected.C += TRANSLATE_STEP;
            }
            else if (key == Key.Down)
            {
                selected.C -= TRANSLATE_STEP;
            }
            selected.RegenShape();
        }


        private void HandleKeyScale(Key key)
        {
            if (key == Key.Left)
            {
                selected.K -= SCALE_STEP;
            }
            if (key == Key.Right)
            {
                selected.K += SCALE_STEP;
            }
            if (key == Key.Up)
            {
                selected.A += SCALE_STEP;
            }
            if (key == Key.Down)
            {
                selected.A -= SCALE_STEP;
            }
            selected.RegenShape();
        }

        private void HandleKeyStart(Key key)
        {
            if (key == Key.Left)
            {
                selected.Start -= RESTRICT_STEP;
            }
            if (key == Key.Right)
            {
                selected.Start += RESTRICT_STEP;
            }
            selected.RegenShape();
        }

        private void HandleKeyEnd(Key key)
        {
            if (key == Key.Left)
            {
                selected.End -= RESTRICT_STEP;
            }
            if (key == Key.Right)
            {
                selected.End += RESTRICT_STEP;
            }
            selected.RegenShape();
        }

        public void AddFunction(PlotFunction function)
        {
            functions.Add(function);
            mCanvas.Children.Add(function.PlotPath);
            mCanvas.InvalidateVisual();
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
