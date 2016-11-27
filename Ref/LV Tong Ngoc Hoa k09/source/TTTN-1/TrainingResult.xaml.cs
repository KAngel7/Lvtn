using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Navigation;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;
using System.Globalization;
using System.Threading;
namespace TTTN_1
{
    /// <summary>Sample that show rendering of data changing in time</summary>
    public partial class TrainingResult : Window
    {
        /// <summary>Programmatically created header</summary>
        Header chartHeader = new Header();

        /// <summary>Text contents of header</summary>
        TextBlock headerContents = new TextBlock();

        //List of MAE
        List<Point>  listPoint=null;

        //
        ObservableDataSource<Point> source1 = null;

        public TrainingResult(List<Point> listPoint)
        {
            InitializeComponent();

            this.listPoint = listPoint;
            
//             headerContents.FontSize = 24;
//             headerContents.Text = "MAE Result";
//             headerContents.HorizontalAlignment = HorizontalAlignment.Center;
//             chartHeader.Content = headerContents;
//             plotter.Children.Add(chartHeader);
        }

        private void Simulation()
        {
            for (int x = 0; x < listPoint.Count; x++)
            {

                Point p1 = new Point(listPoint.ElementAt(x).X, listPoint.ElementAt(x).Y); 
                source1.AppendAsync(Dispatcher, p1);
                Thread.Sleep(50);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create first source
            source1 = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            source1.SetXYMapping(p => p);

          
            // Add all three graphs. Colors are not specified and chosen random
            plotter.AddLineGraph(source1, new Pen(Brushes.Blue, 1), new PenDescription("MAE"));
       
            // Start computation process in second thread
            Thread simThread = new Thread(new ThreadStart(Simulation));
            simThread.IsBackground = true;
            simThread.Start();

        }
    }
}
