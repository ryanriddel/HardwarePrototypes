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
using ledcontrollerlib;
using static ledcontrollerlib.LEDController;

namespace lightstudio
{
    
    public partial class Form1 : Form
    {
        serialManagerForm serialManager = new serialManagerForm();

        LEDController ledcontroller;

        Dictionary<Guid, Bitmap> frameToBitmapDict = new Dictionary<Guid, Bitmap>();

        public Form1()
        {
            InitializeComponent();
            ledcontroller = new LEDController();
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
            
            if (serialManager.port != null )
                ledcontroller.serialPort = serialManager.port;
                    
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
            
            Dictionary<Color, LEDController.pixelBitmap> colorBitmap=GetDisplayPanelColorBitmap();
            List<SubFrame> sframeList = ledcontroller.ConvertColorBitmapsToSubframeList(colorBitmap);
            Frame frame = ledcontroller.ConvertSubframesToFrame(sframeList, Convert.ToInt16(numericUpDown1.Value));
            List<Frame> frameList = new List<Frame>();
            frameList.Add(frame);
            ledcontroller.PlayAnimationOnce(frameList);
            
        }

        private void buttonAddFrame_Click(object sender, EventArgs e)
        {
            Dictionary < Color, pixelBitmap > colorBitmap = GetDisplayPanelColorBitmap();
            List<SubFrame> subframeList = ledcontroller.ConvertColorBitmapsToSubframeList(colorBitmap);
            
            Frame newFrame = ledcontroller.ConvertSubframesToFrame(subframeList, Convert.ToInt16(numericUpDown1.Value));

            Bitmap b = new Bitmap(deviceDisplay2.Width, deviceDisplay2.Height);
            deviceDisplay2.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
            frameToBitmapDict[newFrame.FrameID] = b;

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
                pictureBox7.BackgroundImage = frameToBitmapDict[frame.FrameID];
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
                    pictureBox7.BackgroundImage = frameToBitmapDict[frame.FrameID];
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
            //this timer is used when playing animations to move through the listview 
            //at the same speed that the microcontroller plays the animation.
            //having said that, as the frame durations approach <5-10ms, the animation 
            //shown here will lag behind what the microcontroller is doing
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


            //if buttonPlay was clicked, as opposed to buttonPlay_Click
            //being called by another function (with both args equal to null)
            if (sender != null && e != null)
            {
                numFramesPlayed = 0;

                List<Frame> frameList = new List<Frame>();

                foreach (ListViewItem lvitem in listView1.Items)
                {
                    Frame frm = (Frame)lvitem.Tag;
                    frameList.Add(frm);

                }

                if (loopCheckbox.Checked)
                    ledcontroller.PlayAnimationLoop(frameList);
                else
                    ledcontroller.PlayAnimationOnce(frameList);

            }
            else
            {
                

            }

            listView1.Items[0].Selected = true;
            isAnimationPlaying = true;
            watch = new System.Diagnostics.Stopwatch();
            elapsedMilliseconds = 0;
            currentSelectedIndex = 0;
            watch.Start();
            timer1.Interval = 2;
            timer1.Start();
        }

        

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                Frame frame = (Frame)selectedItem.Tag;
                pictureBox7.BackgroundImage = frameToBitmapDict[frame.FrameID];
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

            ledcontroller.CancelCurrentAnimation();
        }

        private void buttonSaveFrameset_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "LED File|*.led";
            DialogResult res = saveFileDialog1.ShowDialog();
            if(res == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;

                List<Frame> frameList = new List<Frame>();

                foreach(ListViewItem item in listView1.Items)
                {
                    frameList.Add((Frame)item.Tag);
                }
                ledcontroller.WriteFramesToFile(frameList, filename);
            }
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
                
                for(int i=0; i< ledcontroller.NUMPIXELMAPBYTES; i++)
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
            frameToBitmapDict[frame.FrameID] = b;

        }

        private void buttonLoadFrameset_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "LED File|*.led";
            DialogResult res = openFileDialog1.ShowDialog();

            if(res == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                List<Frame> frames = ledcontroller.LoadFramesFromFile(filename);

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
