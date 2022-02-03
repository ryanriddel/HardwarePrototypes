#include <Adafruit_NeoPixel.h>

//Some LED strips switch the Green and Blue values. Set to false if you are connecting to a Bestech display's LED's
#define USINGLEDSTRIP false

//if you want to change NUMPIXELMAPBYTES you will also have to change
//the relevant constant in the desktop application (lightstudio) or this wont work.
#define NUMPIXELMAPBYTES 16


struct Subframe
{
  byte pixelMap[NUMPIXELMAPBYTES] = {0};
  byte colorRed = 0;
  byte colorGreen = 0;
  byte colorBlue = 0;
  
  
};

struct PixelColor
{
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
#define SERIALINPUTBUFFERSIZE 4096

Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  pixels.begin();
  FlashLEDS();
  clearPixels();
}

//circular buffer
byte serialInputBuffer[SERIALINPUTBUFFERSIZE]={0};

long serialInputBufferReadIndex = 0;
long serialInputBufferWriteIndex = 0;
long timeOfLastSerialRead;
long bytesProcessed=0;
long bytesWritten=0;

bool isAnimationPending = false;
bool isAnimationPlaying = false;
int currentAnimationFrame = 0;
long frameTimes[256] = {0};

PixelColor pixelColors[NUMPIXELS];

void loop() {
  // put your main code here, to run repeatedly:
  
  
  //read serial in as quickly as possible
  while(Serial.available())
  {
    char inByte = Serial.read();

    serialInputBuffer[serialInputBufferWriteIndex++] = inByte;

    if(serialInputBufferWriteIndex >= SERIALINPUTBUFFERSIZE)
      serialInputBufferWriteIndex = 0;

    bytesWritten++;
    timeOfLastSerialRead = millis();
  }

  //this is where we process data...if there are at least 4 bytes to read
  if(abs(serialInputBufferReadIndex - serialInputBufferWriteIndex) >= 4 || (bytesWritten-bytesProcessed>=4))
  {
    //arbitrary wait for all serial data to come in
      if(millis() - timeOfLastSerialRead > 2)
      {
        
        if(serialInputBuffer[serialInputBufferReadIndex] == 0xAB && serialInputBuffer[(serialInputBufferReadIndex+1) % SERIALINPUTBUFFERSIZE] == 0xCD && serialInputBuffer[(serialInputBufferReadIndex+2) % SERIALINPUTBUFFERSIZE] == 0xEF)
        {
          //this is a frame
          int bytesRead = getFrameFromBytes(frames[frameCount], serialInputBuffer, serialInputBufferReadIndex + 3);
          frameCount++;
          serialInputBufferReadIndex = (serialInputBufferReadIndex + 3 + bytesRead) % SERIALINPUTBUFFERSIZE;

          bytesProcessed+=3+bytesRead;
        }
        else if(serialInputBuffer[serialInputBufferReadIndex] == 0xBA && serialInputBuffer[(serialInputBufferReadIndex+1) % SERIALINPUTBUFFERSIZE] == 0xDC && serialInputBuffer[(serialInputBufferReadIndex+2) % SERIALINPUTBUFFERSIZE] == 0xFE)
        {
          //this is a command
          if(serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE] == 0xAA)
          {
            //clear frame buffer
            frameCount = 0;
            
          }
          else if(serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE] == 0xBB)
          {
            //play animation
            isAnimationPending = true;
          }
          else
          {
            //undefined command
            Serial.println("UNDEFINED COMMAND!");
          }
          bytesProcessed+=4;
          serialInputBufferReadIndex = (serialInputBufferReadIndex + 4) % SERIALINPUTBUFFERSIZE;
          
        }
        else
        {
          //this is undefined. send an error message
          String errorMessage = "";
          errorMessage += serialInputBufferReadIndex;
          errorMessage += ".";
          errorMessage += serialInputBufferWriteIndex;
          errorMessage += ".";
          errorMessage += bytesWritten;
          errorMessage += ".";
          errorMessage += bytesProcessed;
          Serial.println(errorMessage);
          serialInputBufferReadIndex = 0;
          serialInputBufferWriteIndex = 0;
          bytesProcessed=0;
          bytesWritten=0;
          frameCount = 0;
        }
      }
  }

  if(isAnimationPending)
  {
    isAnimationPending = false;
    isAnimationPlaying = true;
    currentAnimationFrame = 0;
    
    frameTimes[0]=millis();
    for(int i=1; i<frameCount; i++)
    {
      frameTimes[i] = frameTimes[i-1] + frames[i].frameDuration;   
    }
    ApplyFrame(frames[0]);
  }

  if(isAnimationPlaying)
  {
    if(millis() > frameTimes[currentAnimationFrame] + frames[currentAnimationFrame].frameDuration)
    {
      if(currentAnimationFrame == frameCount)
      {
        //end animation
        isAnimationPlaying = false;
        frameCount = 0;
        clearPixels();
      }
      else
      {
        currentAnimationFrame++;
        ApplyFrame(frames[currentAnimationFrame]);
      }
      
    }
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


void ApplyFrame(Frame& frame)
{
  if(frame.subframeCount < 1)
    return;
    

  for(int i=0; i<frame.subframeCount; i++)
  {
  
    byte colorR = frame.subframes[i].colorRed;
    byte colorG = frame.subframes[i].colorGreen;
    byte colorB = frame.subframes[i].colorBlue;
    
  
    for(int a=0; a<NUMPIXELMAPBYTES; a++)
    {
      for(int b=0; b<8; b++)
      {
        if( ((frame.subframes[i].pixelMap[a] >> b) & 1) == 1)
        {
          //check to see if this pixel is already the correct color to cut down on unnecessary data transfers
          if(pixelColors[a*8+b].colorRed != colorR || pixelColors[a*8+b].colorGreen != colorG || pixelColors[a*8+b].colorBlue != colorB )
          {
            if(USINGLEDSTRIP)
              pixels.setPixelColor(a*8+b, pixels.Color(colorR, colorB, colorG));
            else
              pixels.setPixelColor(a*8+b, pixels.Color(colorR, colorG, colorB));
            
            pixelColors[a*8+b].colorRed=colorR;
            pixelColors[a*8+b].colorGreen=colorG;
            pixelColors[a*8+b].colorBlue=colorB;
          }
        }
      }
    }
  }
  
  pixels.show();
}

void clearPixels()
{
  for(int i=0; i<NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(0,0,0));
    pixelColors[i].colorRed=0;
    pixelColors[i].colorGreen=0;
    pixelColors[i].colorBlue=0;
  }
  pixels.show();
}

//returns the number of bytes read
int getFrameFromBytes(Frame& newFrame, byte byteArray[], int offset)
{
  newFrame.subframeCount = 0;
  byte numSubframes = byteArray[offset % SERIALINPUTBUFFERSIZE];

  
  newFrame.subframeCount = numSubframes;
  newFrame.frameDuration = byteArray[(offset + 1) % SERIALINPUTBUFFERSIZE] + (byteArray[(offset + 2) % SERIALINPUTBUFFERSIZE]<<8);

  int bytesPerSubframe = NUMPIXELMAPBYTES + 3;

  for(int j=0; j<numSubframes; j++)
  {

    newFrame.subframes[j].colorRed = byteArray[(j*bytesPerSubframe + 3 + offset) % SERIALINPUTBUFFERSIZE];
    newFrame.subframes[j].colorGreen = byteArray[(j*bytesPerSubframe + 4 + offset) % SERIALINPUTBUFFERSIZE];
    newFrame.subframes[j].colorBlue = byteArray[(j*bytesPerSubframe + 5 + offset) % SERIALINPUTBUFFERSIZE];

    for(int i=0; i<NUMPIXELMAPBYTES; i++)
    {
      newFrame.subframes[j].pixelMap[i] = byteArray[(j*bytesPerSubframe + 6 + i + offset) % SERIALINPUTBUFFERSIZE];
    }

  }

  int totalBytesRead = bytesPerSubframe * numSubframes + 3;

  return totalBytesRead; 
}
