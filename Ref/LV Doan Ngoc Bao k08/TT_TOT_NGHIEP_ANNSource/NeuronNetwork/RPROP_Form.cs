﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeuronNetwork
{
    public partial class RPROP_Form : Form
    {
        public double minUpdate = 0.0;
        public double maxUpdate = 50.0;
        public double epoches = 10000;
        public double residual = 0.0;
        public bool ok = false;
        public RPROP_Form()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.minUpdateTextBox.Text == null || this.minUpdateTextBox.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Please insert default update value", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.ok = false;

            }
            else if (this.maxUpdateTextBox.Text == null || this.maxUpdateTextBox.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Please insert max update value", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.ok = false;
            }
            else if (this.theEpoches.Text == null || this.theEpoches.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Please insert The Maximum Number of Epoches", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.ok = false;
            }
            else if (this.residualError.Text == null || this.residualError.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Please insert Residual of Errors", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.ok = false;
            }
            else
            {
                try
                {
                    this.minUpdate = Double.Parse(this.minUpdateTextBox.Text);
                    this.maxUpdate = Double.Parse(this.maxUpdateTextBox.Text);
                    this.epoches = Double.Parse(this.theEpoches.Text);
                    this.residual = Double.Parse(this.residualError.Text);
                    this.ok = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Please check value in text box", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    this.ok = false;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.maxUpdateTextBox.Text = "";
            this.minUpdateTextBox.Text = "";
            this.theEpoches.Text = "";
            this.residualError.Text = "";
            this.ok = false;
        }
    }
}
