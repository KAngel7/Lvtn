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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra.Single;



namespace TTTN_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Network network;

        public MainWindow()
        {
            InitializeComponent();
            network = null;
            
//             var xdata = new float[] { 10, 20, 30, 40 };
//             var xdata1 = new float[] { 10, 20, 30, 40 };
//             var ydata = new float[] { 15, 20, 25, 30 };
// 
//            
//             // build matrices
//             var X = DenseMatrix.CreateFromColumns(
//               new[] { new DenseVector(xdata.Length, 1), new DenseVector(xdata), new DenseVector(xdata1) });
//             var y = new DenseVector(ydata);
//            
//             // solve
//             var p = X.QR().Solve(y);
//             var a = p[0];
//             var b = p[1];
//             System.Windows.Forms.MessageBox.Show("a " + a + "b " + b + "c " + p[2]);
        }

        private void btn_trn_train_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            int counter = 0;
            bool isFormatFileRight = true;
            int beginRow = Convert.ToInt32(this.trn_from.Text);
            int endRow = Convert.ToInt32(this.trn_to.Text);
            
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.datafilebox.Text);

                //read single column data file
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;
                        sample.Add(Double.Parse(line));
                    
                }
                if (!isFormatFileRight)
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    sample = null;
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                sample = null;
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.IO.IOException io)
            {
                sample = null;
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception excp)
            {
                sample = null;
                System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
            if (sample != null)
            {
                if (this.radio_bp.IsChecked == true)
                {
                    network.Bp_Khanh_run(sample, 0.1, 0.05, 10000, 0.00001);
                    network.Sa_Khanh_run(sample, 9000, 20, 15, 100);
                }
                else if (this.radio_sa.IsChecked == true)
                {
                    //network = new Network(12, 12, 1);
                    network.SA_Train(sample);
                   // System.Windows.MessageBox.Show("Algorithm not implement yet!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //                     network.Rprop_Train(sample);
                    //                     this.tabTesting.IsEnabled = true;
                    //                     this.tabForecasting.IsEnabled = true;
                    //                     this.tabTesting.Focus();
                }
                else if (this.radio_sa_2.IsChecked == true)
                {
                    //network = new Network(12, 12, 1);
                    network.SA_Train_2(sample);
                    // System.Windows.MessageBox.Show("Algorithm not implement yet!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //                     network.Rprop_Train(sample);
                    //                     this.tabTesting.IsEnabled = true;
                    //                     this.tabForecasting.IsEnabled = true;
                    //                     this.tabTesting.Focus();
                }
                else
                {
                    System.Windows.MessageBox.Show("Please choose algorithm to train", null, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_cfg_create_Click(object sender, RoutedEventArgs e)
        {
            string nInputNodes = this.n_inputNodes.Text;
            string nHiddenNodes = this.n_hidenNodes.Text;
            string nOutputNodes = this.n_outputNodes.Text;
            if (nInputNodes == "" || nHiddenNodes == "" || nOutputNodes == "")
            {
                System.Windows.MessageBox.Show("Please insert the number of Input Nodes, Hidden Nodes, Output Nodes", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    network = new Network(Int32.Parse(nInputNodes), Int32.Parse(nHiddenNodes), Int32.Parse(nOutputNodes));
                    System.Windows.MessageBox.Show("NetWork configuration successfull, You can train it");                    
                    this.n_inputNodes.IsEnabled = false;
                    this.n_hidenNodes.IsEnabled = false;
                    this.n_outputNodes.IsEnabled = false;
                }
                catch (Exception exception)
                {
                    System.Windows.MessageBox.Show(exception.Message);
                }
            }
           
        }

        private void btn_cfg_reset_Click(object sender, RoutedEventArgs e)
        {
            n_inputNodes.Clear();
            n_hidenNodes.Clear();
            n_outputNodes.Clear();
            this.n_inputNodes.IsEnabled = true;
            this.n_hidenNodes.IsEnabled = true;
            this.n_outputNodes.IsEnabled = true;
           
        }

        private void btn_trn_browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            DialogResult result = file.ShowDialog();
            string dataFile = "";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dataFile = file.FileName;
            }
            else
                return;
            if (dataFile == null || dataFile.Equals(""))
            {
                System.Windows.MessageBox.Show("Please choose data file before training", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                System.IO.StreamReader fPointer = null;
                string line = null;
                int numRows = 0;              
                try
                {
                    fPointer = new System.IO.StreamReader(dataFile);
                    while ((line = fPointer.ReadLine()) != null)
                    {
                        numRows++;
                    }
                    this.datafilebox.Text = dataFile;
                    this.datafilebox.IsEnabled = false;
                    this.totalrow.Clear();
                    this.totalrow.AppendText(numRows.ToString());                    
                }
                catch (System.OutOfMemoryException outOfMemory)
                {
                    System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (System.IO.IOException io)
                {
                    System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (System.Exception excp)
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    if (fPointer != null)
                        fPointer.Close();
                }
            }
        }

        private void btn_test_reset_Click(object sender, RoutedEventArgs e)
        {
            test_from.Clear();
            test_to.Clear();
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            int beginRow = 1;
            int endRow = 2;
            try
            {
                beginRow = Convert.ToInt32(this.test_from.Text) - network.m_iNumInputNodes;
                endRow = Convert.ToInt32(this.test_to.Text);
            }
            catch
            {

            }
           
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.datafilebox.Text);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;
                    sample.Add(Double.Parse(line));
                   
                }
                
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
                sample = null;
            }
            catch (System.IO.IOException io)
            {
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
                sample = null;
            }
            catch (System.Exception excp)
            {
                System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                sample = null;
            }
            finally
            {
                if (file != null)
                    file.Close();
            }

            if (sample != null)
            {
                if (this.radio_sa_2.IsChecked == true)
                    this.network.test2(sample);
                else
                    this.network.test(sample);
            }
               
        }

        private void btn_fc_forecast_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;          
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.datafilebox.Text);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;

                    sample.Add(Double.Parse(line));                   
                   
                }

               // System.Console.WriteLine("idxRow: " + idxRow);
              
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
                sample = null;
            }
            catch (System.IO.IOException io)
            {
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
                sample = null;
            }
            catch (System.Exception excp)
            {
                System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                sample = null;
            }
            finally
            {
                if (file != null)
                    file.Close();
            }

            if (sample != null)
            {
                this.network.Forecast(sample, Convert.ToInt32(this.n_forecast.Text));
//                 try
//                 {
//                     System.Console.WriteLine("Sample is not null");
//                     this.network.Forecast(sample, Convert.ToInt32(this.n_forecast.Text));
//                 }
//                 catch
//                 {
//                     
//                     System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
//                 }
//                 finally
//                 {
//                     sample.Clear();
//                 }
            }
        }

        private void btn_fc_reset_Click(object sender, RoutedEventArgs e)
        {
            n_forecast.Clear();
        }

        private void n_inputNodes_TextChanged(object sender, TextChangedEventArgs e)
        {
            n_hidenNodes.Text = n_inputNodes.Text;
            n_outputNodes.Text = "1";
        }

        private void btn_trn_graph_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
          
            bool isFormatFileRight = true;
        
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.datafilebox.Text);
                int beginRow = Convert.ToInt32(this.trn_from.Text);
                int endRow = Convert.ToInt32(this.trn_to.Text);
                //read single column data file
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;

                    if (idxRow < beginRow || idxRow > endRow)
                        continue;
                        sample.Add(Double.Parse(line));
                    
                }
                if (!isFormatFileRight)
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    sample = null;
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                sample = null;
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.IO.IOException io)
            {
                sample = null;
                System.Windows.MessageBox.Show("File does not found", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception excp)
            {
                sample = null;
                System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
            if (sample != null)
            {
                Graph grahp = new Graph(sample);
            grahp.Show();
            }
        }

    }
}
