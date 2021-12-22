
namespace lightstudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorPickerPanel = new System.Windows.Forms.Panel();
            this.colorPickerPictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.blueTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.greenTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.redTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timeline1 = new lightstudio.Timeline();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.deviceDisplay2 = new lightstudio.DeviceDisplay();
            this.colorPickerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickerPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorPickerPanel
            // 
            this.colorPickerPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.colorPickerPanel.Controls.Add(this.colorPickerPictureBox);
            this.colorPickerPanel.Controls.Add(this.label4);
            this.colorPickerPanel.Controls.Add(this.blueTextBox);
            this.colorPickerPanel.Controls.Add(this.label3);
            this.colorPickerPanel.Controls.Add(this.greenTextBox);
            this.colorPickerPanel.Controls.Add(this.label2);
            this.colorPickerPanel.Controls.Add(this.label1);
            this.colorPickerPanel.Controls.Add(this.redTextBox);
            this.colorPickerPanel.Location = new System.Drawing.Point(318, 37);
            this.colorPickerPanel.Name = "colorPickerPanel";
            this.colorPickerPanel.Size = new System.Drawing.Size(395, 64);
            this.colorPickerPanel.TabIndex = 1;
            // 
            // colorPickerPictureBox
            // 
            this.colorPickerPictureBox.BackColor = System.Drawing.Color.Black;
            this.colorPickerPictureBox.Location = new System.Drawing.Point(303, 25);
            this.colorPickerPictureBox.Name = "colorPickerPictureBox";
            this.colorPickerPictureBox.Size = new System.Drawing.Size(76, 27);
            this.colorPickerPictureBox.TabIndex = 7;
            this.colorPickerPictureBox.TabStop = false;
            this.colorPickerPictureBox.Click += new System.EventHandler(this.colorPickerPictureBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "B:";
            // 
            // blueTextBox
            // 
            this.blueTextBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.blueTextBox.Location = new System.Drawing.Point(224, 25);
            this.blueTextBox.Name = "blueTextBox";
            this.blueTextBox.Size = new System.Drawing.Size(61, 27);
            this.blueTextBox.TabIndex = 5;
            this.blueTextBox.Text = "0";
            this.blueTextBox.TextChanged += new System.EventHandler(this.blueTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "G:";
            // 
            // greenTextBox
            // 
            this.greenTextBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.greenTextBox.Location = new System.Drawing.Point(125, 25);
            this.greenTextBox.Name = "greenTextBox";
            this.greenTextBox.Size = new System.Drawing.Size(61, 27);
            this.greenTextBox.TabIndex = 3;
            this.greenTextBox.Text = "0";
            this.greenTextBox.TextChanged += new System.EventHandler(this.greenTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "R:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color";
            // 
            // redTextBox
            // 
            this.redTextBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.redTextBox.Location = new System.Drawing.Point(30, 25);
            this.redTextBox.Name = "redTextBox";
            this.redTextBox.Size = new System.Drawing.Size(61, 27);
            this.redTextBox.TabIndex = 0;
            this.redTextBox.Text = "0";
            this.redTextBox.TextChanged += new System.EventHandler(this.redTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(27, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 63);
            this.panel1.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(13, 29);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(109, 27);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Milliseconds",
            "Seconds"});
            this.comboBox1.Location = new System.Drawing.Point(128, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(116, 28);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "Milliseconds";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Duration";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add Frame";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // timeline1
            // 
            this.timeline1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.timeline1.BackgroundColor = System.Drawing.Color.Black;
            this.timeline1.Clock = null;
            this.timeline1.GridAlpha = 40;
            this.timeline1.Location = new System.Drawing.Point(12, 526);
            this.timeline1.Name = "timeline1";
            this.timeline1.Size = new System.Drawing.Size(753, 50);
            this.timeline1.TabIndex = 4;
            this.timeline1.Text = "timeline1";
            this.timeline1.TrackBorderSize = 2;
            this.timeline1.TrackHeight = 20;
            this.timeline1.TrackSpacing = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1212, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel1.Text = "Connection";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(920, 666);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "Write Frame";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // deviceDisplay2
            // 
            this.deviceDisplay2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deviceDisplay2.BackgroundImage")));
            this.deviceDisplay2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deviceDisplay2.Location = new System.Drawing.Point(819, 12);
            this.deviceDisplay2.Name = "deviceDisplay2";
            this.deviceDisplay2.Size = new System.Drawing.Size(378, 615);
            this.deviceDisplay2.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 707);
            this.Controls.Add(this.deviceDisplay2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.timeline1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.colorPickerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "EGS LED Studio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.colorPickerPanel.ResumeLayout(false);
            this.colorPickerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickerPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel colorPickerPanel;
        private System.Windows.Forms.PictureBox colorPickerPictureBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox blueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox greenTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox redTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private Timeline timeline1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Button button2;
        private DeviceDisplay deviceDisplay2;
    }
}

