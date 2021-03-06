using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.IO.Ports;

namespace ledcontrollerlib
{
    public class LEDController
    {
        public byte NUMPIXELMAPBYTES = 6;
        byte[] playCommand = new byte[] { 0xBA, 0xDC, 0xFE, 0xBB };
        byte[] playLoopCommand = new byte[] { 0xBA, 0xDC, 0xFE, 0xEE };
        byte[] cancelAnimationCommand = new byte[] { 0xBA, 0xDC, 0xFE, 0xCC };
        byte[] clearFrameBufferCommand = new byte[] { 0xBA, 0xDC, 0xFE, 0xAA };
        byte[] startOfFrameHeader = new byte[] { 0xAB, 0xCD, 0xEF };

        public SerialPort serialPort;

        
        
        public class pixelBitmap
        {
            public byte[] byteArray;

            public pixelBitmap()
            {
                byteArray = new byte[16];
            }

            public void setPixel(byte pixelNumber)
            {
                int integerPart = (int)Math.Floor((decimal)(pixelNumber / 8));
                int remainderPart = (int)(pixelNumber % 8);
                byteArray[integerPart] = (byte)(byteArray[integerPart] | (1 << remainderPart));
            }
        }

        public class SubFrame
        {
            public Color color;
            public pixelBitmap subframeBitmap;

            public SubFrame(Color subframeColor, pixelBitmap subframePixelBitmap)
            {
                color = subframeColor;
                subframeBitmap = subframePixelBitmap;
            }

        }


        public class Frame
        {
            public List<SubFrame> Subframes;
            public int durationMilliseconds = 0;
            public Guid FrameID;
            public byte StripID;

            public Frame()
            {
                Subframes = new List<SubFrame>();
                FrameID = Guid.NewGuid();
            }

            public Frame(int duration)
            {
                durationMilliseconds = duration;
                Subframes = new List<SubFrame>();
                FrameID = Guid.NewGuid();
            }
        }

        public class Animation
        {
            public List<Frame> Frames;
            public string Name;
            public int Duration = 0; //milliseconds
            public Animation()
            {
                Frames = new List<Frame>();
                Name = Guid.NewGuid().ToString();
            }

            public Animation(List<Frame> frameList)
            {
                Frames = frameList;

                foreach (Frame frm in frameList)
                    Duration += frm.durationMilliseconds;
            }
        }

        public LEDController()
        {

        
        }

        public bool ConnectSerialPort(string portName, int baudRate)
        {
            try
            {
                serialPort = new SerialPort(portName, baudRate);

                if (IsSerialEnabled())
                    return true;
                else return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        private bool IsSerialEnabled()
        {
            bool isSerialEnabled = false;
            if (serialPort == null)
                isSerialEnabled = false;
            else if (serialPort.IsOpen)
                isSerialEnabled = true;

            return isSerialEnabled;
        }

        /// <summary>
        /// Takes an ordered list of colors and returns a subframe for each
        /// </summary>
        /// <param name="pixelColors">A list of colors, with each element's index corresponding to the pixel number</param>
        /// <returns>A dictionary which associates each color present in the frame with a bitmap of pixels of that color</returns>
        private Dictionary<Color, pixelBitmap> ConvertPixelListToSubframeDictionary(List<Color> pixelColors)
        {
            Dictionary<Color, pixelBitmap> colorBitmap = new Dictionary<Color, pixelBitmap>();
            for(int i=0; i<pixelColors.Count; i++)
            {
                if (pixelColors[i] == Color.Transparent)
                    pixelColors[i] = Color.Black;


                if (colorBitmap.ContainsKey(pixelColors[i]) == false)
                {
                    colorBitmap[pixelColors[i]] = new pixelBitmap();
                }
                colorBitmap[pixelColors[i]].setPixel((byte)i);

            }
            return colorBitmap;
        }


        public List<SubFrame> ConvertPixelListToSubframeList(List<Color> pixelColors)
        {
            Dictionary<Color, pixelBitmap> colorBitmap = ConvertPixelListToSubframeDictionary(pixelColors);
            List<SubFrame> subframeList = new List<SubFrame>();

            foreach(Color clr in colorBitmap.Keys)
            {
                SubFrame newSubframe = new SubFrame(clr, colorBitmap[clr]);
                subframeList.Add(newSubframe);
            }

            return subframeList;
        }

        public List<SubFrame> ConvertColorBitmapsToSubframeList(Dictionary<Color, pixelBitmap> colorBitmaps)
        {
            List<SubFrame> subframeList = new List<SubFrame>();

            foreach (Color clr in colorBitmaps.Keys)
            {
                SubFrame newSubframe = new SubFrame(clr, colorBitmaps[clr]);
                subframeList.Add(newSubframe);
            }

            return subframeList;
        }

        public Frame ConvertColorBitmapsToFrame(Dictionary<Color, pixelBitmap> colorBitmaps, int duration)
        {
            return ConvertSubframesToFrame(ConvertColorBitmapsToSubframeList(colorBitmaps), duration);
        }

        public Frame ConvertPixelListToFrame(List<Color> pixelList, int duration)
        {
            return ConvertSubframesToFrame(ConvertPixelListToSubframeList(pixelList), duration);
        }

        public byte[] SerializeFrames(List<Frame> frameList, byte stripNum)
        {
            List<byte> byteList = new List<byte>();

            foreach (Frame frm in frameList)
            {

                byte[] frameBytes = GetBytesFromFrame(frm);
                byte[] serialMsg = new byte[frameBytes.Length + startOfFrameHeader.Length + 1];
                
                for (int i = 0; i < 3; i++)
                    byteList.Add(startOfFrameHeader[i]);

                byteList.Add(stripNum);
                
                for (int i = 0; i < frameBytes.Length; i++)
                    byteList.Add(frameBytes[i]);

            }

            return byteList.ToArray<byte>();
        }

        public Frame ConvertSubframesToFrame(List<SubFrame> subframeList, int frameDuration)
        {
            Frame returnFrame = new Frame(frameDuration);
            returnFrame.Subframes = subframeList;
            returnFrame.FrameID = Guid.NewGuid();

            return returnFrame;
        }

        public byte[] GetBytesFromFrame(Frame frame)
        {

            int numSubframes = frame.Subframes.Count;

            byte bytesPerSubframe = (byte)(3 + NUMPIXELMAPBYTES);
            int numBytes = 1 + 2 + numSubframes * bytesPerSubframe;

            byte[] byteArray = new byte[numBytes];
            byteArray[0] = (byte)numSubframes;

            byteArray[1] = (byte)(frame.durationMilliseconds & 255);
            byteArray[2] = (byte)(frame.durationMilliseconds >> 8);

            for (int i = 0; i < numSubframes; i++)
            {
                byteArray[i * bytesPerSubframe + 3] = frame.Subframes[i].color.R;
                byteArray[i * bytesPerSubframe + 4] = frame.Subframes[i].color.G;
                byteArray[i * bytesPerSubframe + 5] = frame.Subframes[i].color.B;

                for (int j = 0; j < NUMPIXELMAPBYTES; j++)
                    byteArray[i * bytesPerSubframe + 6 + j] = frame.Subframes[i].subframeBitmap.byteArray[j];
            }

            return byteArray;
        }

        public void WriteFramesToFile(List<Frame> frameList, string filePath)
        {
            StreamWriter file = File.AppendText(filePath);
            int frameCounter = 0;
            foreach (Frame frame in frameList)
            {

                file.WriteLine("Frame#" + frameCounter + " " + frame.durationMilliseconds + " " + frame.Subframes.Count);

                for (int i = 0; i < frame.Subframes.Count; i++)
                {
                    file.Write(frame.Subframes[i].color.R + "," + frame.Subframes[i].color.G + "," + frame.Subframes[i].color.B);
                    for (int j = 0; j < NUMPIXELMAPBYTES; j++)
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

        public List<Frame> LoadFramesFromFile(string filePath)
        {
            string[] allLines = System.IO.File.ReadAllLines(filePath);
            int currentLine = 0;
            List<Frame> frameList = new List<Frame>();

            while (currentLine < allLines.Length)
            {
                string[] lineParts = allLines[currentLine].Split(' ');

                if (lineParts[0].StartsWith("Frame"))
                {
                    Frame newFrame = new Frame(Convert.ToInt16(lineParts[1]));
                    byte numSubframes = Convert.ToByte(lineParts[2]);

                    for (int i = 0; i < numSubframes; i++)
                    {
                        currentLine++;

                        lineParts = allLines[currentLine].Split(',');
                        Color clr = Color.FromArgb(Convert.ToByte(lineParts[0]), Convert.ToByte(lineParts[1]), Convert.ToByte(lineParts[2]));

                        pixelBitmap bmap = new pixelBitmap();

                        for (int j = 0; j < NUMPIXELMAPBYTES; j++)
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

        public void PlayAnimationOnce(List<Frame> frameList)
        {
            byte[] serialOut = SerializeFrames(frameList, 0);

            if(IsSerialEnabled())
            {
                serialPort.Write(clearFrameBufferCommand, 0, 4);
                serialPort.Write(serialOut, 0, serialOut.Length);
                serialPort.Write(playCommand, 0, 4);
            }
        }

        public void PlayAnimationLoop(List<Frame> frameList)
        {
            byte[] serialOut = SerializeFrames(frameList, 0);

            if (IsSerialEnabled())
            {
                serialPort.Write(clearFrameBufferCommand, 0, 4);
                serialPort.Write(serialOut, 0, serialOut.Length);
                serialPort.Write(playLoopCommand, 0, 4);
            }
        }

        

        public void CancelCurrentAnimation()
        {
            if(IsSerialEnabled())
            {
                serialPort.Write(cancelAnimationCommand, 0, 4);
            }
        }
    }
}
