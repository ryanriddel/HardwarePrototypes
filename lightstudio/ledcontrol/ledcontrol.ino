#include <Adafruit_NeoPixel.h>
struct Subframe
{
  byte pixelMap[16] = {0};
  byte colorRed = 0;
  byte colorGreen = 0;
  byte colorBlue = 0;
  
  
};

struct Frame
{
  unsigned int subframeCount = 0;
  unsigned int frameDuration = 0; //milliseconds
  Subframe subframes[16];
  
};
  
Frame frames[256];
int frameCount = 0;

#define NUMPIXELS 38
#define PIN        5

Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(38400);
  pixels.begin();
  clearPixels();
}

byte inputBuffer[512]={0};
int inputBufferLength = 0;
bool isMessageOver = false;
bool isMessageStarted = false;
bool isAnimationPending = false;

void loop() {
  // put your main code here, to run repeatedly:
  while(Serial.available())
  {
    char inByte = Serial.read();

    if(inByte == '<'  && !isMessageStarted)
    {
      inputBufferLength=0;
      isMessageStarted = true;
    }
    else if(inByte == '>' && isMessageStarted)
    {
      isMessageOver = true;
    }
    else if(inByte == '$' && Serial.available() == 0)
    {
      isAnimationPending = true;
    }
    else
    {
      inputBuffer[inputBufferLength] = inByte;
      inputBufferLength++;
    }
  }

  if(isMessageOver)
  {
    isMessageOver = false;
    isMessageStarted = false;
    
    String messageOut = "";
    messageOut += inputBufferLength;
    messageOut += ":";
    for(int i=0; i<inputBufferLength; i++)
    {
      messageOut += inputBuffer[i];
      messageOut += " ";
    }
    //Serial.println(messageOut);
    inputBufferLength = 0;
    //Serial.flush();
    getFrameFromBytes(frames[frameCount], inputBuffer);
    frameCount++;

    
  }

  if(isAnimationPending )
  {
    isAnimationPending = false;
    for(int i=0; i<frameCount; i++)
    {
      ApplyFrame(frames[i]);
    }
    frameCount = 0;
  }
}




void FlashLEDS()
{
  for(int i=0; i<NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(255,255,255));
    pixels.show();
  }
  delay(2000);
  for(int i=0; i<NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(0,0,0));
    pixels.show();
  }
  delay(2000);
 
}

void ApplyFrames(Frame frameArray[], int frameCount)
{
  for(int i=0; i<frameCount; i++)
  {
    ApplyFrame(frameArray[i]);
    //delay(frameArray[i].frameDuration);
  }
}

void ApplyFrame(Frame& frame)
{
  if(frame.subframeCount < 1)
    return;
    
  pixels.clear();
  


  for(int i=0; i<frame.subframeCount; i++)
  {
  
    byte colorR = frame.subframes[i].colorRed;
    byte colorG = frame.subframes[i].colorGreen;
    byte colorB = frame.subframes[i].colorBlue;
    
  
    for(int a=0; a<16; a++)
    {
      for(int b=0; b<8; b++)
      {
        if( ((frame.subframes[i].pixelMap[a] >> b) & 1) == 1)
        {
          pixels.setPixelColor(a*8+b, pixels.Color(colorR, colorG, colorB));
        }
      }
    }
  }
  
  pixels.show();
  delay(frame.frameDuration);
  clearPixels();
}

void clearPixels()
{
  for(int i=0; i<NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(0,0,0));
  }
  pixels.show();
}

void getFrameFromBytes(Frame& newFrame, byte byteArray[])
{
  newFrame.subframeCount = 0;
  byte numSubframes = byteArray[0];

  
  newFrame.subframeCount = numSubframes;
  newFrame.frameDuration = byteArray[1] + (byteArray[2]<<8);

  for(int j=0; j<numSubframes; j++)
  {

    newFrame.subframes[j].colorRed = byteArray[j*19 + 3];
    newFrame.subframes[j].colorGreen = byteArray[j*19 + 4];
    newFrame.subframes[j].colorBlue = byteArray[j*19 + 5];

    for(int i=0; i<16; i++)
    {
      newFrame.subframes[j].pixelMap[i] = byteArray[j*19 + 6 + i];
    }

  }
  
}
