namespace winformTestbed
{
    partial class Form2
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.phoneBox = new System.Windows.Forms.TextBox();
            this.saveandclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(40, 13);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(100, 20);
            this.nameBox.TabIndex = 0;
            this.nameBox.Text = "name";
            // 
            // phoneBox
            // 
            this.phoneBox.Location = new System.Drawing.Point(40, 54);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(100, 20);
            this.phoneBox.TabIndex = 1;
            this.phoneBox.Text = "phone";
            // 
            // saveandclose
            // 
            this.saveandclose.Location = new System.Drawing.Point(40, 105);
            this.saveandclose.Name = "saveandclose";
            this.saveandclose.Size = new System.Drawing.Size(92, 62);
            this.saveandclose.TabIndex = 2;
            this.saveandclose.Text = "Save and Close";
            this.saveandclose.UseVisualStyleBackColor = true;
            this.saveandclose.Click += new System.EventHandler(this.saveandclose_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(170, 202);
            this.Controls.Add(this.saveandclose);
            this.Controls.Add(this.phoneBox);
            this.Controls.Add(this.nameBox);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox phoneBox;
        private System.Windows.Forms.Button saveandclose;
    }
}