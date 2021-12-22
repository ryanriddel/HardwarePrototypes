using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lightstudio
{
    public partial class DeviceDisplayPanel : UserControl
    {
        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        public DeviceDisplayPanel()
        {
            InitializeComponent();
        }

        private void DeviceDisplayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            RectStartPoint = e.Location;
            Invalidate();
        }

        private void DeviceDisplayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point tempEndPoint = e.Location;
            Rect.Location = new Point(
                Math.Min(RectStartPoint.X, tempEndPoint.X),
                Math.Min(RectStartPoint.Y, tempEndPoint.Y));
            Rect.Size = new Size(
                Math.Abs(RectStartPoint.X - tempEndPoint.X),
                Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            this.Invalidate();
        }

        private void DeviceDisplayPanel_Paint(object sender, PaintEventArgs e)
        {
            
            if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
            {
                e.Graphics.FillRectangle(selectionBrush, Rect);
            }
            
        }

        private void DeviceDisplayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Rect.Contains(e.Location))
                {
                    Console.WriteLine("Right click");
                }
            }
        }
    }
}
