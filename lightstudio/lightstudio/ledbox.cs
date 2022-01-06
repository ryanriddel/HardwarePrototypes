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
    public enum Orientation { Horizontal, Vertical, Corner}
    public enum OrientationWidth { Horizontal=42, Vertical=19, Corner=33}
    public enum OrientationHeight { Horizontal = 20, Vertical = 25, Corner = 33 }



    public partial class ledbox : UserControl
    {
        public static List<ledbox> highlightedCellList = new List<ledbox>();
        public static Dictionary<int, List<ledbox>> numToBox = new Dictionary<int, List<ledbox>>();



        ToolTip myTooltip;

        public Color boxColor = Color.White;
        Orientation boxOrientation = Orientation.Horizontal;
        public byte pixelNumber;

        Point centerPoint = new Point(0, 0);
        Color highlightColor = Color.White;
        int highlightThickness = 3;
        bool isHighlighted = false;
        public Rectangle rectArea;

        public ledbox(byte pixelNum)
        {
            InitializeComponent();
            rectArea = new Rectangle(new Point(this.Location.X, this.Location.Y), new Size(this.Width, this.Height));
            pixelNumber = pixelNum;

            myTooltip = new ToolTip();
            myTooltip.AutoPopDelay = 2000;
            myTooltip.InitialDelay = 1000;
            myTooltip.ShowAlways = true;
            myTooltip.SetToolTip(this, "#: " + this.pixelNumber.ToString());
            myTooltip.Popup += MyTooltip_Popup;


            if (numToBox.ContainsKey(pixelNum) == false)
                numToBox[pixelNum] = new List<ledbox>();

            numToBox[pixelNum].Add(this);
        }

        private void MyTooltip_Popup(object sender, PopupEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Tooltip");
        }

        public void HighlightCell(bool doHighlight)
        {
            isHighlighted = doHighlight;

            if (doHighlight)
                highlightedCellList.Add(this);
            else
                highlightedCellList.Remove(this);

            this.Invalidate();
            //this.InvokePaint();

        }

        public Point getCenterPoint()
        {
            return new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2);
        }

        public ledbox(byte pixelNum, Orientation orientation)
        {
            InitializeComponent();
            pixelNumber = pixelNum;
            rectArea = new Rectangle(new Point(this.Location.X, this.Location.Y), new Size(this.Width, this.Height));

            myTooltip = new ToolTip();
            myTooltip.AutoPopDelay = 2000;
            myTooltip.InitialDelay = 1000;
            myTooltip.ShowAlways = true;
            myTooltip.SetToolTip(this, "#: " + this.pixelNumber.ToString());
            myTooltip.Popup += MyTooltip_Popup;




            boxOrientation = orientation;

            if(boxOrientation == Orientation.Horizontal)
            {
                this.Size = new Size(27, 20);
            }
            else if(boxOrientation == Orientation.Vertical)
            {
                this.Size = new Size(19, 25);
            }
            else if(boxOrientation == Orientation.Corner)
            {
                this.Size = new Size(33, 33);
            }

            if (numToBox.ContainsKey(pixelNum) == false)
                numToBox[pixelNum] = new List<ledbox>();

            numToBox[pixelNum].Add(this);

        }

        private void ledbox_Paint(object sender, PaintEventArgs e)
        {
            if (isHighlighted)
            {
                using (var pen = new Pen(highlightColor, highlightThickness))
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - highlightThickness, this.Height - highlightThickness));
            }
            
        }

        private void ledbox_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.pixelNumber);
            bool newHighlightState = !this.isHighlighted;

            for (int j = 0; j < ledbox.numToBox[this.pixelNumber].Count; j++)
            {
                ledbox.numToBox[this.pixelNumber][j].HighlightCell(newHighlightState);
            }
        }
    }
}
