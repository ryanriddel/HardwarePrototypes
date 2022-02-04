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
    public partial class DeviceDisplay : UserControl
    {
        public List<ledbox> ledboxList = new List<ledbox>();
        public List<ledbox> highlightedBoxes = new List<ledbox>();

        public enum DeviceType { Display49Inch, LEDStrip};
        public DeviceDisplay()
        {
            InitializeComponent();

            highlightedBoxes = new List<ledbox>();

            DeviceType dType = DeviceType.Display49Inch;
            int numLEDS = 38;
            PopulateLEDBoxes(dType, numLEDS);
        }

        int frameOffsetX = 8;
        int frameOffsetY = 10;

        public void ClearLEDBoxes()
        {
            for (int i = 0; i < ledboxList.Count; i++)
            {
                ledboxList[i].Dispose();
            }
            ledboxList.Clear();
        }
        public void PopulateLEDBoxes(DeviceType dType, int numLEDS)
        {
            if (dType == DeviceType.Display49Inch)
                Create49InchDisplayBoxes();
            else if (dType == DeviceType.LEDStrip)
                CreateLEDStripBoxes(numLEDS);

        }

        void CreateLEDStripBoxes(int numLEDS)
        {
            throw new NotImplementedException();
            this.BackgroundImage = null;
            this.BackColor = Color.Black;
            this.Invalidate();
        }

        void Create49InchDisplayBoxes()
        {
            
            ledboxList = new List<ledbox>();
            int numTopBoxes = 5;
            int numVerticalBoxes = 21;


            ledbox topCenterBox = new ledbox(0, Orientation.Horizontal);
            topCenterBox.Name = "ledbox0";
            topCenterBox.Location = new Point(this.Width / 2 - topCenterBox.Width / 2, frameOffsetY);
            this.Controls.Add(topCenterBox);
            ledboxList.Add(topCenterBox);

            ledbox ltCornerBox = new ledbox((byte)(numTopBoxes + 1), Orientation.Corner);
            ltCornerBox.Name = "ledbox" + (numTopBoxes + 1).ToString() + "-L";
            ltCornerBox.Location = new Point(frameOffsetX, frameOffsetY);
            this.Controls.Add(ltCornerBox);
            ledboxList.Add(ltCornerBox);

            ledbox rtCornerBox = new ledbox((byte)(numTopBoxes + 1), Orientation.Corner);
            rtCornerBox.Name = "ledbox" + (numTopBoxes + 1).ToString() + "-R";
            rtCornerBox.Location = new Point(this.Width - rtCornerBox.Width - frameOffsetX, frameOffsetY);
            this.Controls.Add(rtCornerBox);
            ledboxList.Add(rtCornerBox);

            //top horizontal boxes
            for (int i = 0; i < numTopBoxes; i++)
            {
                ledbox newTopBoxL = new ledbox((byte)(numTopBoxes - i), Orientation.Horizontal);
                newTopBoxL.Name = "ledbox" + (i + 1) + "-L";
                newTopBoxL.Location = new Point(frameOffsetX + ltCornerBox.Width + i * newTopBoxL.Width, frameOffsetY);

                ledbox newTopBoxR = new ledbox((byte)(numTopBoxes - i), Orientation.Horizontal);
                newTopBoxR.Name = "ledbox" + (i + 1) + "-R";
                newTopBoxR.Location = new Point(this.Width - frameOffsetX - rtCornerBox.Width - (i + 1) * newTopBoxR.Width, frameOffsetY);

                this.Controls.Add(newTopBoxL);
                ledboxList.Add(newTopBoxL);
                this.Controls.Add(newTopBoxR);
                ledboxList.Add(newTopBoxR);
            }

            for (int i = 0; i < numVerticalBoxes; i++)
            {
                ledbox newSideBoxL = new ledbox((byte)(i + numTopBoxes + 2), Orientation.Vertical);
                newSideBoxL.Name = "ledbox" + (i + numTopBoxes) + "-L";
                newSideBoxL.Location = new Point(frameOffsetX, i * newSideBoxL.Height + ltCornerBox.Height + frameOffsetY);

                ledbox newSideBoxR = new ledbox((byte)(i + numTopBoxes + 2), Orientation.Vertical);
                newSideBoxR.Name = "ledbox" + (i + numTopBoxes) + "-R";
                newSideBoxR.Location = new Point(this.Width - newSideBoxR.Width - frameOffsetX, rtCornerBox.Height + i * newSideBoxR.Height + frameOffsetY);

                this.Controls.Add(newSideBoxL);
                ledboxList.Add(newSideBoxL);
                this.Controls.Add(newSideBoxR);
                ledboxList.Add(newSideBoxR);
            }

            ledbox lbCornerBox = new ledbox((byte)(numVerticalBoxes + numTopBoxes + 2), Orientation.Corner);
            lbCornerBox.Name = "ledbox" + (numVerticalBoxes + numTopBoxes) + "-L";
            lbCornerBox.Location = new Point(frameOffsetX, rtCornerBox.Height + numVerticalBoxes * ((int)OrientationHeight.Vertical) + frameOffsetY);
            this.Controls.Add(lbCornerBox);
            ledboxList.Add(lbCornerBox);

            ledbox rbCornerBox = new ledbox((byte)(numVerticalBoxes + numTopBoxes + 2), Orientation.Corner);
            rbCornerBox.Name = "ledbox" + (numVerticalBoxes + numTopBoxes) + "-R";
            rbCornerBox.Location = new Point(this.Width - frameOffsetX - rbCornerBox.Width, rtCornerBox.Height + numVerticalBoxes * ((int)OrientationHeight.Vertical) + frameOffsetY);
            this.Controls.Add(rbCornerBox);
            ledboxList.Add(rbCornerBox);


            int bottomHorizontalY = 2 * rtCornerBox.Height + numVerticalBoxes * ((int)OrientationHeight.Vertical) + frameOffsetY - ((int)OrientationHeight.Horizontal);
            //bottom horizontal boxes
            for (int i = 0; i < numTopBoxes; i++)
            {
                ledbox newBottomBoxL = new ledbox((byte)(i + numTopBoxes + numVerticalBoxes + 3), Orientation.Horizontal);
                newBottomBoxL.Name = "ledbox" + (i + numTopBoxes + numVerticalBoxes + 1) + "-L";
                //newBottomBoxL.Location = new Point(frameOffsetX + lbCornerBox.Width + i * newBottomBoxL.Width, this.Height-newBottomBoxL.Height-frameOffsetY);
                newBottomBoxL.Location = new Point(frameOffsetX + lbCornerBox.Width + i * newBottomBoxL.Width, bottomHorizontalY);

                ledbox newBottomBoxR = new ledbox((byte)(i + numTopBoxes + numVerticalBoxes + 3), Orientation.Horizontal);
                newBottomBoxR.Name = "ledbox" + (i + numTopBoxes + numVerticalBoxes + 1) + "-R";
                newBottomBoxR.Location = new Point(this.Width - frameOffsetX - rbCornerBox.Width - (i + 1) * newBottomBoxR.Width, bottomHorizontalY);

                this.Controls.Add(newBottomBoxL);
                ledboxList.Add(newBottomBoxL);
                this.Controls.Add(newBottomBoxR);
                ledboxList.Add(newBottomBoxR);
            }

            ledbox bottomCenterBox = new ledbox((byte)(numTopBoxes * 2 + numVerticalBoxes + 3), Orientation.Horizontal);
            bottomCenterBox.Name = "ledbox" + (numTopBoxes * 2 + numVerticalBoxes + 3).ToString();
            bottomCenterBox.Location = new Point(this.Width / 2 - bottomCenterBox.Width / 2, bottomHorizontalY);
            this.Controls.Add(bottomCenterBox);
            ledboxList.Add(bottomCenterBox);
        }

        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        private void DeviceDisplay_Paint(object sender, PaintEventArgs e)
        {
            
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                {
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            
        }

        private void DeviceDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            RectStartPoint = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < ledboxList.Count; i++)
                    {
                        if (ledboxList[i].rectArea.Contains(e.Location))
                        {
                            ledboxList[i].HighlightCell(true);
                        }
                    }
                }
                else
                {

                    UnHighlightBoxes();
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, new Point(e.X, e.Y));
            }
        }

        private void DeviceDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Rect.Contains(e.Location))
                {
                    Console.WriteLine("Right click");
                }
            }
            else if(e.Button == MouseButtons.Left)
            {
                if (GetBoxesInRectangle(Rect).Count > 0)
                {
                    HighlightBoxes(GetBoxesInRectangle(Rect));

                    Rect.Size = new Size(1, 1);
                }
                else
                {
                    UnHighlightBoxes();
                    Rect.Size = new Size(1,1);
                }
                
                this.Invalidate();
            }
        }
        private List<ledbox> GetBoxesInRectangle(Rectangle rect)
        {
            List<ledbox> intersectingBoxes = new List<ledbox>();

            for(int i=0; i<ledboxList.Count; i++)
            {
                if (rect.Contains(ledboxList[i].getCenterPoint()))
                    intersectingBoxes.Add(ledboxList[i]);
            }
            return intersectingBoxes;
        }

        private void HighlightBoxes(List<ledbox> boxesToHighlight)
        {
            for (int i = 0; i < boxesToHighlight.Count; i++)
            {
                
                for(int j=0; j< ledbox.numToBox[boxesToHighlight[i].pixelNumber].Count; j++)
                {
                    ledbox.numToBox[boxesToHighlight[i].pixelNumber][j].HighlightCell(true);
                
                }
            }
        }

        private void UnHighlightBoxes()
        {
            //why do i unhighlight all ledboxes twice like this, you might wonder?
            //i dont know.  this fixes a very irritating bug where sometimes pixels wouldnt unhighlight.
            
            for (int i = 0; i < ledboxList.Count; i++)
                ledboxList[i].HighlightCell(false);
            for (int i = 0; i < ledboxList.Count; i++)
                ledboxList[i].HighlightCell(false);
            
        }
        
        private void DeviceDisplay_MouseMove(object sender, MouseEventArgs e)
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

        private void setSelectedToTransparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ledbox.highlightedCellList.Count; i++)
            {
                ledbox.highlightedCellList[i].BackColor = Color.Transparent;
            }
        }
    }
}
