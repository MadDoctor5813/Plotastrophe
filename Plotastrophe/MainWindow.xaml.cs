using Plotastrophe.functions;
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
            CreateFunctionButtons();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.None;
                selectModeText.Text = "Selection Mode: None";
            }
            else if (e.Key == Key.T)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Translate;
                selectModeText.Text = "Selection Mode: Translate";
            }
            else if (e.Key == Key.S)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Scale;
                selectModeText.Text = "Selection Mode: Scale";
            }
            else if (e.Key == Key.Z)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Start;
                selectModeText.Text = "Selection Mode: Start";
            }
            else if (e.Key == Key.X)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.End;
                selectModeText.Text = "Selection Mode: End";
            }
            else
            {
                plotCanvas.HandleKey(e.Key);
            }
        }

        private void CreateFunctionButtons()
        {
            AddFunctionButton<LinearFunction>();
            AddFunctionButton<QuarticFunction>();
            AddFunctionButton<QuinticFunction>();
            AddFunctionButton<Reciprocal>();
            AddFunctionButton<ExponentialFunction>();
            AddFunctionButton<LogarithmicFunction>();
            AddFunctionButton<SinFunction>();
            AddFunctionButton<CosFunction>();
            AddFunctionButton<TanFunction>();
            AddFunctionButton<SecFunction>();
            AddFunctionButton<CscFunction>();
            AddFunctionButton<CotFunction>();
            AddFunctionButton<QuadraticFunction>();
            AddFunctionButton<SquareRootFunction>();
            AddFunctionButton<CubicFunction>();
            AddFunctionButton<RationalFunction>();
        }

        private void AddFunctionButton<T>() where T : PlotFunction, new()
        {
            ListViewItem item = new ListViewItem();
            Button button = new Button();
            button.Content = new TextBlock() { Text = typeof(T).ToString() };
            button.Click += OnFunctionClick<T>;
            item.Content = button;
            buttonList.Items.Add(item);
        }

        private void OnFunctionClick<T>(object sender, RoutedEventArgs e) where T : PlotFunction, new()
        {
            PlotFunction func = new T() { Canvas = plotCanvas };
            func.RegenShape();
            plotCanvas.AddFunction(func);
            MessageBox.Show(canvas.Focus().ToString());
        }
    }
}
