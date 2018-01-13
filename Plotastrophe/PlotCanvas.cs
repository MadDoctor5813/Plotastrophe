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

        private MainWindow window;
        private Canvas mCanvas;

        private const double PLOT_SIZE = 100;

        private const double TRANSLATE_STEP = 0.1;
        private const double SCALE_STEP = 0.01;
        private const double RESTRICT_STEP = 0.1;

        private List<PlotFunction> functions;

        public SelectionState SelectMode { get; set; } = SelectionState.None;
        private PlotFunction selected = null;

        public PlotCanvas(MainWindow window, Canvas canvas)
        {
            this.window = window;
            mCanvas = canvas;
            canvas.MouseLeftButtonDown += CanvasOnClick;
            functions = new List<PlotFunction>();
        }

        public bool HandleKey(Key key)
        {
            window.UpdateFuncInfo(selected);
            if (selected != null)
            {
                switch (SelectMode)
                {
                    case SelectionState.Translate:
                        return HandleKeyTranslate(key);
                    case SelectionState.Scale:
                        return HandleKeyScale(key);
                    case SelectionState.Start:
                        return HandleKeyStart(key);
                    case SelectionState.End:
                        return HandleKeyEnd(key);
                }
            }
            return false;
        }


        private bool HandleKeyTranslate(Key key)
        {
            bool handled = false;
            if (key == Key.Left)
            {
                selected.D += TRANSLATE_STEP;
                handled = true;
            }
            else if (key == Key.Right)
            {
                selected.D -= TRANSLATE_STEP;
                handled = true;
            }
            else if (key == Key.Up)
            {
                selected.C += TRANSLATE_STEP;
                handled = true;
            }
            else if (key == Key.Down)
            {
                selected.C -= TRANSLATE_STEP;
                handled = true;
            }
            if (handled)
            {
                selected.RegenShape();
            }
            return handled;
        }


        private bool HandleKeyScale(Key key)
        {
            bool handled = false;
            if (key == Key.Left)
            {
                selected.K -= SCALE_STEP;
                handled = true;
            }
            if (key == Key.Right)
            {
                selected.K += SCALE_STEP;
                handled = true;
            }
            if (key == Key.Up)
            {
                selected.A += SCALE_STEP;
                handled = true;
            }
            if (key == Key.Down)
            {
                selected.A -= SCALE_STEP;
                handled = true;
            }
            if (handled)
            {
                selected.RegenShape();
            }
            return handled;
        }

        private bool HandleKeyStart(Key key)
        {
            bool handled = false;
            if (key == Key.Left)
            {
                selected.Start -= RESTRICT_STEP;
                handled = true;
            }
            if (key == Key.Right)
            {
                selected.Start += RESTRICT_STEP;
                handled = true;
            }
            if (handled)
            {
                selected.RegenShape();
            }
            return handled;
        }

        private bool HandleKeyEnd(Key key)
        {
            bool handled = true;
            if (key == Key.Left)
            {
                selected.End -= RESTRICT_STEP;
                handled = true;
            }
            if (key == Key.Right)
            {
                selected.End += RESTRICT_STEP;
                handled = true;
            }
            if (handled)
            {
                selected.RegenShape();
            }
            return handled;
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
            window.UpdateFuncInfo(selected);
        }

        private void CanvasOnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Deselect();
        }

        public void Deselect()
        {
            selected?.SetSelected(false);
            selected = null;
            window.UpdateFuncInfo(selected);
        }

        public void DeleteSelected()
        {
            mCanvas.Children.Remove(selected.PlotPath);
            functions.Remove(selected);
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
