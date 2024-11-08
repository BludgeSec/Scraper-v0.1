namespace Scraper_v0._1
{
    partial class Form1
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
            this.networkLst = new System.Windows.Forms.ListView();
            this.locIp = new System.Windows.Forms.TextBox();
            this.scanBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // networkLst
            // 
            this.networkLst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.networkLst.HideSelection = false;
            this.networkLst.Location = new System.Drawing.Point(33, 101);
            this.networkLst.Name = "networkLst";
            this.networkLst.Size = new System.Drawing.Size(754, 780);
            this.networkLst.TabIndex = 0;
            this.networkLst.UseCompatibleStateImageBehavior = false;
            // 
            // locIp
            // 
            this.locIp.Location = new System.Drawing.Point(33, 28);
            this.locIp.Name = "locIp";
            this.locIp.Size = new System.Drawing.Size(245, 20);
            this.locIp.TabIndex = 1;
         
            // 
            // scanBtn
            // 
            this.scanBtn.Location = new System.Drawing.Point(33, 54);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(133, 41);
            this.scanBtn.TabIndex = 2;
            this.scanBtn.Text = "Scan Network";
            this.scanBtn.UseVisualStyleBackColor = true;
            this.scanBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 906);
            this.Controls.Add(this.scanBtn);
            this.Controls.Add(this.locIp);
            this.Controls.Add(this.networkLst);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView networkLst;
        private System.Windows.Forms.TextBox locIp;
        private System.Windows.Forms.Button scanBtn;
    }
}

