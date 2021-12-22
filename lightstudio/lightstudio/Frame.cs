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
        Color color;
        UInt32 colorBits;
        byte pixelBitmap = 0;

    }



    class Frame
    {
        public List<SubFrame> Subframes;
        public int durationMilliseconds = 0;

        Frame()
        {
            Subframes = new List<SubFrame>();
        }

        Frame(int duration)
        {
            durationMilliseconds = duration;
            Subframes = new List<SubFrame>();
        }
    }
}
