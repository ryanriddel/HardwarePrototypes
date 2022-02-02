#include <Adafruit_NeoPixel.h>



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
#define SERIALINPUTBUFFERSIZE 2048

Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(38400);
  pixels.begin();
  FlashLEDS();
  clearPixels();
}

//circular buffer
byte serialInputBuffer[SERIALINPUTBUFFERSIZE]={0};
int serialInputBufferIndex = 0;
int serialInputBufferReadIndex = 0;
int serialInputBufferWriteIndex = 0;
long timeOfLastSerialRead;


bool isAnimationPending = false;
bool isAnimationPlaying = false;
int currentAnimationFrame = 0;
long frameTimes[256] = {0};

void loop() {
  // put your main code here, to run repeatedly:
  
  
  //read serial in as quickly as possible
  while(Serial.available())
  {
    char inByte = Serial.read();

    serialInputBuffer[serialInputBufferWriteIndex++] = inByte;

    if(serialInputBufferWriteIndex >= SERIALINPUTBUFFERSIZE)
      serialInputBufferWriteIndex = 0;

    timeOfLastSerialRead = millis();
  }

  //if there are at least 4 bytes to read
  if(abs(serialInputBufferReadIndex - serialInputBufferWriteIndex) >= 4)
  {
    //arbitrary 5ms wait for all serial data to come in
      if(millis() - timeOfLastSerialRead > 5)
      {
        
        if(serialInputBuffer[serialInputBufferReadIndex] == 0xAB && serialInputBuffer[serialInputBufferReadIndex+1] == 0xCD && serialInputBuffer[serialInputBufferReadIndex+2] == 0xEF)
        {
          //this is a frame
          int bytesRead = getFrameFromBytes(frames[frameCount], serialInputBuffer, serialInputBufferReadIndex + 3);
          frameCount++;
          serialInputBufferReadIndex = (serialInputBufferReadIndex + 3 + bytesRead) % SERIALINPUTBUFFERSIZE;
        }
        else if(serialInputBuffer[serialInputBufferReadIndex] == 0xBA && serialInputBuffer[serialInputBufferReadIndex+1] == 0xDC && serialInputBuffer[serialInputBufferReadIndex+2] == 0xFE)
        {
          //Serial.println("COMMAND RECEIVED");
          //this is a command
          if(serialInputBuffer[serialInputBufferReadIndex+3] == 0xAA)
          {
            //clear frame buffer
            //Serial.println("CLEAR FB");
            frameCount = 0;
          }
          else if(serialInputBuffer[serialInputBufferReadIndex+3] == 0xBB)
          {
            //Serial.println("PLAYING ANIMATION");
            //play animation
            isAnimationPending = true;
          }
          else
          {
            //undefined command
            Serial.println("UNDEFINED COMMAND!");
          }

          serialInputBufferReadIndex = (serialInputBufferReadIndex + 4) % SERIALINPUTBUFFERSIZE;
          
        }
        else
        {
          //this is undefined
          Serial.println("PARSING ERROR!");
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
    
  pixels.clear();
  

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
          pixels.setPixelColor(a*8+b, pixels.Color(colorR, colorG, colorB));
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
