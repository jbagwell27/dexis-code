namespace winformTestbed
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
            this.secondWindow = new System.Windows.Forms.Label();
            this.launch2nd = new System.Windows.Forms.Button();
            this.saveInfoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // secondWindow
            // 
            this.secondWindow.AutoSize = true;
            this.secondWindow.Location = new System.Drawing.Point(138, 27);
            this.secondWindow.Name = "secondWindow";
            this.secondWindow.Size = new System.Drawing.Size(93, 13);
            this.secondWindow.TabIndex = 0;
            this.secondWindow.Text = "2nd Window Data";
            // 
            // launch2nd
            // 
            this.launch2nd.Location = new System.Drawing.Point(12, 12);
            this.launch2nd.Name = "launch2nd";
            this.launch2nd.Size = new System.Drawing.Size(99, 42);
            this.launch2nd.TabIndex = 1;
            this.launch2nd.Text = "Launch 2nd Window";
            this.launch2nd.UseVisualStyleBackColor = true;
            this.launch2nd.Click += new System.EventHandler(this.launch2nd_Click);
            // 
            // saveInfoButton
            // 
            this.saveInfoButton.Location = new System.Drawing.Point(175, 152);
            this.saveInfoButton.Name = "saveInfoButton";
            this.saveInfoButton.Size = new System.Drawing.Size(75, 23);
            this.saveInfoButton.TabIndex = 2;
            this.saveInfoButton.Text = "Save Info";
            this.saveInfoButton.UseVisualStyleBackColor = true;
            this.saveInfoButton.Click += new System.EventHandler(this.saveInfoButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 222);
            this.Controls.Add(this.saveInfoButton);
            this.Controls.Add(this.launch2nd);
            this.Controls.Add(this.secondWindow);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label secondWindow;
        private System.Windows.Forms.Button launch2nd;
        private System.Windows.Forms.Button saveInfoButton;
    }
}

