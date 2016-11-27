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
    /// <summary>
    /// Interaction logic for TestingResult.xaml
    /// </summary>
    public partial class TestingResult : Window
    {
        
        List<Point>  s=null;
        List<Point> c = null;

        //
        ObservableDataSource<Point> source1 = null;
        ObservableDataSource<Point> source2 = null;

        public TestingResult(List<Point> s, List<Point> c)
        {
            InitializeComponent();

            this.s = s;
            this.c = c;
            
//             headerContents.FontSize = 24;
//             headerContents.Text = "MAE Result";
//             headerContents.HorizontalAlignment = HorizontalAlignment.Center;
//             chartHeader.Content = headerContents;
//             plotter.Children.Add(chartHeader);
        }

        private void Simulation()
        {
            for (int x = 0; x < c.Count; x++)
            {
                Point p1 = new Point(c.ElementAt(x).X, c.ElementAt(x).Y); 
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
            Pen dashed = new Pen(Brushes.Red, 3);
            dashed.DashStyle = DashStyles.Dot;
            plotter.AddLineGraph(source1, dashed, new PenDescription("Computation"));

            // Create first source
            source2 = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            source2.SetXYMapping(p => p);
            for (int x = 0; x < s.Count; x++)
            {
                Point p1 = new Point(s.ElementAt(x).X, s.ElementAt(x).Y);
                source2.AppendAsync(Dispatcher, p1);
            }
            // Add all three graphs. Colors are not specified and chosen random
            plotter.AddLineGraph(source2, new Pen(Brushes.Blue, 1), new PenDescription("Observation"));
       
            // Start computation process in second thread
            Thread simThread = new Thread(new ThreadStart(Simulation));
            simThread.IsBackground = true;
            simThread.Start();

        }
    }
}
