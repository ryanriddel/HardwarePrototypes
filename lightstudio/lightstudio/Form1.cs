using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lightstudio
{
   

    public partial class Form1 : Form
    {
        serialManagerForm serialManager = new serialManagerForm();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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


        private Dictionary<Color, pixelBitmap> GetDisplayPanelColorBitmap()
        {
            Dictionary<Color, pixelBitmap> colorBitmap = new Dictionary<Color, pixelBitmap>();
            foreach (ledbox box in deviceDisplay2.ledboxList)
            {
                Color thisColor = box.BackColor;

                
                if (thisColor == Color.Transparent)
                    thisColor = Color.Black;


                if (colorBitmap.ContainsKey(thisColor) == false)
                {
                    colorBitmap[thisColor] = new pixelBitmap();
                }
                colorBitmap[thisColor].setPixel(box.pixelNumber);

            }
            return colorBitmap;
        }
        private void buttonPeekFrame_Click(object sender, EventArgs e)
        {
            bool isSerialEnabled = true;
            if (serialManager.port == null)
                isSerialEnabled = false;
            else if (!serialManager.port.IsOpen)
                isSerialEnabled = false;


            Dictionary<Color, pixelBitmap> colorBitmap=GetDisplayPanelColorBitmap();


            byte numSubframes = (byte)colorBitmap.Keys.Count;
            int numBytes = 3 + 1 + 2 + numSubframes * (3 + 16);
            byte[] serialWriteOut = new byte[numBytes];

            //start of frame header
            serialWriteOut[0] = 171; //0xAB
            serialWriteOut[1] = 205; //0xCD
            serialWriteOut[2] = 239; //0xEF
            
            
            serialWriteOut[3] = numSubframes;


            Int16 subframeDuration = Convert.ToInt16(numericUpDown1.Value);
            serialWriteOut[4] = (byte)(subframeDuration & 255);
            serialWriteOut[5] = (byte)(subframeDuration >> 8);
            
            for(int i=0; i<colorBitmap.Keys.Count; i++)
            {
                Color clr = colorBitmap.Keys.ElementAt<Color>(i);
                serialWriteOut[6 + i * 19] = clr.R;
                serialWriteOut[7 + i * 19] = clr.G;
                serialWriteOut[8 + i * 19] = clr.B;

                
                byte[] colorByteBuffer = { clr.R, clr.G, clr.B };
                
                System.Diagnostics.Debug.WriteLine("Color: " + clr.R.ToString() + "  " + clr.G.ToString() + "  " + clr.B.ToString());

                byte[] pixelBitmapByteBuffer = new byte[16];
                for (int j = 0; j < 16; j++)
                {
                    
                    System.Diagnostics.Debug.Write(Convert.ToString(colorBitmap[clr].byteArray[j], 10));
                    System.Diagnostics.Debug.Write("  ");
                    serialWriteOut[9 + j + i * 19] = colorBitmap[clr].byteArray[j];
                }
                System.Diagnostics.Debug.WriteLine("");

            }
            System.Diagnostics.Debug.WriteLine("NumBytes: " + numBytes);
            //serialWriteOut[numBytes - 1] = Convert.ToByte('>');
            
            if(isSerialEnabled)
                serialManager.port.Write(serialWriteOut, 0, numBytes);


            //send play command
            serialWriteOut[0] = 186;  //0xBA
            serialWriteOut[1] = 220;  //0xDC
            serialWriteOut[2] = 254;  //0xFE
            serialWriteOut[3] = 187;  //0xBB: play animation  

            if(isSerialEnabled)
                serialManager.port.Write(serialWriteOut, 0, 4);

        }

        private void buttonAddFrame_Click(object sender, EventArgs e)
        {
            Dictionary < Color, pixelBitmap > colorBitmap = GetDisplayPanelColorBitmap();


            Frame newFrame = new Frame();

            for (int i = 0; i < colorBitmap.Keys.Count; i++)
            {

                Color clr = colorBitmap.Keys.ElementAt<Color>(i);
                pixelBitmap pBmp = new pixelBitmap();
                colorBitmap[clr].byteArray.CopyTo(pBmp.byteArray, 0);

                SubFrame sFrame = new SubFrame(clr, pBmp);
                newFrame.Subframes.Add(sFrame);
            }
            newFrame.durationMilliseconds = Convert.ToInt16(numericUpDown1.Value);

            //deselect all cells
            //foreach (ledbox box in this.deviceDisplay2.ledboxList)
              //  box.HighlightCell(false);

            Bitmap b = new Bitmap(deviceDisplay2.Width, deviceDisplay2.Height);
            deviceDisplay2.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
            newFrame.frameImage = b;

            ListViewItem newItem = new ListViewItem(new[] { newFrame.FrameID.ToString(), newFrame.Subframes.Count.ToString(), newFrame.durationMilliseconds.ToString() });
            newItem.Tag = newFrame;
            listView1.Items.Add(newItem);
            

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                Frame frame = (Frame)selectedItem.Tag;
                pictureBox7.BackgroundImage = frame.frameImage;
                pictureBox7.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count>0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                int index = item.Index;
                int numItems = listView1.Items.Count;

                if(index > 0)
                {
                    listView1.Items.Remove(item);
                    listView1.Items.Insert(index - 1, item);
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                int index = item.Index;
                int numItems = listView1.Items.Count;

                if (index < numItems-1)
                {
                    listView1.Items.Remove(item);
                    listView1.Items.Insert(index + 1, item);
                }
            }
        }

        private void buttonTrash_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];

                listView1.Items.Remove(item);
                int newIndex = 0;
                if (item.Index > 0)
                    newIndex = item.Index - 1;

                if (listView1.Items.Count > newIndex)
                {
                    listView1.Items[newIndex].Selected = true;

                    Frame frame = (Frame)listView1.Items[0].Tag;
                    pictureBox7.BackgroundImage = frame.frameImage;
                    pictureBox7.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    pictureBox7.BackgroundImage = null;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int numFramesPlayed = 0;
        bool isAnimationPlaying = false;
        long elapsedMilliseconds = 0;
        int currentSelectedIndex = 0;
        System.Diagnostics.Stopwatch watch;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int currentDuration = ((Frame)listView1.SelectedItems[0].Tag).durationMilliseconds;

            if(watch.ElapsedMilliseconds > elapsedMilliseconds + currentDuration)
            {
                elapsedMilliseconds = watch.ElapsedMilliseconds;
                if (isAnimationPlaying)
                {
                    //move to the next Frame
                    if (currentSelectedIndex < listView1.Items.Count-1)
                    {
                        numFramesPlayed++;
                        System.Diagnostics.Debug.WriteLine("#Frames: " + numFramesPlayed);
                        currentSelectedIndex++;
                        listView1.Items[currentSelectedIndex].Selected = true;
                        listView1.Items[currentSelectedIndex].EnsureVisible();
                            
                    }
                    else
                        isAnimationPlaying = false;
                }
                else
                {
                    timer1.Stop();
                    if (loopCheckbox.Checked)
                        buttonPlay_Click(null, null);
                }
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            //first, send a clear command.  Then, serialize the frames and send over serial.  Finally, send a play command
            bool isSerialEnabled = true;
            if (serialManager.port == null)
                isSerialEnabled=false;
            else if (!serialManager.port.IsOpen)
                isSerialEnabled=false;

            if(sender != null && e != null)
                numFramesPlayed = 0;

            byte[] clearCommand = { 186, 220, 254, 170 };
            if (isSerialEnabled)
                serialManager.port.Write(clearCommand, 0, 4);

            foreach (ListViewItem lvitem in listView1.Items)
            {
                Frame frm = (Frame) lvitem.Tag;
                byte[] frameBytes = GetBytesFromFrame(frm);
                byte[] serialMsg = new byte[frameBytes.Length + 3];

                serialMsg[0] = 171;
                serialMsg[1] = 205;
                serialMsg[2] = 239;

                for (int i = 0; i < frameBytes.Length; i++)
                    serialMsg[i + 3] = frameBytes[i];

                if(isSerialEnabled)
                    serialManager.port.Write(serialMsg, 0, serialMsg.Length);

            }
            //give the microcontroller a moment to receive and process the serial data
            //System.Threading.Thread.Sleep(10);
            
            byte[] playCommand = new byte[] { 186,220,254,187};
            if(isSerialEnabled)
                serialManager.port.Write(playCommand, 0, 4);

            listView1.Items[0].Selected = true;
            isAnimationPlaying = true;
            watch = new System.Diagnostics.Stopwatch();
            elapsedMilliseconds = 0;
            currentSelectedIndex = 0;
            watch.Start();
            timer1.Interval = 5;
            timer1.Start();
        }

        private byte[] GetBytesFromFrame(Frame frame)
        {

            int numSubframes = frame.Subframes.Count;

            //<1byte # of subframes><2byte duration><numsubframes*(3byte color + 16 byte pixelmap)>
            int numBytes = 1 + 2 + numSubframes * (3 + 16);
            
            byte[] byteArray = new byte[numBytes];
            byteArray[0] = (byte) numSubframes;
            
            byteArray[1] = (byte)(frame.durationMilliseconds & 255);
            byteArray[2] = (byte)(frame.durationMilliseconds >> 8);

            for (int i=0; i<numSubframes; i++)
            {
                byteArray[i * 19 + 3] = frame.Subframes[i].color.R;
                byteArray[i * 19 + 4] = frame.Subframes[i].color.G;
                byteArray[i * 19 + 5] = frame.Subframes[i].color.B;

                for (int j = 0; j < 16; j++)
                    byteArray[i * 19 + 6 + j] = frame.Subframes[i].subframeBitmap.byteArray[j];
            }

            return byteArray;
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                Frame frame = (Frame)selectedItem.Tag;
                pictureBox7.BackgroundImage = frame.frameImage;
                pictureBox7.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                ListViewItem newItem = (ListViewItem)selectedItem.Clone();
                listView1.Items.Insert(selectedItem.Index, newItem);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            isAnimationPlaying = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "LED File|*.led";
            DialogResult res = saveFileDialog1.ShowDialog();
            if(res == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                WriteFramesToFile(filename);

            }
        }

        private void WriteFramesToFile(string filePath)
        {
            StreamWriter file = File.AppendText(filePath);
            int frameCounter = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                Frame frame = (Frame)item.Tag;
                
                file.WriteLine("Frame#" + frameCounter + " " + frame.durationMilliseconds + " " + frame.Subframes.Count);

                for (int i = 0; i < frame.Subframes.Count; i++)
                {
                    //file.Write("<");
                    file.Write(frame.Subframes[i].color.R + "," + frame.Subframes[i].color.G + "," + frame.Subframes[i].color.B);
                    for (int j = 0; j < 16; j++)
                    {
                        file.Write(",");
                        file.Write(frame.Subframes[i].subframeBitmap.byteArray[j]);
                    }
                    file.WriteLine("");
                }
                frameCounter++;
            }
            file.Flush();
            file.Close();
        }

        private List<Frame> LoadFramesFromFile(string filePath)
        {
            string[] allLines = System.IO.File.ReadAllLines(filePath);
            int currentLine = 0;
            List<Frame> frameList = new List<Frame>();

            while(currentLine < allLines.Length)
            {
                string[] lineParts = allLines[currentLine].Split(" ");
                
                if(lineParts[0].StartsWith("Frame"))
                {
                    Frame newFrame = new Frame(Convert.ToInt16(lineParts[1]));
                    byte numSubframes = Convert.ToByte(lineParts[2]);

                    for(int i=0; i<numSubframes; i++)
                    {
                        currentLine++;

                        lineParts = allLines[currentLine].Split(",");
                        Color clr = Color.FromArgb(Convert.ToByte(lineParts[0]), Convert.ToByte(lineParts[1]), Convert.ToByte(lineParts[2]));

                        pixelBitmap bmap = new pixelBitmap();

                        for (int j = 0; j < 16; j++)
                            bmap.byteArray[j] = Convert.ToByte(lineParts[3 + j]);

                        SubFrame sFrame = new SubFrame(clr, bmap);

                        newFrame.Subframes.Add(sFrame);
                    }
                    frameList.Add(newFrame);
                    currentLine++;

                }
            }

            return frameList;
        }

        void WriteFrameToDeviceDisplay(Frame frame)
        {
            for (int i = 0; i < deviceDisplay2.ledboxList.Count; i++)
            {
                deviceDisplay2.ledboxList[i].HighlightCell(false);
                deviceDisplay2.ledboxList[i].BackColor = Color.Transparent;
            }

            foreach(SubFrame sFrame in frame.Subframes)
            {
                Color clr = sFrame.color;

                for(int i=0; i<16; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (((( (int) sFrame.subframeBitmap.byteArray[i]) >> j ) & 1) > 0)
                        {
                            for (int k = 0; k < ledbox.numToBox[i * 8 + j].Count; k++)
                            {
                                ledbox.numToBox[i * 8 + j][k].BackColor = clr;
                                ledbox.numToBox[i * 8 + j][k].Invalidate();
                            }
                        }
                    }
                }
            }
            deviceDisplay2.Invalidate();

            Bitmap b = new Bitmap(deviceDisplay2.Width, deviceDisplay2.Height);
            deviceDisplay2.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
            frame.frameImage = b;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "LED File|*.led";
            DialogResult res = openFileDialog1.ShowDialog();

            if(res == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                List<Frame> frames = LoadFramesFromFile(filename);

                if (frames.Count > 0)
                    listView1.Items.Clear();

                for (int i = 0; i < frames.Count; i++)
                {
                    WriteFrameToDeviceDisplay(frames[i]);
                    ListViewItem newItem = new ListViewItem(new[] { frames[i].FrameID.ToString(), frames[i].Subframes.Count.ToString(), frames[i].durationMilliseconds.ToString() });
                    newItem.Tag = frames[i];
                    listView1.Items.Add(newItem);
                }
            }
        }
    }
}
