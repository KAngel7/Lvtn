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

namespace TTTN_1
{
    /// <summary>
    /// Interaction logic for BP_Congfig.xaml
    /// </summary>
    
    public partial class BP_Congfig : Window
    {
        public double learn = 0.0;
        public double momen = 0.0;      
        public double residual = 0.0;
        public double theEpoches = 0.0;



        public BP_Congfig()
        {
            InitializeComponent();
            maxEpoches.AppendText("10000");
            residualError.AppendText("1.0E-5");
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            
            this.DialogResult = false;
            this.Close();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (this.learningRate.Text == null || this.learningRate.Text.Equals(""))            {
                System.Windows.MessageBox.Show("Please insert Learning rate", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
             

            }else

            if (this.momentumTerm.Text == null || this.momentumTerm.Text.Equals(""))            {
                System.Windows.MessageBox.Show("Please insert Momentum term", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
             
            }else

            if (this.maxEpoches.Text == null || this.maxEpoches.Text.Equals(""))            {
                System.Windows.MessageBox.Show("Please insert The Maximum Number of Epoches", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
               
            }else

            if (this.residualError.Text == null || this.residualError.Text.Equals(""))            {
                System.Windows.MessageBox.Show("Please insert Residual of Errors", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            
            }else

            try
            {
                this.momen = Double.Parse(this.momentumTerm.Text);
                this.learn = Double.Parse(this.learningRate.Text);
                this.theEpoches = Double.Parse(this.maxEpoches.Text);
                this.residual = Double.Parse(this.residualError.Text);

                this.DialogResult = true;
                this.Close();
            }
            catch (System.Exception excp)
            {
                System.Windows.MessageBox.Show("Input is wrong format", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }

        }

        private void Silder_lnr_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double t = slider_Lnr.Value / 10.0;
            this.learningRate.Text = t.ToString();
        }

        private void Silder_mmt_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double t = slider_mmt.Value / 10.0;
            this.momentumTerm.Text = t.ToString();
        }

    }
}
