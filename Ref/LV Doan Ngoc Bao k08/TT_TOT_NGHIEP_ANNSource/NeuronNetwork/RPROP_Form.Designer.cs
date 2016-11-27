namespace NeuronNetwork
{
    partial class RPROP_Form
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maxUpdateTextBox = new System.Windows.Forms.TextBox();
            this.minUpdateTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.theEpoches = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.residualError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(319, 302);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(67, 25);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(169, 302);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 26);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Max update value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Default update value";
            // 
            // maxUpdateTextBox
            // 
            this.maxUpdateTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.maxUpdateTextBox.Location = new System.Drawing.Point(236, 120);
            this.maxUpdateTextBox.Name = "maxUpdateTextBox";
            this.maxUpdateTextBox.Size = new System.Drawing.Size(150, 20);
            this.maxUpdateTextBox.TabIndex = 9;
            this.maxUpdateTextBox.Text = "50.0";
            // 
            // minUpdateTextBox
            // 
            this.minUpdateTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.minUpdateTextBox.Location = new System.Drawing.Point(235, 58);
            this.minUpdateTextBox.Name = "minUpdateTextBox";
            this.minUpdateTextBox.Size = new System.Drawing.Size(151, 20);
            this.minUpdateTextBox.TabIndex = 8;
            this.minUpdateTextBox.Text = "0.0001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Maximum Number of Epoches";
            // 
            // theEpoches
            // 
            this.theEpoches.BackColor = System.Drawing.SystemColors.Window;
            this.theEpoches.Location = new System.Drawing.Point(235, 182);
            this.theEpoches.Name = "theEpoches";
            this.theEpoches.Size = new System.Drawing.Size(151, 20);
            this.theEpoches.TabIndex = 17;
            this.theEpoches.Text = "10000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Residual of Errors";
            // 
            // residualError
            // 
            this.residualError.Location = new System.Drawing.Point(234, 235);
            this.residualError.Name = "residualError";
            this.residualError.Size = new System.Drawing.Size(152, 20);
            this.residualError.TabIndex = 19;
            this.residualError.Text = "1.0E-5";
            // 
            // RPROP_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(519, 386);
            this.Controls.Add(this.residualError);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.theEpoches);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxUpdateTextBox);
            this.Controls.Add(this.minUpdateTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RPROP_Form";
            this.Text = "RPROP_FORM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maxUpdateTextBox;
        private System.Windows.Forms.TextBox minUpdateTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox theEpoches;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox residualError;
    }
}