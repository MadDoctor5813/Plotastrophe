using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Plotastrophe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PlotCanvas plotCanvas;

        public MainWindow()
        {
            InitializeComponent();
            plotCanvas = new PlotCanvas(canvas);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.None;
                selectModeText.Text = "Selection Mode: None";
            }
            if (e.Key == Key.T)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Translate;
                selectModeText.Text = "Selection Mode: Translate";
            }
            if (e.Key == Key.S)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Scale;
                selectModeText.Text = "Selection Mode: Scale";
            }
            if (e.Key == Key.R)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Restrictions;
                selectModeText.Text = "Selection Mode: Restrictions";
            }
        }

    }
}
