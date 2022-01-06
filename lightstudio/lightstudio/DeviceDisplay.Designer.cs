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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceDisplay));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setSelectedToTransparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSelectedToTransparentToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(260, 56);
            // 
            // setSelectedToTransparentToolStripMenuItem
            // 
            this.setSelectedToTransparentToolStripMenuItem.Name = "setSelectedToTransparentToolStripMenuItem";
            this.setSelectedToTransparentToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.setSelectedToTransparentToolStripMenuItem.Text = "Set Selected to Transparent";
            this.setSelectedToTransparentToolStripMenuItem.Click += new System.EventHandler(this.setSelectedToTransparentToolStripMenuItem_Click);
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setSelectedToTransparentToolStripMenuItem;
    }
}
