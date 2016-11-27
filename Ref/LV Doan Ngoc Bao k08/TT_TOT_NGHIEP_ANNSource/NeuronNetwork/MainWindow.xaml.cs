﻿using System;
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

namespace NeuronNetwork
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
            this.tabTraining.IsEnabled = false;
            this.tabTesting.IsEnabled = false;
            this.tabForecasting.IsEnabled = false;
        }

        /*
         * Function to create a network when click on New Network button
         * Author: DataMining-Research08
         */
        private void new_network_Click(object sender, RoutedEventArgs e)
        {
            string nInputNodes = this.nInputNodes.Text;
            string nHiddenNodes = this.nHiddenNodes.Text;
            string nOutputNodes = this.nOutputNodes.Text;
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
                    this.tabTraining.IsEnabled = true;
                    this.tabTraining.Focus();
                    this.nInputNodes.IsEnabled = false;
                    this.nHiddenNodes.IsEnabled = false;
                    this.nOutputNodes.IsEnabled = false;
                }
                catch (Exception exception)
                {
                    System.Windows.MessageBox.Show(exception.Message);
                }
            }
        }

        /*
         * Function to choose data file
         * Author: DataMining-Research08
         */
        private void browse_file_Click(object sender, RoutedEventArgs e)
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
                int numColumns = 0;
                try
                {
                    fPointer = new System.IO.StreamReader(dataFile);
                    while ((line = fPointer.ReadLine()) != null)
                    {
                        if (numRows == 0)
                        {
                            char[] delimiterChars = { ' ',','};
                            string[] words = line.Split(delimiterChars);
                            numColumns = words.Length;
                        }
                        numRows++;
                    }
                    this.DatatextBox.Text = dataFile;
                    this.DatatextBox.IsEnabled = false;
                    this.numColumnTextBox.Text = Convert.ToString(numColumns);
                    this.numRowsTextBox.Text = Convert.ToString(numRows);
                    this.numColumnTextBox.IsEnabled = false;
                    this.numRowsTextBox.IsEnabled = false;
                    this.columnSelectTextBox.Text = "1";
                    this.beginLineTextBox.Text = "1";
                    this.endLineTextBox.Text = Convert.ToString(numRows);
                    this.columnSelectTextBox.IsEnabled = true;
                    if (numColumns == 1)
                    {
                        this.columnSelectTextBox.IsEnabled = false;
                    }
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

        /*
         * Funtion which is called when click on button Train
         * Author: DataMining-Research08
         * */
        private void train_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            int counter = 0;
            bool isFormatFileRight = true;
            int beginRow = Convert.ToInt32(this.beginLineTextBox.Text);
            int endRow = Convert.ToInt32(this.endLineTextBox.Text);
            int columnSelected = Convert.ToInt32(this.columnSelectTextBox.Text);
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.DatatextBox.Text);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    string[] words = line.Split(delimiterChars);
                    if (columnSelected <= words.Length)
                    {
                        sample.Add(Double.Parse(words[columnSelected - 1]));
                    }
                    else
                    {
                        isFormatFileRight = false;
                        break;
                    }
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
                if (this.radioBackPro.IsChecked == true)
                {
                    network.Bp_Train(sample);
                    this.tabTesting.IsEnabled = true;
                    this.tabForecasting.IsEnabled = true;
                    this.tabTesting.Focus();

                }
                else if (this.radioRPROP.IsChecked == true)
                {
                    network.Rprop_Train(sample);
                    this.tabTesting.IsEnabled = true;
                    this.tabForecasting.IsEnabled = true;
                    this.tabTesting.Focus();
                }
                else
                {
                    System.Windows.MessageBox.Show("Please choose algorithm to train", null, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void load_network_Click(object sender, RoutedEventArgs e)
        {
            bool debug = false;
            string networkFilePath = "";
            if (!debug)
            {
                OpenFileDialog file = new OpenFileDialog();
                DialogResult result = file.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    networkFilePath = file.FileName;
                }
                else return;
            }
            else
                networkFilePath = "D:\\HK112\\Do An\\Application\\trunk\\network.xml";
            Network loadedNetwork = Network.Import(networkFilePath);
            if (loadedNetwork == null)
            {
                System.Windows.MessageBox.Show("Network file is wrong format!!!", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.nInputNodes.Text = Convert.ToString(loadedNetwork.m_iNumInputNodes);
                this.nInputNodes.IsEnabled = false;
                this.nHiddenNodes.Text = Convert.ToString(loadedNetwork.m_iNumHiddenNodes);
                this.nHiddenNodes.IsEnabled = false;
                this.nOutputNodes.Text = Convert.ToString(loadedNetwork.m_iNumOutputNodes);
                this.nOutputNodes.IsEnabled = false;

                network = loadedNetwork;
                System.Windows.MessageBox.Show("Network loaded succesfully!. You can retrain/test/forcast with it");
                this.tabTraining.IsEnabled = true;
                this.tabTesting.IsEnabled = true;
                this.tabForecasting.IsEnabled = true;
            }
        }

        private void BrowseTestFile_Click(object sender, RoutedEventArgs e)
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
                System.Windows.MessageBox.Show("Please choose data file before testing", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                System.IO.StreamReader fPointer = null;
                string line = null;
                int numRows = 0;
                int numColumns = 0;
                try
                {
                    fPointer = new System.IO.StreamReader(dataFile);
                    while ((line = fPointer.ReadLine()) != null)
                    {
                        if (numRows == 0)
                        {
                            char[] delimiterChars = { ' ',','};
                            string[] words = line.Split(delimiterChars);
                            numColumns = words.Length;
                        }
                        numRows++;
                    }
                    this.TestTextBox.Text = dataFile;
                    this.TestTextBox.IsEnabled = false;
                    this.numColumnTBoxTest.Text = Convert.ToString(numColumns);
                    this.numRowsTBoxTest.Text = Convert.ToString(numRows);
                    this.numColumnTBoxTest.IsEnabled = false;
                    this.numRowsTBoxTest.IsEnabled = false;
                    this.columnSelectTest.Text = "1";
                    this.beginRowTest.Text = "1";
                    this.endRowTest.Text = Convert.ToString(numRows);
                    this.columnSelectTest.IsEnabled = true;
                    if (numColumns == 1)
                    {
                        this.columnSelectTest.IsEnabled = false;
                    }
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

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            bool isFormatFileRight = true;
            int beginRow = Convert.ToInt32(this.beginRowTest.Text);
            int endRow = Convert.ToInt32(this.endRowTest.Text);
            int columnSelected = Convert.ToInt32(this.columnSelectTest.Text);
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.TestTextBox.Text);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    string[] words = line.Split(delimiterChars);
                    if (columnSelected <= words.Length)
                    {
                        sample.Add(Double.Parse(words[columnSelected - 1]));
                    }
                    else
                    {
                        isFormatFileRight = false;
                        break;
                    }
                }
                if (!isFormatFileRight)
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    sample = null;
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

            if(sample != null)
                this.network.test(sample);
        }

        private void BrowseForcastData_Click(object sender, RoutedEventArgs e)
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
                System.Windows.MessageBox.Show("Please choose data file before testing", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                System.IO.StreamReader fPointer = null;
                string line = null;
                int numRows = 0;
                int numColumns = 0;
                try
                {
                    fPointer = new System.IO.StreamReader(dataFile);
                    while ((line = fPointer.ReadLine()) != null)
                    {
                        if (numRows == 0)
                        {
                            char[] delimiterChars = { ' ', ',' };
                            string[] words = line.Split(delimiterChars);
                            numColumns = words.Length;
                        }
                        numRows++;
                    }
                    this.ForcastTextBox.Text = dataFile;
                    this.ForcastTextBox.IsEnabled = false;
                    this.numColumnsForcast.Text = Convert.ToString(numColumns);
                    this.numRowsForcast.Text = Convert.ToString(numRows);
                    this.numColumnsForcast.IsEnabled = false;
                    this.numRowsForcast.IsEnabled = false;
                    this.columnSelectForcast.Text = "1";
                    this.beginRowForcast.Text = "1";
                    this.endRowForcast.Text = Convert.ToString(numRows);
                    this.columnSelectForcast.IsEnabled = true;
                    if (numColumns == 1)
                    {
                        this.columnSelectForcast.IsEnabled = false;
                    }
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

        private void ForecastButton_Click(object sender, RoutedEventArgs e)
        {
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            bool isFormatFileRight = true;
            int beginRow = Convert.ToInt32(this.beginRowForcast.Text);
            int endRow = Convert.ToInt32(this.endRowForcast.Text);
            int columnSelected = Convert.ToInt32(this.columnSelectForcast.Text);
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(this.ForcastTextBox.Text);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    string[] words = line.Split(delimiterChars);
                    if (columnSelected <= words.Length)
                    {
                        sample.Add(Double.Parse(words[columnSelected - 1]));
                    }
                    else
                    {
                        isFormatFileRight = false;
                        break;
                    }
                }
                if (!isFormatFileRight)
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    sample = null;
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

            if(sample != null)
            {
                try
                {
                    this.network.forecast(sample, int.Parse(this.nAheadTextBox.Text));
                }
                catch
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    sample.Clear();
                }
            }
        }

        private void clear_network_Click(object sender, RoutedEventArgs e)
        {
            network = null;
            this.nInputNodes.Text = "";
            this.nHiddenNodes.Text = "";
            this.nOutputNodes.Text = "";
            this.nInputNodes.IsEnabled = true;
            this.nHiddenNodes.IsEnabled = true;
            this.nOutputNodes.IsEnabled = true;
            this.tabTraining.IsEnabled = false;
            this.tabTesting.IsEnabled = false;
            this.tabForecasting.IsEnabled = false;
        }

        private void save_network_Click(object sender, RoutedEventArgs e)
        {
            bool debug = false;
            string networkFilePath = "";
            if (!debug)
            {
                SaveFileDialog file = new SaveFileDialog();
                file.Filter = "XML file|*.xml";
                file.Title = "Save Network";
                DialogResult result = file.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    networkFilePath = file.FileName;
                }
            }
            else
                networkFilePath = "D:\\HK112\\Do An\\Application\\trunk\\saveNetwork.xml";
            if (Network.Export(network, networkFilePath))
            {
                System.Windows.MessageBox.Show("You have already saved the network!");
            }
        }
    }
}
