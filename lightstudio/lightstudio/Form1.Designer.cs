
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
            this.components = new System.ComponentModel.Container();
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.deviceDisplay2 = new lightstudio.DeviceDisplay();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonStop = new System.Windows.Forms.PictureBox();
            this.buttonCopy = new System.Windows.Forms.PictureBox();
            this.loopCheckbox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.buttonPlay = new System.Windows.Forms.PictureBox();
            this.buttonTrash = new System.Windows.Forms.PictureBox();
            this.buttonDown = new System.Windows.Forms.PictureBox();
            this.buttonUp = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorPickerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickerPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonCopy)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTrash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonUp)).BeginInit();
            this.panel4.SuspendLayout();
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
            this.colorPickerPanel.Location = new System.Drawing.Point(291, 16);
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
            this.blueTextBox.BackColor = System.Drawing.SystemColors.HighlightText;
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
            this.greenTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
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
            this.redTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
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
            this.panel1.Location = new System.Drawing.Point(16, 16);
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
            this.button1.Location = new System.Drawing.Point(264, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add Frame";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1139, 25);
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
            this.button2.Location = new System.Drawing.Point(10, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "Peek Frame";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // deviceDisplay2
            // 
            this.deviceDisplay2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deviceDisplay2.BackgroundImage")));
            this.deviceDisplay2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deviceDisplay2.Location = new System.Drawing.Point(748, 12);
            this.deviceDisplay2.Name = "deviceDisplay2";
            this.deviceDisplay2.Size = new System.Drawing.Size(378, 611);
            this.deviceDisplay2.TabIndex = 7;
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(59, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(285, 248);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Frame#";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "#Subframes";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Duration";
            this.columnHeader3.Width = 85;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.buttonStop);
            this.panel2.Controls.Add(this.buttonCopy);
            this.panel2.Controls.Add(this.loopCheckbox);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pictureBox7);
            this.panel2.Controls.Add(this.buttonPlay);
            this.panel2.Controls.Add(this.buttonTrash);
            this.panel2.Controls.Add(this.buttonDown);
            this.panel2.Controls.Add(this.buttonUp);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Location = new System.Drawing.Point(28, 233);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(670, 323);
            this.panel2.TabIndex = 9;
            // 
            // buttonStop
            // 
            this.buttonStop.BackgroundImage = global::lightstudio.Properties.Resources.stop_button1;
            this.buttonStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStop.Location = new System.Drawing.Point(234, 266);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(56, 54);
            this.buttonStop.TabIndex = 18;
            this.buttonStop.TabStop = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.BackgroundImage = global::lightstudio.Properties.Resources.papers;
            this.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCopy.Location = new System.Drawing.Point(4, 132);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(50, 55);
            this.buttonCopy.TabIndex = 17;
            this.buttonCopy.TabStop = false;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // loopCheckbox
            // 
            this.loopCheckbox.AutoSize = true;
            this.loopCheckbox.Location = new System.Drawing.Point(14, 266);
            this.loopCheckbox.Name = "loopCheckbox";
            this.loopCheckbox.Size = new System.Drawing.Size(119, 24);
            this.loopCheckbox.TabIndex = 16;
            this.loopCheckbox.Text = "Play On Loop";
            this.loopCheckbox.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Location = new System.Drawing.Point(500, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(156, 244);
            this.panel3.TabIndex = 15;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(10, 56);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(136, 29);
            this.button4.TabIndex = 16;
            this.button4.Text = "Load Frameset";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 29);
            this.button3.TabIndex = 15;
            this.button3.Text = "Save Frameset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox7.Location = new System.Drawing.Point(350, 16);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(143, 244);
            this.pictureBox7.TabIndex = 14;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackgroundImage = global::lightstudio.Properties.Resources.play_arrow;
            this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPlay.Location = new System.Drawing.Point(161, 266);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(51, 54);
            this.buttonPlay.TabIndex = 11;
            this.buttonPlay.TabStop = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonTrash
            // 
            this.buttonTrash.BackgroundImage = global::lightstudio.Properties.Resources.trash;
            this.buttonTrash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonTrash.Location = new System.Drawing.Point(4, 193);
            this.buttonTrash.Name = "buttonTrash";
            this.buttonTrash.Size = new System.Drawing.Size(51, 54);
            this.buttonTrash.TabIndex = 11;
            this.buttonTrash.TabStop = false;
            this.buttonTrash.Click += new System.EventHandler(this.buttonTrash_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.BackgroundImage = global::lightstudio.Properties.Resources.down_arrow;
            this.buttonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDown.Location = new System.Drawing.Point(3, 72);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(51, 54);
            this.buttonDown.TabIndex = 10;
            this.buttonDown.TabStop = false;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.BackgroundImage = global::lightstudio.Properties.Resources.up_arrow;
            this.buttonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonUp.Location = new System.Drawing.Point(2, 12);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(51, 54);
            this.buttonUp.TabIndex = 9;
            this.buttonUp.TabStop = false;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.colorPickerPanel);
            this.panel4.Location = new System.Drawing.Point(12, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(705, 157);
            this.panel4.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 629);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.deviceDisplay2);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonCopy)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTrash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonUp)).EndInit();
            this.panel4.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Button button2;
        private DeviceDisplay deviceDisplay2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox buttonPlay;
        private System.Windows.Forms.PictureBox buttonTrash;
        private System.Windows.Forms.PictureBox buttonDown;
        private System.Windows.Forms.PictureBox buttonUp;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox buttonStop;
        private System.Windows.Forms.PictureBox buttonCopy;
        private System.Windows.Forms.CheckBox loopCheckbox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

