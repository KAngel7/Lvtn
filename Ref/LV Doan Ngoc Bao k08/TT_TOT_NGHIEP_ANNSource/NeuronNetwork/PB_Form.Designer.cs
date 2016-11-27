﻿namespace NeuronNetwork
{
    partial class PB_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.learningRate = new System.Windows.Forms.TextBox();
            this.momentumTerm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TheNumberEpoches = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.residualError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // learningRate
            // 
            this.learningRate.Location = new System.Drawing.Point(190, 54);
            this.learningRate.Name = "learningRate";
            this.learningRate.Size = new System.Drawing.Size(151, 20);
            this.learningRate.TabIndex = 0;
            this.learningRate.TextChanged += new System.EventHandler(this.learningRate_TextChanged);
            // 
            // momentumTerm
            // 
            this.momentumTerm.Location = new System.Drawing.Point(190, 102);
            this.momentumTerm.Name = "momentumTerm";
            this.momentumTerm.Size = new System.Drawing.Size(150, 20);
            this.momentumTerm.TabIndex = 1;
            this.momentumTerm.TextChanged += new System.EventHandler(this.momentumTerm_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Learning Rate";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Momentum Term";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(112, 283);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 26);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(274, 284);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(67, 25);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Maximum Number of epoches";
            // 
            // TheNumberEpoches
            // 
            this.TheNumberEpoches.Location = new System.Drawing.Point(190, 154);
            this.TheNumberEpoches.Name = "TheNumberEpoches";
            this.TheNumberEpoches.Size = new System.Drawing.Size(151, 20);
            this.TheNumberEpoches.TabIndex = 9;
            this.TheNumberEpoches.Text = "10000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Residual of Errors";
            // 
            // residualError
            // 
            this.residualError.Location = new System.Drawing.Point(190, 205);
            this.residualError.Name = "residualError";
            this.residualError.Size = new System.Drawing.Size(150, 20);
            this.residualError.TabIndex = 11;
            this.residualError.Text = "1.0E-5";
            // 
            // PB_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(496, 379);
            this.Controls.Add(this.residualError);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TheNumberEpoches);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.momentumTerm);
            this.Controls.Add(this.learningRate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PB_Form";
            this.Text = "PB_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox learningRate;
        private System.Windows.Forms.TextBox momentumTerm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TheNumberEpoches;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox residualError;

    }
}