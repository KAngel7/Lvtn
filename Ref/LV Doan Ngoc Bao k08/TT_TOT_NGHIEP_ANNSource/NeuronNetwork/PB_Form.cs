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
    public partial class PB_Form : Form
    {
        public double learn = 0.0;
        public double momen = 0.0;
        public bool ok = false;
        public double residual = 0.0;
        public double theEpoches = 0.0;
        public PB_Form()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.learningRate.Text = "";
            this.momentumTerm.Text = "";
            this.TheNumberEpoches.Text = "";
            this.residualError.Text = "";
            this.ok = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.learningRate.Text == null || this.learningRate.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Please insert Learning rate", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.ok = false;

            }
            else if (this.momentumTerm.Text == null || this.momentumTerm.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Please insert Momentum term", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                this.ok = false;
            }
            else if (this.TheNumberEpoches.Text == null || this.TheNumberEpoches.Text.Equals("")) 
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
                    this.momen = Double.Parse(this.momentumTerm.Text);
                    this.learn = Double.Parse(this.learningRate.Text);
                    this.theEpoches = Double.Parse(this.TheNumberEpoches.Text);
                    this.residual = Double.Parse(this.residualError.Text);
                    this.ok = true;
                    this.Close();
                }
                catch (System.Exception excp)
                {
                    System.Windows.MessageBox.Show("Input is wrong format", null, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        private void momentumTerm_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void learningRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void expectedError_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
