
namespace AutoVerDown
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.proBarDownLoad = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.lblPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // proBarDownLoad
            // 
            this.proBarDownLoad.ForeColor = System.Drawing.Color.DarkSalmon;
            this.proBarDownLoad.Location = new System.Drawing.Point(76, 117);
            this.proBarDownLoad.Name = "proBarDownLoad";
            this.proBarDownLoad.Size = new System.Drawing.Size(451, 29);
            this.proBarDownLoad.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(498, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Wide Latin", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPercent.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblPercent.Location = new System.Drawing.Point(76, 174);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(0, 28);
            this.lblPercent.TabIndex = 2;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 337);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.proBarDownLoad);
            this.Name = "Form1";
            this.Text = "版本更新 Ver DownLoad";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar proBarDownLoad;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblPercent;
    }
}

