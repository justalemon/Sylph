namespace Undine
{
    partial class Landing
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
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.FileGroupBox = new System.Windows.Forms.GroupBox();
            this.FileButton = new System.Windows.Forms.Button();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.BasicGroupBox = new System.Windows.Forms.GroupBox();
            this.DeveloperLabel = new System.Windows.Forms.Label();
            this.RegionLabel = new System.Windows.Forms.Label();
            this.ConsoleLabel = new System.Windows.Forms.Label();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ImageGroupBox = new System.Windows.Forms.GroupBox();
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.FileGroupBox.SuspendLayout();
            this.BasicGroupBox.SuspendLayout();
            this.ImageGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.Filter = "All suported file types|*.iso;*.nds;*.dsi|PlayStation Portable|*.iso|Nintendo DS/" +
    "DSi|*.nds;*.dsi";
            // 
            // FileGroupBox
            // 
            this.FileGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileGroupBox.Controls.Add(this.FileButton);
            this.FileGroupBox.Controls.Add(this.FileTextBox);
            this.FileGroupBox.Location = new System.Drawing.Point(12, 12);
            this.FileGroupBox.Name = "FileGroupBox";
            this.FileGroupBox.Size = new System.Drawing.Size(343, 51);
            this.FileGroupBox.TabIndex = 0;
            this.FileGroupBox.TabStop = false;
            this.FileGroupBox.Text = "File";
            // 
            // FileButton
            // 
            this.FileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FileButton.Location = new System.Drawing.Point(262, 18);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(75, 23);
            this.FileButton.TabIndex = 1;
            this.FileButton.Text = "Open";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // FileTextBox
            // 
            this.FileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileTextBox.Location = new System.Drawing.Point(6, 19);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.ReadOnly = true;
            this.FileTextBox.Size = new System.Drawing.Size(250, 20);
            this.FileTextBox.TabIndex = 0;
            // 
            // BasicGroupBox
            // 
            this.BasicGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BasicGroupBox.Controls.Add(this.DeveloperLabel);
            this.BasicGroupBox.Controls.Add(this.RegionLabel);
            this.BasicGroupBox.Controls.Add(this.ConsoleLabel);
            this.BasicGroupBox.Controls.Add(this.IdentifierLabel);
            this.BasicGroupBox.Controls.Add(this.NameLabel);
            this.BasicGroupBox.Location = new System.Drawing.Point(12, 69);
            this.BasicGroupBox.Name = "BasicGroupBox";
            this.BasicGroupBox.Size = new System.Drawing.Size(343, 90);
            this.BasicGroupBox.TabIndex = 1;
            this.BasicGroupBox.TabStop = false;
            this.BasicGroupBox.Text = "Basic Information";
            // 
            // DeveloperLabel
            // 
            this.DeveloperLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeveloperLabel.Location = new System.Drawing.Point(6, 29);
            this.DeveloperLabel.Name = "DeveloperLabel";
            this.DeveloperLabel.Size = new System.Drawing.Size(331, 13);
            this.DeveloperLabel.TabIndex = 6;
            this.DeveloperLabel.Text = "Developer";
            this.DeveloperLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RegionLabel
            // 
            this.RegionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RegionLabel.Location = new System.Drawing.Point(6, 68);
            this.RegionLabel.Name = "RegionLabel";
            this.RegionLabel.Size = new System.Drawing.Size(331, 13);
            this.RegionLabel.TabIndex = 2;
            this.RegionLabel.Text = "Region";
            this.RegionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConsoleLabel
            // 
            this.ConsoleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleLabel.Location = new System.Drawing.Point(6, 55);
            this.ConsoleLabel.Name = "ConsoleLabel";
            this.ConsoleLabel.Size = new System.Drawing.Size(331, 13);
            this.ConsoleLabel.TabIndex = 5;
            this.ConsoleLabel.Text = "Console";
            this.ConsoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IdentifierLabel
            // 
            this.IdentifierLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IdentifierLabel.Location = new System.Drawing.Point(6, 42);
            this.IdentifierLabel.Name = "IdentifierLabel";
            this.IdentifierLabel.Size = new System.Drawing.Size(331, 13);
            this.IdentifierLabel.TabIndex = 4;
            this.IdentifierLabel.Text = "Identifier";
            this.IdentifierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLabel.Location = new System.Drawing.Point(6, 16);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(331, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name/Title";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImageGroupBox
            // 
            this.ImageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageGroupBox.Controls.Add(this.ImagePictureBox);
            this.ImageGroupBox.Location = new System.Drawing.Point(12, 165);
            this.ImageGroupBox.Name = "ImageGroupBox";
            this.ImageGroupBox.Size = new System.Drawing.Size(343, 100);
            this.ImageGroupBox.TabIndex = 2;
            this.ImageGroupBox.TabStop = false;
            this.ImageGroupBox.Text = "Image";
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagePictureBox.Location = new System.Drawing.Point(3, 16);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(337, 81);
            this.ImagePictureBox.TabIndex = 0;
            this.ImagePictureBox.TabStop = false;
            // 
            // Landing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 387);
            this.Controls.Add(this.ImageGroupBox);
            this.Controls.Add(this.BasicGroupBox);
            this.Controls.Add(this.FileGroupBox);
            this.Name = "Landing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Undine";
            this.FileGroupBox.ResumeLayout(false);
            this.FileGroupBox.PerformLayout();
            this.BasicGroupBox.ResumeLayout(false);
            this.ImageGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.GroupBox FileGroupBox;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.GroupBox BasicGroupBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label ConsoleLabel;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.Label RegionLabel;
        private System.Windows.Forms.GroupBox ImageGroupBox;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.Label DeveloperLabel;
    }
}

