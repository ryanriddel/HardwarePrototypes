using System.Drawing;

namespace lightstudio
{
    partial class DeviceDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceDisplay));
            this.SuspendLayout();
            // 
            // DeviceDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DoubleBuffered = true;
            this.Name = "DeviceDisplay";
            this.Size = new System.Drawing.Size(378, 608);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DeviceDisplay_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DeviceDisplay_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DeviceDisplay_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DeviceDisplay_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
