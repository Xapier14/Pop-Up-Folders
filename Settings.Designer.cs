namespace Pop_Up_Folders
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ItemCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_DName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_CallName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Button_Save = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBox_MH = new System.Windows.Forms.TextBox();
            this.TextBox_MW = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.UI_Timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.ItemCountLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TextBox_DName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextBox_CallName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FlowPanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 228);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folder Creator";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(376, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Make Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ItemCountLabel
            // 
            this.ItemCountLabel.AutoSize = true;
            this.ItemCountLabel.Location = new System.Drawing.Point(6, 202);
            this.ItemCountLabel.Name = "ItemCountLabel";
            this.ItemCountLabel.Size = new System.Drawing.Size(91, 17);
            this.ItemCountLabel.TabIndex = 6;
            this.ItemCountLabel.Text = "Item Count: 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Folder Contents:";
            // 
            // TextBox_DName
            // 
            this.TextBox_DName.Location = new System.Drawing.Point(376, 15);
            this.TextBox_DName.Name = "TextBox_DName";
            this.TextBox_DName.Size = new System.Drawing.Size(176, 22);
            this.TextBox_DName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Display Name:";
            // 
            // TextBox_CallName
            // 
            this.TextBox_CallName.Location = new System.Drawing.Point(88, 15);
            this.TextBox_CallName.Name = "TextBox_CallName";
            this.TextBox_CallName.Size = new System.Drawing.Size(176, 22);
            this.TextBox_CallName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Call Name:";
            // 
            // FlowPanel
            // 
            this.FlowPanel.AllowDrop = true;
            this.FlowPanel.AutoScroll = true;
            this.FlowPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlowPanel.Location = new System.Drawing.Point(6, 60);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(546, 133);
            this.FlowPanel.TabIndex = 0;
            this.FlowPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.FlowPanel_Drop);
            this.FlowPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.FlowPanel_DEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Button_Save);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.TextBox_MH);
            this.groupBox2.Controls.Add(this.TextBox_MW);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(18, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(546, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(409, 66);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(131, 23);
            this.Button_Save.TabIndex = 6;
            this.Button_Save.Text = "Save Settings";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(518, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "px";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(518, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "px";
            // 
            // TextBox_MH
            // 
            this.TextBox_MH.Location = new System.Drawing.Point(151, 42);
            this.TextBox_MH.Name = "TextBox_MH";
            this.TextBox_MH.Size = new System.Drawing.Size(361, 22);
            this.TextBox_MH.TabIndex = 3;
            // 
            // TextBox_MW
            // 
            this.TextBox_MW.Location = new System.Drawing.Point(151, 15);
            this.TextBox_MW.Name = "TextBox_MW";
            this.TextBox_MW.Size = new System.Drawing.Size(361, 22);
            this.TextBox_MW.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Window Max Height: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Window Max Width: ";
            // 
            // SaveDialog
            // 
            this.SaveDialog.DefaultExt = "lnk";
            this.SaveDialog.Filter = "Shortcuts|*.lnk";
            this.SaveDialog.Title = "Save folder shortcut";
            this.SaveDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveDialog_FileOk);
            // 
            // UI_Timer
            // 
            this.UI_Timer.Enabled = true;
            this.UI_Timer.Interval = 25;
            this.UI_Timer.Tick += new System.EventHandler(this.UI_Timer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(197, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Written by: Lance Crisang - Copyright 2020";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Opacity = 0.85D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pop-Up Folder - Settings";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ItemCountLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBox_DName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_CallName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TextBox_MH;
        private System.Windows.Forms.TextBox TextBox_MW;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.Timer UI_Timer;
        private System.Windows.Forms.Label label4;
    }
}

