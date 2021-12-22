using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lightstudio.Events;
using lightstudio.Timing;
using lightstudio.TestObjects;


namespace lightstudio
{
   

    public partial class Form1 : Form
    {
        private TimeBeamClock _clock = new TimeBeamClock();
        serialManagerForm serialManager = new serialManagerForm();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //colorDialog1.ShowDialog();
            AdjustMyLength track1 = new AdjustMyLength { Start = 0, End = 100, Name = "Track1" };
            AdjustMyParts track2 = new AdjustMyParts(10) { Name = "Track1" };
            
            
            timeline1.AddTrack(track1);
            timeline1.AddTrack(track2);

            
        }

        private void colorPickerPictureBox_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorPickerPictureBox.BackColor = colorDialog1.Color;
                updateColorPickerPanel(colorDialog1.Color);
            }
            
        }

        private void updateColorPickerBox()
        {
            try
            {
                Color newColor = Color.FromArgb(Convert.ToInt16(redTextBox.Text), Convert.ToInt16(greenTextBox.Text), Convert.ToInt16(blueTextBox.Text));
                colorPickerPictureBox.BackColor = newColor;

                for (int i = 0; i < ledbox.highlightedCellList.Count; i++)
                {
                    ledbox.highlightedCellList[i].BackColor = newColor;
                }
            }
            catch(Exception e)
            {

            }
        }

        private void updateColorPickerPanel(Color newColor)
        {
            if(newColor==Color.Transparent)
            {
                newColor = Color.Black;
            }

            redTextBox.Text = newColor.R.ToString();
            greenTextBox.Text = newColor.G.ToString();
            blueTextBox.Text = newColor.B.ToString();

            updateColorPickerBox();

            
        }

        private void redTextBox_TextChanged(object sender, EventArgs e)
        {
            updateColorPickerBox();

        }

        private void greenTextBox_TextChanged(object sender, EventArgs e)
        {
            updateColorPickerBox();
        }

        private void blueTextBox_TextChanged(object sender, EventArgs e)
        {
            updateColorPickerBox();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            serialManager.ShowDialog();

            
        }

        Dictionary<Color, List<ledbox>> colorHashmap;
        Dictionary<Color, pixelBitmap> colorBitmap;
        private void button2_Click(object sender, EventArgs e)
        {
            if (serialManager.isConnected == false)
                return;
            
            
            
            
            colorHashmap = new Dictionary<Color, List<ledbox>>();
            colorBitmap = new Dictionary<Color, pixelBitmap>();

            foreach(ledbox box in deviceDisplay2.ledboxList)
            {
                Color thisColor = box.BackColor;

                if(thisColor != Color.Transparent)
                {
                    if(colorHashmap.ContainsKey(thisColor) == false)
                    {
                        colorHashmap[thisColor] = new List<ledbox>();
                    }

                    colorHashmap[thisColor].Add(box);

                    if(colorBitmap.ContainsKey(thisColor) == false)
                    {
                        colorBitmap[thisColor] = new pixelBitmap();
                    }
                    colorBitmap[thisColor].setPixel(box.pixelNumber);

                }
            }

            byte[] commandByte = { Convert.ToByte('C') };
            serialManager.port.Write(commandByte, 0, 1);

            byte numSubframes = (byte) colorBitmap.Keys.Count;
            commandByte[0] = numSubframes;
            serialManager.port.Write(commandByte, 0, 1);

            Int16 subframeDuration = Convert.ToInt16(numericUpDown1.Value);

            foreach (Color clr in colorBitmap.Keys)
            {
                
                //serialManager.port.Write("1231422");

                byte[] colorByteBuffer = { clr.R, clr.G, clr.B };
                //byte[] pixelBitmapByteBuffer = { 7, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                
                byte[] pixelBitmapByteBuffer = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    pixelBitmapByteBuffer[i] = colorBitmap[clr].byteArray[i];
                }


                serialManager.port.Write(colorByteBuffer, 0, 3);
                serialManager.port.Write(pixelBitmapByteBuffer, 0, 16);
            }

            
        }
    }
}
