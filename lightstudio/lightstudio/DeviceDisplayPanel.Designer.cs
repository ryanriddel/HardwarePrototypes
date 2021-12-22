
namespace lightstudio
{
    partial class DeviceDisplayPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DeviceDisplayPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Name = "DeviceDisplayPanel";
            this.Size = new System.Drawing.Size(454, 658);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DeviceDisplayPanel_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DeviceDisplayPanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DeviceDisplayPanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DeviceDisplayPanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
