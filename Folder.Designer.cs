namespace Pop_Up_Folders
{
    partial class Folder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Folder));
            this.FadeTimer = new System.Windows.Forms.Timer(this.components);
            this.L_Title = new System.Windows.Forms.Label();
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CurCheck = new System.Windows.Forms.Timer(this.components);
            this.CloseTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // FadeTimer
            // 
            this.FadeTimer.Enabled = true;
            this.FadeTimer.Interval = 5;
            this.FadeTimer.Tick += new System.EventHandler(this.FadeTimer_Tick);
            // 
            // L_Title
            // 
            this.L_Title.AutoSize = true;
            this.L_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.L_Title.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_Title.ForeColor = System.Drawing.Color.Silver;
            this.L_Title.Location = new System.Drawing.Point(0, 0);
            this.L_Title.Margin = new System.Windows.Forms.Padding(4);
            this.L_Title.Name = "L_Title";
            this.L_Title.Size = new System.Drawing.Size(123, 23);
            this.L_Title.TabIndex = 0;
            this.L_Title.Text = "Folder Name";
            // 
            // FlowPanel
            // 
            this.FlowPanel.AutoScroll = true;
            this.FlowPanel.Location = new System.Drawing.Point(0, 23);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(598, 233);
            this.FlowPanel.TabIndex = 1;
            this.FlowPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FlowPanel_Paint);
            // 
            // CurCheck
            // 
            this.CurCheck.Enabled = true;
            this.CurCheck.Tick += new System.EventHandler(this.CurCheck_Tick);
            // 
            // CloseTimer
            // 
            this.CloseTimer.Interval = 10000;
            this.CloseTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // Folder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.ClientSize = new System.Drawing.Size(598, 256);
            this.Controls.Add(this.FlowPanel);
            this.Controls.Add(this.L_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Folder";
            this.ShowInTaskbar = false;
            this.Text = "Folder";
            this.Deactivate += new System.EventHandler(this.Folder_MLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer FadeTimer;
        private System.Windows.Forms.Label L_Title;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private System.Windows.Forms.Timer CurCheck;
        private System.Windows.Forms.Timer CloseTimer;
    }
}