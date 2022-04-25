
namespace _17321_Blok1
{
    partial class GammaInput
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
            this.Blue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Green = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Red = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Blue
            // 
            this.Blue.Location = new System.Drawing.Point(99, 133);
            this.Blue.Name = "Blue";
            this.Blue.Size = new System.Drawing.Size(120, 25);
            this.Blue.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(31, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Blue";
            // 
            // Green
            // 
            this.Green.Location = new System.Drawing.Point(99, 94);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(120, 25);
            this.Green.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Green";
            // 
            // Red
            // 
            this.Red.Location = new System.Drawing.Point(99, 54);
            this.Red.Name = "Red";
            this.Red.Size = new System.Drawing.Size(120, 25);
            this.Red.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Red";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Enter values between 0.2 and 5.0";
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.OK.Location = new System.Drawing.Point(22, 182);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(90, 29);
            this.OK.TabIndex = 10;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = false;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Cancel.Location = new System.Drawing.Point(127, 182);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(90, 29);
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // GammaInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(260, 248);
            this.Controls.Add(this.Blue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Green);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Red);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Cancel);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GammaInput";
            this.Text = "GammaInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Blue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Green;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Red;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
    }
}