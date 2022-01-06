using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace lightstudio
{
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

    class SubFrame
    {
        public Color color;
        public pixelBitmap subframeBitmap;

        public SubFrame(Color subframeColor, pixelBitmap subframePixelBitmap)
        {
            color = subframeColor;
            subframeBitmap = subframePixelBitmap;
        }

    }



    class Frame
    {
        public List<SubFrame> Subframes;
        public int durationMilliseconds = 0;
        public Bitmap frameImage;
        public Guid FrameID;

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

    class FrameListViewItem : System.Windows.Forms.ListViewItem
    {
        public Frame frame;
    }
}
