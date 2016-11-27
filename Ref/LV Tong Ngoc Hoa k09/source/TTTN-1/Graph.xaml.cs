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
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.Globalization;
using System.Threading;

namespace TTTN_1
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
          List<double>  s=null;
       
       
        ObservableDataSource<Point> source2 = null;

        public Graph(List<double> s)
        {
            InitializeComponent();

            this.s = s;
         
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
            // Create first source
            source2 = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            source2.SetXYMapping(p => p);
            for (int x = 0; x < s.Count; x++)
            {
                Point p1 = new Point(x,s.ElementAt(x));
                source2.AppendAsync(Dispatcher, p1);
            }
            // Add all three graphs. Colors are not specified and chosen random

            
            plotter.AddLineGraph(source2, new Pen(Brushes.Blue, 3), new PenDescription("Observation"));
   

        }
    }
}
