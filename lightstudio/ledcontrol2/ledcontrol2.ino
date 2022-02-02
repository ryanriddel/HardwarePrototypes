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

Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(38400);
  pixels.begin();
  FlashLEDS();
  clearPixels();
}
//circular buffer
byte serialInputBuffer[2048]={0};
int serialInputBufferIndex = 0;


bool isMessageOver = false;
bool isMessageStarted = false;

bool isAnimationPending = false;
bool isAnimationPlaying = false;
int currentAnimationFrame = 0;
long frameTimes[256] = {0};

void loop() {
  // put your main code here, to run repeatedly:
  while(Serial.available())
  {
    char inByte = Serial.read();
    
    
    if(inByte == '<'  && !isMessageStarted)
    {
      serialInputBufferIndex=0;
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
      serialInputBuffer[serialInputBufferIndex] = inByte;
      serialInputBufferIndex++;
    }
  }

  if(isMessageOver)
  {
    isMessageOver = false;
    isMessageStarted = false;
    
    String messageOut = "";
    messageOut += serialInputBufferIndex;
    messageOut += ":";
    for(int i=0; i<serialInputBufferIndex; i++)
    {
      messageOut += serialInputBuffer[i];
      messageOut += " ";
    }
    //Serial.println(messageOut);
    serialInputBufferIndex = 0;
    //Serial.flush();
    getFrameFromBytes(frames[frameCount], serialInputBuffer);
    frameCount++;

    
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

void getFrameFromBytes(Frame& newFrame, byte byteArray[])
{
  newFrame.subframeCount = 0;
  byte numSubframes = byteArray[0];

  
  newFrame.subframeCount = numSubframes;
  newFrame.frameDuration = byteArray[1] + (byteArray[2]<<8);

  int bytesPerSubframe = NUMPIXELMAPBYTES + 3;

  for(int j=0; j<numSubframes; j++)
  {

    newFrame.subframes[j].colorRed = byteArray[j*bytesPerSubframe + 3];
    newFrame.subframes[j].colorGreen = byteArray[j*bytesPerSubframe + 4];
    newFrame.subframes[j].colorBlue = byteArray[j*bytesPerSubframe + 5];

    for(int i=0; i<NUMPIXELMAPBYTES; i++)
    {
      newFrame.subframes[j].pixelMap[i] = byteArray[j*bytesPerSubframe + 6 + i];
    }

  }
  
}
