using Microsoft.Win32;
using Plotastrophe.functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        private string _selectText = "Selection Mode: None";
        private string SelectText
        {
            get
            {
                return _selectText;
            }
            set
            {
                _selectText = value;
                UpdateStatus();
            }
        }

        private string _infoText;
        private string InfoText
        {
            get
            {
                return _infoText;
            }
            set
            {
                _infoText = value;
                UpdateStatus();
            }
        }

        private void UpdateStatus()
        {
            selectModeText.Text = _selectText + _infoText;
        }

        public MainWindow()
        {
            InitializeComponent();
            plotCanvas = new PlotCanvas(this, canvas);
            CreateFunctionButtons();
            UpdateStatus();
        }

        public void UpdateFuncInfo(PlotFunction func)
        {
            if (func == null)
            {
                InfoText = "";
            }
            else
            {
                InfoText = $" | {func.GetType().ToString()}: A: {func.A:0.####} C: {func.C:0.####} K: {func.K:0.####} D: {func.D:0.####} Start: {func.Start:0.####} End: {func.End:0.####}";
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = HandleKey(e.Key);
            if (!e.Handled)
            {
                e.Handled = plotCanvas.HandleKey(e.Key);
            }
        }

        private bool HandleKey(Key key)
        {
            if (key == Key.Escape)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.None;
                SelectText = "Selection Mode: None";
                return true;
            }
            else if (key == Key.T)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Translate;
                SelectText = "Selection Mode: Translate";
                return true;
            }
            else if (key == Key.S)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Scale;
                SelectText = "Selection Mode: Scale";
                return true;
            }
            else if (key == Key.Z)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.Start;
                SelectText = "Selection Mode: Start";
                return true;
            }
            else if (key == Key.X)
            {
                plotCanvas.SelectMode = PlotCanvas.SelectionState.End;
                SelectText = "Selection Mode: End";
                return true;
            }
            else if (key == Key.Delete)
            {
                plotCanvas.DeleteSelected();
                return true;
            }
            return false;
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
            canvas.Focus();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Plot file (*.plot)|*.plot";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                using (FileStream file = File.OpenWrite(dlg.FileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(file, plotCanvas.Functions);
                }
            }
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Plot file (*.plot)|*.plot";
            bool? result = dlg.ShowDialog();
            List<PlotFunction> funcs = null;
            if (result == true)
            {
                using (FileStream file = File.OpenRead(dlg.FileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    funcs = (formatter.Deserialize(file) as List<PlotFunction>);
                }
            }
            if (funcs != null)
            {
                plotCanvas.ClearAllFunctions();
                foreach (var func in funcs)
                {
                    func.Canvas = plotCanvas;
                    func.RegenShape();
                    plotCanvas.AddFunction(func);
                }
            }
        }
    }
}
